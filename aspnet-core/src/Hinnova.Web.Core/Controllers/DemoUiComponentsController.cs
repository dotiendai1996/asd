using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.IO.Extensions;
using Abp.UI;
using System;
using System.Net;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using Hinnova.Dto;
using Hinnova.Storage;
using Abp.Web.Models;
using Hinnova.DemoUiComponents.Dto;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Abp.Authorization;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Hinnova.QLVB;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Aspose.Words.MailMerging;
using Aspose.Words.Replacing;
using System.Threading;
using System.Globalization;
using Aspose.Words;
using Hinnova.Sessions;
using Aspose.Cells;

namespace Hinnova.Web.Controllers
{
    //[AbpMvcAuthorize]
    //[Route("api/[controller]")]
    //[ApiController]
    [AbpAllowAnonymous]
    public class DemoUiComponentsController : HinnovaControllerBase
    {

        public string pathUpload;
        public string ContentType;
        private readonly IWebHostEnvironment _env;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<HistoryUpload> _historyUploadRepository;
        private readonly string connectionString;
        private readonly ISessionAppService _sessionAppService;

        public DemoUiComponentsController(IBinaryObjectManager binaryObjectManager, IWebHostEnvironment hostingEnvironment, IRepository<HistoryUpload> historyUploadRepository, ISessionAppService sessionAppService)
        {
            _binaryObjectManager = binaryObjectManager;
            _env = hostingEnvironment;
            _historyUploadRepository = historyUploadRepository;
            connectionString = _env.GetAppConfiguration().GetConnectionString("Default");
            _sessionAppService = sessionAppService;
        }


        [WrapResult(false)]
        [RemoteService(false)]
        [HttpPost]

        // Scan


        public async Task Upload(int userId)
        {
            pathUpload = "";


            try
            {

                var files = Request.Form.Files;


                //var myFile = Request.Form.Files["file"];

                foreach (var file in files)
                {
                    if (file.Length > 1048576) //1MB
                    {
                        throw new UserFriendlyException(L("File_SizeLimit_Error"));
                    }


                    var path = Path.Combine(_env.WebRootPath, "Scan Document");
                    //Logger.Error("myFile" + file.FileName);
                    //Logger.Error("path" + path);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string filePath = Path.Combine(path, file.FileName);

                    string fileName = file.FileName;

                    FileInfo f = new FileInfo(Path.Combine(path, file.FileName));

                    if (f.Exists)
                    {
                        //f.FullName = file.FileName
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                await conn.OpenAsync();
                            }
                            var maxVersion = await conn.QueryAsync<int>(sql: "dbo.FindMaxVersionOfUploadFile", param: new { fileName }, commandType: CommandType.StoredProcedure);

                            using (var fileStream = System.IO.File.Create(Path.Combine(path, Path.GetFileNameWithoutExtension(file.FileName) + "_ver" + (maxVersion.AsList()[0] + 1) + Path.GetExtension(file.FileName))))
                            {
                                file.CopyTo(fileStream);
                                await _historyUploadRepository.InsertAsync(new HistoryUpload { File = file.FileName, Version = maxVersion.AsList()[0] + 1 });
                                ContentType = path + "\\" + Path.GetFileNameWithoutExtension(file.FileName) + "_ver" + (maxVersion.AsList()[0] + 1) + Path.GetExtension(file.FileName);
                                pathUpload = pathUpload + ContentType + ";";

                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                        {
                            file.CopyTo(fileStream);
                            await _historyUploadRepository.InsertAsync(new HistoryUpload { File = file.FileName, Version = 1 });
                            ContentType = path + "\\" + file.FileName;
                            pathUpload = pathUpload + ContentType + ";";

                        }
                        //file.CopyTo(Path.Combine(path, file.FileName));
                        //await _historyUploadRepository.InsertAsync(new HistoryUpload { File = file.FileName, Version = 1 });
                    }

                }



                //   Logger.Error("pathUpload" + myFileT.Files.GetFile);
                Logger.Error("pathUpload");
                //var myFile = Request.Form.Files["myFile"];

            }
            catch (Exception ex
           )
            {
                Logger.Error(ex.ToString);
                Response.StatusCode = 400;

            }

            //return Json(new AjaxResponse(new ErrorInfo("test")));
        }



        //Api upload file 

        [WrapResult(false)]
        [RemoteService(false)]
        [HttpPost]
        public async Task<string> Upload_file( string currentTime)
        {
            string date = DateTime.Today.ToString("dd-MM-yyyy");
            //string basePath = "temp\\" + userId;

            DateTime aDate = DateTime.Now;
            //string name = aDate.ToString("H-m"); 
            try
            {
                var files = Request.Form.Files;

                foreach (var file in files)
                {
           
                    var path = Path.Combine(_env.WebRootPath +"\\" + date , currentTime);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string fileName = file.FileName;

                    FileInfo f = new FileInfo(Path.Combine(path, file.FileName));

                    if (f.Exists)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                await conn.OpenAsync();
                            }
                          //  string newFileName =  Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName) + "/" + aDate.ToString("ss");
                            using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                            {
                                file.CopyTo(fileStream);
                                return path;
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                        {
                        
                            file.CopyTo(fileStream);
                            return path;
                        }
                    }

                   
                }
                return pathUpload;

            }
            catch (Exception ex
           )
            {
                Response.StatusCode = 400;
                return "";
            }

         
        }


        // upload HĐ

        public async Task<string> UploadHĐ(string path1 , string currentTime)
        {
          
            try
            {
                var files = Request.Form.Files;

                foreach (var file in files)
                {
                    var path = Path.Combine(_env.WebRootPath + "\\Template\\" + path1 ,  currentTime);

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string fileName = file.FileName;

                    FileInfo f = new FileInfo(Path.Combine(path, file.FileName));

                    if (f.Exists)
                    {

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                await conn.OpenAsync();
                            }
                            //  string newFileName =  Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName) + "/" + aDate.ToString("ss");
                            using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                            {

                               
                                file.CopyTo(fileStream);
                                return path;
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                        {

                            file.CopyTo(fileStream);
                            return path;
                        }
                    }


                }
                return pathUpload;

            }
            catch (Exception ex
           )
            {
                Response.StatusCode = 400;
                return "";
            }


        }

        //upload Image

        public async Task<string> Upload_fileImage()
        {
            string date = DateTime.Today.ToString("dd-MM-yyyy");
            //string basePath = "temp\\" + userId;

            DateTime aDate = DateTime.Now;
            //string name = aDate.ToString("H-m"); 
            try
            {
                var files = Request.Form.Files;

                foreach (var file in files)
                {
                    //if (file.Length > 1048576) //1MB
                    //{
                    //    throw new UserFriendlyException(L("File_SizeLimit_Error"));
                    //}
                    var path = Path.Combine(_env.WebRootPath , date );

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string fileName = file.FileName;

                    FileInfo f = new FileInfo(Path.Combine(path, file.FileName));

                    if (f.Exists)
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                await conn.OpenAsync();
                            }
                            //  string newFileName =  Path.GetFileNameWithoutExtension(fileName) + Path.GetExtension(fileName) + "/" + aDate.ToString("ss");
                            using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                            {
                                file.CopyTo(fileStream);
                                return path;
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                        {

                            file.CopyTo(fileStream);
                            return path;
                        }
                    }


                }
                return pathUpload;

            }
            catch (Exception ex
           )
            {
                Response.StatusCode = 400;
                return "";
            }

        }

        //Thống kê 

        public IActionResult GetExcelFile(string FromDate, string ToDate   ,string tenFile ,string tenCTY)
        {
            var curDir = Directory.GetCurrentDirectory();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("FromDate", FromDate);
            parameters.Add("ToDate", ToDate);
            parameters.Add("tenCTY", tenCTY);

            string filePath = curDir + "/wwwroot/XuatThongKe/"+ tenFile;
            string storeName = "GetListNhanVienToDateFromDate";
            
            DataSet data = DataFromStore(storeName, parameters);

            Workbook designer = new Workbook(filePath);

            WorkbookDesigner designWord = new WorkbookDesigner(designer);
            

            designWord.SetDataSource(data);
            designWord.Process(false);
            designWord.Workbook.FileName = filePath;
            designWord.Workbook.FileFormat = FileFormatType.Xlsx;
            designWord.Workbook.Settings.CalcMode = CalcModeType.Automatic;
            designWord.Workbook.Settings.RecalculateBeforeSave = true;
            designWord.Workbook.Settings.ReCalculateOnOpen = true;
            designWord.Workbook.Settings.CheckCustomNumberFormat = true;
            if(tenFile == "BaocaoTuNgayDenNgay.xlsx")
            {
                designer.Worksheets[0].Cells["A4"].PutValue("Từ " + Convert.ToDateTime(FromDate).ToString("dd-MM-yyyy") + " Đến " + Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy"));
            }
            else if (tenFile == "BaocaoThang.xlsx")
            {
                designer.Worksheets[0].Cells["A4"].PutValue("Tháng " + Convert.ToDateTime(FromDate).ToString("MM") + " Năm " + Convert.ToDateTime(ToDate).ToString("yyyy"));
            }
            //else if (tenFile == "BaocaoQuy.xlsx")
            //{

            //    designer.Worksheets[0].Cells["A4"].PutValue("Quý " + Convert.ToDateTime(FromDate).ToString("dd-MM-yyyy") + " Đến " + Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy"));
            //}
            else 
            {
                designer.Worksheets[0].Cells["A4"].PutValue("Năm " + Convert.ToDateTime(FromDate).ToString("yyyy")) ;
            }


            MemoryStream str = new MemoryStream();
            designWord.Workbook.Save(str, Aspose.Cells.SaveFormat.Xlsx);

            str.Position = 0;

            return File(str, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", tenFile);
        }

        private DataSet DataFromStore(string storedProcName, DynamicParameters parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var da = new SqlDataAdapter(storedProcName, conn);
                var ds = new DataSet();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                foreach (var item in parameters.ParameterNames)
                {
                    da.SelectCommand.Parameters.Add(new SqlParameter(item, parameters.Get<object>(item)));
                }
                da.Fill(ds);

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = "table" + i;
                }

                return ds;
            }
        }


        //conver .doc to Html 
       

            // In HĐ
            public IActionResult GetWordFile(int hopDongId , string path)
        {
         
            var curDir = Directory.GetCurrentDirectory();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("idHD", hopDongId);

            string filePath = curDir + "/wwwroot/"+path;
            string storeName = "GetInFoHopDongExPort";
            int[] arr = new int[] { };

            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DataSet data = DataFromStore(storeName, parameters);
            //data.Relations.Add(new DataRelation("CustomerType_Customer", data.Tables[1].Columns["Id"], data.Tables[0].Columns["CustomerTypeCode"], true));

            Document doc = new Document(filePath);
            var NgaySinh = Convert.ToDateTime(data.Tables[0].Rows[0]["NgaySinh"].ToString());
            string ngaySinh = NgaySinh.ToString("dd-MM-yyyy");
            var NgayKy = Convert.ToDateTime(data.Tables[0].Rows[0]["NgayKy"].ToString());
            string ngayKy = NgayKy.ToString("dd-MM-yyyy");
            var NgayCap = Convert.ToDateTime(data.Tables[0].Rows[0]["NgayCap"].ToString());
            string ngayCap = NgayCap.ToString("dd-MM-yyyy");
            var NgayCoHieuLuc = Convert.ToDateTime(data.Tables[0].Rows[0]["NgayCoHieuLuc"].ToString());
            string ngayCoHieuLuc = NgayCoHieuLuc.ToString("dd-MM-yyyy");
            var NgayHetHan = Convert.ToDateTime(data.Tables[0].Rows[0]["NgayHetHan"].ToString());
               string ngayHetHan = NgayHetHan.ToString("dd-MM-yyyy");

            double luongCB = (double)(data.Tables[0].Rows[0]["LuongCoBan"]);
            var day = DateTime.Now.ToString("dd");
            var month = DateTime.Now.ToString("MM");
            var year1 = DateTime.Now.ToString("yy");
            var year2 = DateTime.Now.ToString("yyyy");
            //Logger.Error("luongCB: " + luongCB);
            //DateTime aDate = DateTime.Now;
            doc.Range.Replace("<day>", day.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<month>", month.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<year1>", year1.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<year2>", year2.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<ngayKy>", ngayKy.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<ngaySinh>", ngaySinh.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<ngayCap>", ngayCap.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<ngayCoHieuLuc>", ngayCoHieuLuc.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<ngayHetHan>", ngayHetHan.ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<nguyenQuan>", data.Tables[0].Rows[0]["NguyenQuan"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<diaChiHKTT>", data.Tables[0].Rows[0]["DiaChiHKTT"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<soCMND>", data.Tables[0].Rows[0]["SoCMND"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<nganHangCode>", data.Tables[0].Rows[0]["NganHangCode"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<tkNganHang>", data.Tables[0].Rows[0]["TkNganHang"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<noiCap>", data.Tables[0].Rows[0]["NoiCap"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<hoTenNhanVien>", data.Tables[0].Rows[0]["HoTenNhanVien"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<quocTich>", data.Tables[0].Rows[0]["QuocTich"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<noiCap>", data.Tables[0].Rows[0]["NoiCap"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<viTriCongViecCode>", data.Tables[0].Rows[0]["ViTriCongViecCode"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<luongCoban>", luongCB.ToString("#,###"), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<gioiTinh>", data.Tables[0].Rows[0]["GioiTinh"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<danToc>", data.Tables[0].Rows[0]["DanToc"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<donViCongTac>", data.Tables[0].Rows[0]["DonViCongTac"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<tenHopDong>", data.Tables[0].Rows[0]["TenHopDong"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<noiSinh>", data.Tables[0].Rows[0]["NoiSinh"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<soHopDong>", data.Tables[0].Rows[0]["SoHopDong"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.Range.Replace("<thoiHanHopDong>", data.Tables[0].Rows[0]["thoiHanHopDong"].ToString(), new FindReplaceOptions(FindReplaceDirection.Forward));
            doc.MailMerge.CleanupParagraphsWithPunctuationMarks = true;
            doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields | MailMergeCleanupOptions.RemoveUnusedRegions;
            //doc.MailMerge.Execute(new string[] { "A" }, new string[] { "this is value of a" });
            doc.MailMerge.ExecuteWithRegions(data);
            MemoryStream stream = new MemoryStream();
            doc.Save(stream, Aspose.Words.SaveFormat.Docx);
            stream.Position = 0;
            var current = DateTime.Now.ToString("dd-MM-yyyy");
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "MauPhieuTrinh_" + current + ".docx");
        }



    }
}