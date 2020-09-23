using Abp.Data;
using Hinnova.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Hinnova.EntityFrameworkCore.StoreProcedure
{
    public class SqlServerStoreRepository : ISqlServerStoreRepository
    {
        private const string SQL_DIRECTION_IN = "IN";
        private const string SQL_DIRECTION_OUT = "OUT";

        private static Dictionary<string, List<GetProcDetail>> DBArguments = new Dictionary<string, List<GetProcDetail>>();

        private readonly IActiveTransactionProvider _transactionProvider;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _appConfiguration;
        private DbConnection sqlConnection;

        public SqlServerStoreRepository(IActiveTransactionProvider transactionProvider,
            IWebHostEnvironment hostingEnvironment)
        {
            _transactionProvider = transactionProvider;
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
        }

        public async Task<List<T>> SelectDataList<T>(string packageName, string procName, Object parameters) where T : new()
        {
            var procDetail = await GetProcDetail(packageName, procName);

            await EnsureConnectionOpen();

            var result = new List<T>();
            using (var command = CreateCommand($"{packageName}.{procName}", CommandType.StoredProcedure, procDetail, parameters))
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())
                    {
                        result.Add(LoadObjectFromDataReader<T>(dataReader));
                    }
                }

                if (parameters != null && command.Parameters != null && command.Parameters.Count > 0)
                {
                    var typeParameter = parameters.GetType();
                    foreach (SqlParameter param in command.Parameters)
                    {
                        if (param.Direction == ParameterDirection.Output)
                        {
                            var property = typeParameter.GetProperty(param.ParameterName.Substring(1));
                            if (property != null)
                            {
                                property.SetValue(parameters, param.Value == DBNull.Value ? null : param.Value);
                            }
                        }
                    }
                }
            }

            EnsureConnectionClose();

            return result;
        }

        public async Task<DataSet> SelectDataSet(string packageName, string procName, Object parameters)
        {
            var procDetail = await GetProcDetail(packageName, procName);

            await EnsureConnectionOpen();

            var result = new DataSet();
            using (var command = CreateCommand($"{packageName}.{procName}", CommandType.StoredProcedure, procDetail, parameters))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command as SqlCommand;
                da.Fill(result);
            }

            EnsureConnectionClose();

            return result;
        }

        private async Task<List<GetProcDetail>> GetProcDetail(string packageName, string procName)
        {
            await EnsureConnectionOpen();

            var result = new List<GetProcDetail>();
            var query = $"SELECT * FROM VIEW_GET_PROC_DETAIL WHERE PACKAGE_NAME = '{packageName}' AND OBJECT_NAME = '{procName}'";

            using (var command = CreateCommand(query, CommandType.Text))
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {

                    while (dataReader.Read())
                    {
                        result.Add(LoadObjectFromDataReader<GetProcDetail>(dataReader));
                    }
                }
            }

            EnsureConnectionClose();

            return result;
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            if (sqlConnection != null)
            {
                var command = sqlConnection.CreateCommand();

                command.CommandText = commandText;
                command.CommandType = commandType;
                //command.Transaction = GetActiveTransaction();

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                return command;
            }
            return null;
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, List<GetProcDetail> userArgs, Object parameters)
        {
            if (sqlConnection != null)
            {
                var command = sqlConnection.CreateCommand();

                command.CommandText = commandText;
                command.CommandType = commandType;
                //command.Transaction = GetActiveTransaction();

                LoadParametersFromObject(command, parameters, userArgs);

                return command;
            }
            return null;
        }

        private async Task EnsureConnectionOpen()
        {
            sqlConnection = new SqlConnection(_appConfiguration["ConnectionStrings:Default"]);

            if (sqlConnection.State != ConnectionState.Open)
            {
                await sqlConnection.OpenAsync();
            }
        }

        private void EnsureConnectionClose()
        {
            if (sqlConnection != null)
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// Creates an object from the specified type and calls the DataReader => Object mapping function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private T LoadObjectFromDataReader<T>(IDataRecord reader) where T : new()
        {
            object theInstanceType = new T();
            LoadObjectFromDataReader(theInstanceType, reader);
            return (T)theInstanceType;
        }

        /// <summary>
        /// Parse and get returned object from data reader
        ///  DataReader => Object mapping function
        ///  Now recursive for propertys that are classes and inheritate from Entite
        /// </summary>
        /// <param name="theInstanceType"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private void LoadObjectFromDataReader(object theInstanceType, IDataRecord reader)
        {
            var infos = TypeDescriptor.GetProperties(theInstanceType.GetType());

            //if (infos != null && infos.Count > 0)
            foreach (PropertyDescriptor info in infos)
            {
                var miName = info.Name;

                for (var f = 0; f < reader.FieldCount; f++)
                {
                    var fName = reader.GetName(f);
                    if (!String.Equals(miName, fName, StringComparison.CurrentCultureIgnoreCase)) continue;
                    var value = reader.GetValue(f);
                    if (value != DBNull.Value)
                    {
                        var obj = value;
                        var typeConverter = info.Converter;
                        if (typeConverter != null)
                        {
                            if (value.GetType().Name == "UniqueIdentifier")
                            {
                                info.SetValue(theInstanceType, new Guid((byte[])obj));
                            }
                            else
                            {
                                if (info.PropertyType.Name == "Boolean" || info.PropertyType.Name == "bool" || info.PropertyType.FullName.Contains("System.Boolean"))
                                {
                                    if (value != null)
                                    {
                                        if (value.ToString() == "1") obj = true;
                                        else obj = false;
                                    }
                                }
                                else
                                {
                                    obj = typeConverter.ConvertFromString(value.ToString());
                                }

                                if (obj != null)
                                {
                                    info.SetValue(theInstanceType, obj);
                                }
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Loads the parameters from object.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The obj.</param>
        /// <param name="userArgs">The LST user args.</param>
        /// ReSharper disable once CSharpWarnings::CS0618
        private static void LoadParametersFromObject(DbCommand command, object parameters, List<GetProcDetail> userArgs)
        {
            if (command == null || parameters == null) return;

            command.Parameters.Clear();

            var t = parameters.GetType();
            var infos = TypeDescriptor.GetProperties(t);
            foreach (var UserArg in userArgs)
            {
                SqlParameter param;
                if (!String.IsNullOrEmpty(UserArg.ARGUMENT_NAME) && UserArg.POSITION > 0)
                {
                    var arg = UserArg;
                    var listInfo = infos.Cast<PropertyDescriptor>();
                    var info = (from p in listInfo
                                where String.Equals(arg.ARGUMENT_NAME, ("@" + p.Name.ToUpper()), StringComparison.CurrentCultureIgnoreCase)
                                select p).FirstOrDefault();

                    // infos.Cast<PropertyDescriptor>()s
                    //     .FirstOrDefault(i => arg.ARGUMENT_NAME.ToUpper() == ("P_" + i.Name).ToUpper());

                    param = new SqlParameter { ParameterName = UserArg.ARGUMENT_NAME, Size = UserArg.MAX_LENGTH };
                    switch (UserArg.IN_OUT)
                    {
                        case SQL_DIRECTION_IN:
                            param.Direction = ParameterDirection.Input;
                            break;
                        case SQL_DIRECTION_OUT:
                            param.Direction = ParameterDirection.Output;
                            break;
                        default:
                            param.Direction = ParameterDirection.InputOutput;
                            break;
                    }

                    param.SqlDbType = GetSqlDbType(UserArg.DATA_TYPE);
                    param.DbType = GetDbType(UserArg.DATA_TYPE);
                    if (param.SqlDbType == SqlDbType.Text && param.Direction != ParameterDirection.Input)
                    {
                        param.Size = 2000;
                    }

                    if (info != null)
                    {
                        var value = info.GetValue(parameters);
                        if (value == null)
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            param.Value = info.GetValue(parameters);
                        }
                    }
                    else
                    {
                        param.Value = DBNull.Value;
                    }

                    command.Parameters.Add(param);
                }
                else if (!String.IsNullOrEmpty(UserArg.DATA_TYPE))
                {
                    param = new SqlParameter
                    {
                        Direction = ParameterDirection.ReturnValue,
                        SqlDbType = GetSqlDbType(UserArg.DATA_TYPE),
                        Size = UserArg.MAX_LENGTH
                    };
                    command.Parameters.Add(param);
                }
            }
        }

        /// <summary>
        /// Gets the type of the oracle db.
        /// </summary>
        /// <param name="datatype">The datatype.</param>
        /// <returns></returns>
        private static SqlDbType GetSqlDbType(string datatype)
        {
            switch (datatype.ToUpper())
            {
                case "NVARCHAR": return SqlDbType.NVarChar;
                case "INT": return SqlDbType.Int;
                case "BIGINT": return SqlDbType.BigInt;
                case "DATE": return SqlDbType.Date;
                case "DATETIME": return SqlDbType.DateTime;
                case "BIT": return SqlDbType.Bit;
                case "CHAR": return SqlDbType.Char;
                case "BINARY": return SqlDbType.Binary;
                case "DECIMAL": return SqlDbType.Decimal;
                case "TINYINT": return SqlDbType.TinyInt;
                case "TEXT": return SqlDbType.Text;
                case "FLOAT": return SqlDbType.Float;
                case "UNIQUEIDENTIFIER": return SqlDbType.UniqueIdentifier;
                default: return SqlDbType.NVarChar;
            }
        }

        /// <summary>
        /// Gets the type of the db.
        /// </summary>
        /// <param name="datatype">The datatype.</param>
        /// <returns></returns>
        private static DbType GetDbType(string datatype)
        {
            switch (datatype.ToUpper())
            {
                case "NVARCHAR": return DbType.String;
                case "INT": return DbType.Int32;
                case "BIGINT": return DbType.Int64;
                case "DATE": return DbType.Date;
                case "DATETIME": return DbType.DateTime;
                case "BIT": return DbType.Boolean;
                case "CHAR": return DbType.String;
                case "BINARY": return DbType.Binary;
                case "DECIMAL": return DbType.Decimal;
                case "TINYINT": return DbType.Int32;
                case "TEXT": return DbType.String;
                case "FLOAT": return DbType.Decimal;
                case "GUID": return DbType.Guid;
                default: return DbType.String;
            }
        }
    }
}
