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
using Abp.Collections.Extensions;
using Hinnova.Authorization.Users;
using Hinnova.Authorization.Users.Importing;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.FormulaParsing.Utilities;

namespace Hinnova.QLNS
{
    //[AbpAuthorize(AppPermissions.Pages_DataChamCongs)]
    public class DataChamCongsAppService : HinnovaAppServiceBase, IDataChamCongsAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<DataChamCong> _dataChamCongRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IDataChamCongsExcelExporter _dataChamCongsExcelExporter;
        private readonly ISqlServerStoreRepository _sqlServerStoreRepository;
        private readonly IHoSosAppService _hoSosAppService;
        private readonly string _connectionString;
        private readonly IRepository<HoSo> _hoSoRepository;

        public DataChamCongsAppService(
            IRepository<DataChamCong> dataChamCongRepository,
            IRepository<TruongGiaoDich> truongGiaoDichRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IDataChamCongsExcelExporter dataChamCongsExcelExporter,
            IWebHostEnvironment env,
            ISqlServerStoreRepository sqlServerStoreRepository,
            IHoSosAppService hoSosAppService, UserManager userManager, IRepository<HoSo> hoSoRepository)
        {
            _hoSosAppService = hoSosAppService;
            _userManager = userManager;
            _hoSoRepository = hoSoRepository;
            _dataChamCongRepository = dataChamCongRepository;
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _dataChamCongsExcelExporter = dataChamCongsExcelExporter;
            _sqlServerStoreRepository = sqlServerStoreRepository;
            _connectionString = env.GetAppConfiguration().GetConnectionString("Default");
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

            if (output.Congty != null && output.Congty.Any())
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
            try
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
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
            }
        }

        public async Task<List<DataChamCongDto>> GetAllByMaChamCong(int maChamCong)
        {
            try
            {
                var dataChamCong = await _dataChamCongRepository.GetAllListAsync(x => x.MaChamCong == maChamCong && x.ProcessDate.Month == DateTime.UtcNow.Month && x.ProcessDate.Year == DateTime.UtcNow.Year);
                if (dataChamCong == null)
                {
                    throw new Exception("No Found Data");
                }

                var result = dataChamCong.Select(x => new DataChamCongDto()
                {
                    MaChamCong = x.MaChamCong,
                    ProcessDate = x.ProcessDate,
                    CheckTime = x.CheckTime,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    TimeOTDuration = x.TimeOTDuration,
                    Status = x.Status
                });
                return result.OrderByDescending(x => x.ProcessDate).ToList();
            }
            catch (Exception e)
            {
                Logger.Warn(e.Message, e);
                return null;
            }
        }

        public async Task<List<DataChamCongDto>> GetAllByBranchId(string branchId, DateTime dateTo, DateTime dateFrom, int maChamCong, string search, bool flagGlobal)
        {
            if (DateTime.Compare(dateFrom.Date, dateTo.Date) <= 0)
            {
                var conn = new SqlConnection(_connectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DynamicParameters parameters;
                if (!flagGlobal)
                {
                    //return _userManager.Users.ToList()
                    //    .Join(_hoSoRepository.GetAll().ToList(), u => u.EmployeeCode, h => h.MaNhanVien,
                    //        (u, h) => new { u, h })
                    //    .Join(_dataChamCongRepository.GetAll().ToList(), uh => uh.h.MaChamCong, d => d.MaChamCong.ToString(),
                    //        (uh, d) => new { uh, d })
                    //    .Where(x => x.d.ProcessDate >= dateFrom && x.d.ProcessDate <= dateTo && x.uh.u.BRANCH_ID == branchId && x.d.MaChamCong == maChamCong)
                    //    .OrderByDescending(x => x.d.ProcessDate)
                    //    .Select(x => new DataChamCongDto()
                    //    {
                    //        Id = x.d.Id,
                    //        CheckIn = x.d.CheckIn,
                    //        ProcessDate = x.d.ProcessDate,
                    //        CheckTime = x.d.CheckTime,
                    //        CheckOut = x.d.CheckOut,
                    //        MaChamCong = x.d.MaChamCong,
                    //        Status = x.d.Status,
                    //        TimeOTDuration = x.d.TimeOTDuration,
                    //        BranchId = x.uh.u.BRANCH_ID

                    //    }).ToList();
                    parameters = new DynamicParameters();
                    parameters.Add("@dateTo", dateTo.Date);
                    parameters.Add("@dateFrom", dateFrom.Date);
                    parameters.Add("@maChamCong", maChamCong);
                    var result = await conn.QueryAsync<DataChamCongDto>("GetAllByBranchIdAndMaChamCong", parameters, null, null, CommandType.StoredProcedure);
                    return result.ToList();
                }

                parameters = new DynamicParameters();
                parameters.Add("@dateTo", dateTo.Date);
                parameters.Add("@dateFrom", dateFrom.Date);
                parameters.Add("@branchId", branchId);
                parameters.Add("@filterKeyword", search);
                var lstCmBRANCH = await conn.QueryAsync<DataChamCongDto>("ProcGetAllByBranchId", parameters, null, null, CommandType.StoredProcedure);
                return lstCmBRANCH.ToList();
            }
            Logger.Error("DateTo < DateFrom");
            return null;

        }

        public async Task<List<string>> GetAllFullNameByFilterKeyWord(string branchId)
        {
            if (string.IsNullOrEmpty(branchId))
            {
                Logger.Warn("No Found BranchId");
                return null;
            }
            var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            var parameters = new DynamicParameters();
            parameters.Add("@branchId", branchId);
            var result = await conn.QueryAsync<HoSo>("GetAllHoSoByBranchGetChild", parameters, null, null, CommandType.StoredProcedure);
            return result.Select(x => x.HoVaTen).ToList();
            //var resultListFullName = await _hoSoRepository.GetAllListAsync(x => x.HoVaTen.Contains(search));
            //var result = await _userManager.Users.Where(x => x.BRANCH_ID == branchId)
            //return resultListFullName.Select(x => x.HoVaTen).ToList();
        }

        public async Task<List<DataChamCongDto>> kiemTraCheckTrongNgay(int maChamCong, DateTime timeCheckDate)
        {
            try
            {
                List<DataChamCong> dataChamCong = await _dataChamCongRepository.GetAllListAsync(x => x.MaChamCong == maChamCong && x.ProcessDate == timeCheckDate.Date);
                if (dataChamCong == null)
                {
                    throw new Exception("No Found Data");
                }
                return dataChamCong.Select(x => new DataChamCongDto()
                {
                    MaChamCong = x.MaChamCong,
                    ProcessDate = x.ProcessDate,
                    CheckTime = x.CheckTime,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    TimeOTDuration = x.TimeOTDuration,
                    Status = x.Status
                }).ToList();
            }
            catch (Exception e)
            {
                Logger.Warn(e.Message, e);
                return null;
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
                Logger.Error(ex.Message, ex);
            }

        }

        //[AbpAuthorize(AppPermissions.Pages_DataChamCongs_Edit)]
        protected virtual async Task Update(CreateOrEditDataChamCongDto input)
        {
            try
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

                if (!timeCheckList.Any(x => x.Equals(timeStr)))
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
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        public async Task CreateOrEditMobile(int maChamCong)
        {
            try
            {
                DateTime now = DateTime.Now;
                var dataChamCong = await _dataChamCongRepository.FirstOrDefaultAsync(x => x.MaChamCong == maChamCong && x.ProcessDate.Date == now.Date);
                var input = new CreateOrEditMobileDataChamCongDto()
                {
                    CheckTime = now.ToString("HH:mm") + ":00",
                    MaChamCong = maChamCong,
                    ProcessDate = DateTime.Now.Date
                };
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
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
            }
        }
        protected async Task CreateMobile(CreateOrEditMobileDataChamCongDto input)
        {
            try
            {
                // 7h30
                var workTimeMonday = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_MONDAY));
                // 7h45
                var workTimeDaily = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_DAILY));
                /// kêt thúc làm việc buổi sáng: 12:00:00
                var middleStart = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_START));
                /// giờ bắt đầu làm việc buổi chiều: 13:00:00
                var middleEnd = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.MIDDLE_END));

                //var dataChamCong = ObjectMapper.Map<DataChamCong>(input);
                DataChamCong dataChamCong = new DataChamCong
                {
                    MaChamCong = input.MaChamCong,
                    ProcessDate = input.ProcessDate.Date,
                    CheckTime = input.CheckTime,
                    Status = DataChamCongConsts.PROCESS,
                    CheckIn = input.CheckTime
                };
                dataChamCong.TimeViolatingRuleFirstDuration = MathTimeViolatingRuleDurationFirst(input.ProcessDate.Date, dataChamCong.CheckTime, workTimeMonday, workTimeDaily, middleStart, middleEnd);

                await _dataChamCongRepository.InsertAsync(dataChamCong);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }

        }

        //[AbpAuthorize(AppPermissions.Pages_DataChamCongs_Edit)]
        protected virtual async Task UpdateMobile(CreateOrEditMobileDataChamCongDto input)
        {
            try
            {
                // 7h30
                var workTimeMonday = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_MONDAY));
                // 7h45
                var workTimeDaily = await _truongGiaoDichRepository.FirstOrDefaultAsync(x => x.Code.Equals(TruongGiaoDichConsts.WORKTIME) && x.CDName.Equals(DataChamCongConsts.WORKTIME_START_DAILY));
                // 17h30
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

                if (!timeCheckList.Any(x => x.Equals(timeStr)))
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
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        public DateTime getCurrentDateTime() => DateTime.Now;
        //{
        //    return DateTime.Now;
        //}

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
                        if (workTimeMonday != null && timeCheckList.Any())
                        {
                            double minutesFirst = (timeCheckList.First() - workTimeMonday.Value.ToTimeSpan()).TotalMinutes;
                            result = minutesFirst > 0 ? minutesFirst : 0;
                        }
                        break;
                    default:
                        if (workTimeDaily != null && timeCheckList.Any())
                        {
                            double minutesFirst = (timeCheckList.First() - workTimeDaily.Value.ToTimeSpan()).TotalMinutes;
                            result = minutesFirst > 0 ? minutesFirst : 0;
                        }
                        break;
                }
            }
            if (IsWorkStartAfternoon(timeCheckList, middleStart, middleEnd))
            {
                if (middleEnd != null && timeCheckList.Any())
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
