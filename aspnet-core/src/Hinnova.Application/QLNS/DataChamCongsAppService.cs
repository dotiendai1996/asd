using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Hinnova.Authorization;
using Hinnova.Dto;
using Hinnova.QLNS.Dtos;
using Hinnova.QLNS.Exporting;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System;
using Hinnova.EntityFrameworkCore.StoreProcedure;
using System.Collections.Generic;
using Hinnova.QLNSDtos;
using Abp.Organizations;
using System.Security.Cryptography.Xml;
using Hinnova.Organizations.Dto;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Abp.Extensions;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Abp.UI;
using GemBox.Spreadsheet;
using System.IO;
using Hinnova.Authorization.Users.Importing;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;

namespace Hinnova.QLNS
{
    //[AbpAuthorize(AppPermissions.Pages_DataChamCongs)]
    public class DataChamCongsAppService : HinnovaAppServiceBase, IDataChamCongsAppService
    {
        private readonly IRepository<DataChamCong> _dataChamCongRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IDataChamCongsExcelExporter _dataChamCongsExcelExporter;
        private readonly ISqlServerStoreRepository _sqlServerStoreRepository;
        private readonly IHoSosAppService _hoSosAppService;
        private readonly string connectionString;

        public DataChamCongsAppService(
            IRepository<DataChamCong> dataChamCongRepository,
            IRepository<TruongGiaoDich> truongGiaoDichRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IDataChamCongsExcelExporter dataChamCongsExcelExporter,
            IWebHostEnvironment env,
            ISqlServerStoreRepository sqlServerStoreRepository,
            IHoSosAppService hoSosAppService)
        {
            _hoSosAppService = hoSosAppService;
            _dataChamCongRepository = dataChamCongRepository;
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _dataChamCongsExcelExporter = dataChamCongsExcelExporter;
            _sqlServerStoreRepository = sqlServerStoreRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public async Task<PagedResultDto<GetDataChamCongForViewDto>> GetAll(GetAllDataChamCongsInput input)
        {
            var result = await _sqlServerStoreRepository.SelectDataList<GetDataChamCongForViewDto>(StoreProcedureName.Schema, StoreProcedureName.GetAllChamCongByFilter, input);

            return new PagedResultDto<GetDataChamCongForViewDto>(
                input.TotalCount,
                result
            );
        }

        public async Task<DataChamCongFilter> GetDataChamCongFilter()
        {
            DataChamCongFilter output = new DataChamCongFilter();

            var truongGiaoDichList = await _truongGiaoDichRepository.GetAll()
                .Where(x => x.Code.Equals(TruongGiaoDichConsts.CT) || x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value).ToListAsync();

            output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            output.ViTriCongViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value));

            if (output.Congty != null && output.Congty.Count() > 0)
            {
                var congTy = output.Congty.FirstOrDefault();
                var org = await _organizationUnitRepository.GetAll().FirstOrDefaultAsync(x => x.DisplayName.ToUpper().Equals(congTy.Value.ToUpper()));
                output.CongViecList = await _hoSosAppService.GetAllCongViec(Convert.ToInt32(org.Id));
            }

            return output;
        }

        public async Task<List<OrganizationUnitDto>> GetCongViecByTenCty(string tenCty)
        {
            var output = new List<OrganizationUnitDto>();
            var org = await _organizationUnitRepository.GetAll()
                .WhereIf(!tenCty.IsNullOrEmpty(), x => x.DisplayName.ToUpper().Equals(tenCty.ToUpper()))
                .FirstOrDefaultAsync();
            if (org != null)
                output = await _hoSosAppService.GetAllCongViec(Convert.ToInt32(org.Id));

            return output;
        }

        public async Task<string> GetProcessDateMax()
        {
            var processDate = await _dataChamCongRepository.GetAll().MaxAsync(x => x.ProcessDate);
            return processDate.ToString(AppConsts.DateTimeFormat);
        }

        public async Task<GetDataChamCongForViewDto> GetDataChamCongForView(int id)
        {
            var dataChamCong = await _dataChamCongRepository.GetAsync(id);

            var output = new GetDataChamCongForViewDto { DataChamCong = ObjectMapper.Map<DataChamCongDto>(dataChamCong) };

            return output;
        }

        //[AbpAuthorize(AppPermissions.Pages_DataChamCongs_Edit)]
        public async Task<GetDataChamCongForEditOutput> GetDataChamCongForEdit(EntityDto input)
        {
            var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetDataChamCongForEditOutput { DataChamCong = ObjectMapper.Map<CreateOrEditDataChamCongDto>(dataChamCong) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditDataChamCongDto input)
        {
            if (input.TimeCheckDate.HasValue)
            {
                var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync(x => x.MaChamCong == input.MaChamCong && x.ProcessDate == input.TimeCheckDate.Value.Date);
                if (dataChamCong == null)
                {
                    await Create(input);
                }
                else
                {
                    input.Id = dataChamCong.Id;
                    await Update(input);
                }
            }
        }

        public async Task CreateOrEditMobile(CreateOrEditMobileDataChamCongDto input)
        {

            var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync(x => x.MaChamCong == input.MaChamCong && x.ProcessDate == input.ProcessDate.Date);
            if (dataChamCong == null)
            {
                await CreateMobile(input);

            }
            else
            {
                input.Id = dataChamCong.Id;
                await UpdateMobile(input);
            }

        }

        public async Task<List<DataChamCongDto>> kiemTraCheckTrongNgay(int MaChamCong, DateTime TimeCheckDate)
        {
            try
            {
                var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync(x => x.MaChamCong == MaChamCong && x.ProcessDate == TimeCheckDate.Date);
                if (dataChamCong == null)
                {
                    return null;
                }
                else
                {
                    var id = dataChamCong.Id.ToString();
                    // return id;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                await conn.OpenAsync();
                            }
                            string sql =
                               $"select * from DataChamCongs where DataChamCongs.id = {id}";

                            var result = await conn.QueryAsync<DataChamCongDto>(sql: sql);
                            return result.ToList();

                        }
                    }
                    catch (Exception ex)
                    {
                        // return ex.Message.ToString();
                        throw;
                    }



                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        //[AbpAuthorize(AppPermissions.Pages_DataChamCongs_Create)]
        protected virtual async Task Create(CreateOrEditDataChamCongDto input)
        {
            try
            {
                var workTimeMonday = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_MONDAY));
                var workTimeDaily = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_DAILY));
                /// kêt thúc làm việc buổi sáng: 12:00:00
                var middleStart = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_START));
                /// giờ bắt đầu làm việc buổi chiều: 13:00:00
                var middleEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_END));

                var dataChamCong = ObjectMapper.Map<DataChamCong>(input);
                var checkTime = input.TimeCheckDate.Value.ToString(AppConsts.TimeFormat);

                dataChamCong.ProcessDate = input.TimeCheckDate.Value.Date;
                dataChamCong.CheckTime = checkTime;
                dataChamCong.TimeViolatingRuleFirstDuration = MathTimeViolatingRuleDurationFirst(dataChamCong.ProcessDate, dataChamCong.CheckTime, workTimeMonday, workTimeDaily, middleStart, middleEnd);
                dataChamCong.Status = DataChamCongConsts.PROCESS;
                dataChamCong.CheckIn = checkTime;

                await _dataChamCongRepository.InsertAsync(dataChamCong);
            }
            catch (Exception ex)
            {
                // return ex.Message.ToString();
                throw;
            }

        }

        //[AbpAuthorize(AppPermissions.Pages_DataChamCongs_Edit)]
        protected virtual async Task Update(CreateOrEditDataChamCongDto input)
        {
            var workTimeMonday = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_MONDAY));
            var workTimeDaily = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_DAILY));
            var workTimeEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_END));
            ///workTimeApprove: số giờ tối thiểu làm trong 1 buổi để được chấm công
            var workTimeApprove = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORK_TIME_APPROVE));
            /// kêt thúc làm việc buổi sáng: 12:00:00
            var middleStart = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_START));
            /// giờ bắt đầu làm việc buổi chiều: 13:00:00
            var middleEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_END));

            var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync((int)input.Id);

            var timeCheckList = dataChamCong.CheckTime.Split('~');
            var timeStr = input.TimeCheckDate.Value.ToString(AppConsts.TimeFormat);

            if (timeCheckList == null || !timeCheckList.Any(x => x.Equals(timeStr)))
            {
                var checkTime = input.TimeCheckDate.Value.ToString(AppConsts.TimeFormat);

                dataChamCong.CheckOut = checkTime;
                dataChamCong.CheckTime += $"~{checkTime}";
                dataChamCong.TimeWorkMorningDuration = MathTimeWorkMorningDuration(dataChamCong.CheckTime, dataChamCong.ProcessDate, workTimeMonday, workTimeDaily, workTimeApprove, middleStart, middleEnd);
                dataChamCong.TimeWorkAfternoonDuration = MathTimeWorkAfternoonDuration(dataChamCong.CheckTime, workTimeEnd, workTimeApprove, middleStart, middleEnd);
                dataChamCong.TimeOTDuration = MathTimeOTDurationDaily(dataChamCong.CheckTime, workTimeEnd);
                dataChamCong.TimeViolatingRuleFirstDuration = MathTimeViolatingRuleDurationFirst(dataChamCong.ProcessDate, dataChamCong.CheckTime, workTimeMonday, workTimeDaily, middleStart, middleEnd);
                dataChamCong.TimeViolatingRuleLastDuration = MathTimeViolatingRuleDurationLast(dataChamCong.CheckTime, middleEnd, workTimeEnd);
                dataChamCong.Status = DataChamCongConsts.SUCCESS;
            }
        }

        protected async Task CreateMobile(CreateOrEditMobileDataChamCongDto input)
        {
            try
            {
                var workTimeMonday = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_MONDAY));
                var workTimeDaily = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_DAILY));
                /// kêt thúc làm việc buổi sáng: 12:00:00
                var middleStart = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_START));
                /// giờ bắt đầu làm việc buổi chiều: 13:00:00
                var middleEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_END));

                //var dataChamCong = ObjectMapper.Map<DataChamCong>(input);
                DataChamCong dataChamCong = new DataChamCong();
                dataChamCong.MaChamCong = input.MaChamCong;
                dataChamCong.ProcessDate = input.ProcessDate.Date;
                dataChamCong.CheckTime = input.CheckTime;
                dataChamCong.TimeViolatingRuleFirstDuration = MathTimeViolatingRuleDurationFirst(input.ProcessDate.Date, dataChamCong.CheckTime, workTimeMonday, workTimeDaily, middleStart, middleEnd);
                dataChamCong.Status = DataChamCongConsts.PROCESS;
                dataChamCong.CheckIn = input.CheckTime;

                await _dataChamCongRepository.InsertAsync(dataChamCong);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //[AbpAuthorize(AppPermissions.Pages_DataChamCongs_Edit)]
        protected virtual async Task UpdateMobile(CreateOrEditMobileDataChamCongDto input)
        {
            var workTimeMonday = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_MONDAY));
            var workTimeDaily = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_DAILY));
            var workTimeEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_END));
            ///workTimeApprove: số giờ tối thiểu làm trong 1 buổi để được chấm công
            var workTimeApprove = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORK_TIME_APPROVE));
            /// kêt thúc làm việc buổi sáng: 12:00:00
            var middleStart = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_START));
            /// giờ bắt đầu làm việc buổi chiều: 13:00:00
            var middleEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_END));

            var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync((int)input.Id);

            var timeCheckList = dataChamCong.CheckTime.Split('~');
            var timeStr = input.CheckTime;

            if (timeCheckList == null || !timeCheckList.Any(x => x.Equals(timeStr)))
            {
                var checkTime = input.CheckTime;

                dataChamCong.CheckOut = checkTime;
                dataChamCong.ProcessDate = input.ProcessDate.Date;
                dataChamCong.CheckTime += $"~{checkTime}";
                dataChamCong.TimeWorkMorningDuration = MathTimeWorkMorningDuration(dataChamCong.CheckTime, dataChamCong.ProcessDate, workTimeMonday, workTimeDaily, workTimeApprove, middleStart, middleEnd);
                dataChamCong.TimeWorkAfternoonDuration = MathTimeWorkAfternoonDuration(dataChamCong.CheckTime, workTimeEnd, workTimeApprove, middleStart, middleEnd);
                dataChamCong.TimeOTDuration = MathTimeOTDurationDaily(dataChamCong.CheckTime, workTimeEnd);
                dataChamCong.TimeViolatingRuleFirstDuration = MathTimeViolatingRuleDurationFirst(dataChamCong.ProcessDate, dataChamCong.CheckTime, workTimeMonday, workTimeDaily, middleStart, middleEnd);
                dataChamCong.TimeViolatingRuleLastDuration = MathTimeViolatingRuleDurationLast(dataChamCong.CheckTime, middleEnd, workTimeEnd);
                dataChamCong.Status = DataChamCongConsts.SUCCESS;
            }
        }

        [AbpAuthorize(AppPermissions.Pages_DataChamCongs_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _dataChamCongRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetDataChamCongsToExcel(GetAllDataChamCongsForExcelInput input)
        {
            var dsResult = await _sqlServerStoreRepository.SelectDataSet(StoreProcedureName.Schema, StoreProcedureName.GetDataChamCongByMonth, input);
            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                for (var idx = 0; idx < dsResult.Tables.Count; idx++)
                {
                    dsResult.Tables[idx].TableName = AppConsts.PrefixTableReport + idx;
                }
            }
            return _dataChamCongsExcelExporter.ExportToFile(dsResult, input.ProcessDate);
        }
        public async Task<FileDto> GetDataChamCongsFilterToExcel(GetAllDataChamCongsInput input)
        {
            input.IsExportExcel = true;
            var dsResult = await _sqlServerStoreRepository.SelectDataSet(StoreProcedureName.Schema, StoreProcedureName.GetAllChamCongByFilter, input);
            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                for (var idx = 0; idx < dsResult.Tables.Count; idx++)
                {
                    dsResult.Tables[idx].TableName = AppConsts.PrefixTableReport + idx;
                }
            }
            return _dataChamCongsExcelExporter.ExportToFile(dsResult);
        }

        #region Method Supports

        private double MathTimeViolatingRuleDurationFirst(DateTime processDate, string timeCheck, TruongGiaoDich workTimeMonday, TruongGiaoDich workTimeDaily, TruongGiaoDich middleStart, TruongGiaoDich middleEnd)
        {
            double result = 0;
            var timeCheckList = timeCheck.Split('~')?.Select(x => x.ToTimeSpan());

            if (IsWorkStartMorning(timeCheckList, middleStart))
            {
                switch (processDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        if (workTimeMonday != null && timeCheckList != null && timeCheckList.Count() > 0)
                        {
                            double minutesFirst = (timeCheckList.First() - workTimeMonday.Value.ToTimeSpan()).TotalMinutes;
                            result = minutesFirst > 0 ? minutesFirst : 0;
                        }
                        break;
                    default:
                        if (workTimeDaily != null && timeCheckList != null && timeCheckList.Count() > 0)
                        {
                            double minutesFirst = (timeCheckList.First() - workTimeDaily.Value.ToTimeSpan()).TotalMinutes;
                            result = minutesFirst > 0 ? minutesFirst : 0;
                        }
                        break;
                }
            }
            if (IsWorkStartAfternoon(timeCheckList, middleStart, middleEnd))
            {
                if (middleEnd != null && timeCheckList != null && timeCheckList.Count() > 0)
                {
                    double minutesFirst = (timeCheckList.First() - middleEnd.Value.ToTimeSpan()).TotalMinutes;
                    result = minutesFirst > 0 ? minutesFirst : 0;
                }
            }
            return result;
        }

        private double MathTimeViolatingRuleDurationLast(string timeCheck, TruongGiaoDich middleEnd, TruongGiaoDich workTimeEnd)
        {
            double result = 0;
            var timeCheckList = timeCheck.Split('~')?.Select(x => x.ToTimeSpan());

            if (workTimeEnd != null && timeCheckList != null && timeCheckList.Count() > 1
                && (timeCheckList.Last() - timeCheckList.First()).TotalMinutes > 5
                && IsWorkAfternoon(timeCheckList, middleEnd))
            {
                double minutesLast = (workTimeEnd.Value.ToTimeSpan() - timeCheckList.Last()).TotalMinutes;
                result = minutesLast > 0 ? minutesLast : 0;
            }
            return result;
        }

        private double MathTimeOTDurationDaily(string timeCheck, TruongGiaoDich workTimeEnd)
        {
            double result = 0;
            var timeCheckList = timeCheck.Split('~')?.Select(x => x.ToTimeSpan());

            if (workTimeEnd != null && timeCheckList != null && timeCheckList.Count() > 1
                && (timeCheckList.Last() - timeCheckList.First()).TotalMinutes > 5)
            {
                double minutesLast = (timeCheckList.Last() - workTimeEnd.Value.ToTimeSpan()).TotalMinutes;
                result += minutesLast > 0 ? minutesLast : 0;
            }
            return result;
        }

        private double MathTimeWorkMorningDuration(string timeCheck, DateTime processDate,
            TruongGiaoDich workTimeMonday, TruongGiaoDich workTimeDaily,
            TruongGiaoDich workTimeApprove, TruongGiaoDich middleStart, TruongGiaoDich middleEnd)
        {
            double result = 0;
            var timeCheckList = timeCheck.Split('~')?.Select(x => x.ToTimeSpan());

            if (middleStart != null && middleEnd != null && workTimeApprove != null
                && timeCheckList != null && timeCheckList.Count() > 1
                && (timeCheckList.Last() - timeCheckList.First()).TotalMinutes > 5
                && IsWorkStartMorning(timeCheckList, middleStart))
            {
                if (processDate.DayOfWeek == DayOfWeek.Monday)
                {
                    var workStart = timeCheckList.First() >= workTimeMonday.Value.ToTimeSpan() ? timeCheckList.First() : workTimeMonday.Value.ToTimeSpan();
                    var workEnd = timeCheckList.Last() >= middleStart.Value.ToTimeSpan() ? middleStart.Value.ToTimeSpan() : timeCheckList.Last();
                    var totalMorning = (workEnd - workStart).TotalMinutes;
                    result = totalMorning > 0 && totalMorning > Convert.ToDouble(workTimeApprove.Value) ? totalMorning : 0;
                }
                else
                {
                    var workStart = timeCheckList.First() >= workTimeDaily.Value.ToTimeSpan() ? timeCheckList.First() : workTimeDaily.Value.ToTimeSpan();
                    var workEnd = timeCheckList.Last() >= middleStart.Value.ToTimeSpan() ? middleStart.Value.ToTimeSpan() : timeCheckList.Last();
                    var totalMorning = (workEnd - workStart).TotalMinutes;
                    result = totalMorning > 0 && totalMorning > Convert.ToDouble(workTimeApprove.Value) ? totalMorning : 0;
                }
            }
            return result;
        }

        private double MathTimeWorkAfternoonDuration(string timeCheck, TruongGiaoDich workTimeEnd,
            TruongGiaoDich workTimeApprove, TruongGiaoDich middleStart, TruongGiaoDich middleEnd)
        {
            double result = 0;
            var timeCheckList = timeCheck.Split('~')?.Select(x => x.ToTimeSpan());

            if (middleStart != null && middleEnd != null && workTimeApprove != null && workTimeEnd != null
                && timeCheckList != null && timeCheckList.Count() > 1
                && (timeCheckList.Last() - timeCheckList.First()).TotalMinutes > 5
                && IsWorkAfternoon(timeCheckList, middleEnd))
            {
                var workStart = timeCheckList.First() >= middleEnd.Value.ToTimeSpan() ? timeCheckList.First() : middleEnd.Value.ToTimeSpan();
                var workEnd = timeCheckList.Last() <= workTimeEnd.Value.ToTimeSpan() ? timeCheckList.Last() : workTimeEnd.Value.ToTimeSpan();
                var totalAfternoon = (workEnd - workStart).TotalMinutes;
                result = totalAfternoon > 0 && totalAfternoon > Convert.ToDouble(workTimeApprove.Value) ? totalAfternoon : 0;
            }

            return result;
        }

        private bool IsWorkStartMorning(IEnumerable<TimeSpan> timeCheckList, TruongGiaoDich middleStart)
        {
            return timeCheckList.First() < middleStart.Value.ToTimeSpan();
        }

        private bool IsWorkStartAfternoon(IEnumerable<TimeSpan> timeCheckList, TruongGiaoDich middleStart, TruongGiaoDich middleEnd)
        {
            return timeCheckList.First() > middleStart.Value.ToTimeSpan() && timeCheckList.First() < middleEnd.Value.ToTimeSpan();
        }

        private bool IsWorkAfternoon(IEnumerable<TimeSpan> timeCheckList, TruongGiaoDich middleEnd)
        {
            return timeCheckList.Last() > middleEnd.Value.ToTimeSpan();
        }

        #endregion
    }
}
