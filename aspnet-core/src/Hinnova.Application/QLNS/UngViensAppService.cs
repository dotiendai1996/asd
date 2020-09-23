

using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Hinnova.QLNSExporting;
using Hinnova.QLNSDtos;
using Hinnova.Dto;
using Abp.Application.Services.Dto;
using Hinnova.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Abp.Dapper.Repositories;
using Microsoft.AspNetCore.Hosting;
using Hinnova.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using Dapper;
using System.IO;
using Abp.UI;

using System.Text;
using GemBox.Spreadsheet;
using Hinnova.Utils.EmailSenders;
using Hinnova.Configuration.Host.Dto;
using System.Net.Mime;
using System.Net.Mail;
using Abp.Organizations;
using Hinnova.QLNS.Dtos;
using Abp.Collections.Extensions;
using Hinnova.Organizations;
using Microsoft.AspNetCore.Routing.Template;
using Hinnova.QLNS.Importing;

namespace Hinnova.QLNS
{
    //[AbpAuthorize(AppPermissions.Pages_UngViens)]
    public class UngViensAppService : HinnovaAppServiceBase, IUngViensAppService
    {

        private readonly IWebHostEnvironment _env;
        private CoreEmailSender CoreEmailSender;
        private readonly IRepository<DangKyKCB> _dangKyKCBRepository;
        private readonly IRepository<LichSuUpload> _lichSuUploadRepository;
        private readonly IRepository<UngVien> _ungVienRepository;
        private readonly IRepository<TruongGiaoDich> _truongGiaoDichRepository;
        private readonly IRepository<TinhThanh> _tinhThanhRepository;
        private readonly IRepository<NoiDaoTao> _noiDaoTaoRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IUngViensExcelExporter _ungViensExcelExporter;
        private readonly IRepository<Template> _templateRepository;
        private readonly IHoSosAppService _hoSosAppService;

        private readonly IConfigEmailsAppService _configEmailsAppService;
        private readonly ILichSuLamViecsAppService _lichSuLamViecsAppService;
        private readonly ILichSuUploadsAppService _lichSuUploadsAppService;
        private readonly IUngVienListExcelDataReader _UngVienListExcelDataReader;
        private readonly string connectionString;
        private readonly string mes;


        public UngViensAppService(IRepository<NoiDaoTao> noiDaoTaoRepository,
            IRepository<LichSuUpload> lichSuUploadRepository,
            IRepository<DangKyKCB> dangKyKCBRepository,

            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IWebHostEnvironment hostingEnvironment,
            IRepository<TinhThanh> tinhThanhRepository,
            IRepository<Template> templateRepository,
            IRepository<TruongGiaoDich> truongGiaoDichRepository,
            IWebHostEnvironment env,
            IRepository<UngVien> ungVienRepository,
            IUngViensExcelExporter ungViensExcelExporter,
            IOrganizationUnitAppService organizationUnitAppService,
            IHoSosAppService hoSosAppService,
            IConfigEmailsAppService configEmailsAppService,
            ILichSuLamViecsAppService lichSuLamViecsAppService,
            ILichSuUploadsAppService lichSuUploadsAppService,
            IUngVienListExcelDataReader UngVienListExcelDataReader)
        {
            _env = hostingEnvironment;
            _ungVienRepository = ungVienRepository;
            _ungViensExcelExporter = ungViensExcelExporter;
            //CoreEmailSender = coreEmailSender;
            _templateRepository = templateRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _truongGiaoDichRepository = truongGiaoDichRepository;
            _noiDaoTaoRepository = noiDaoTaoRepository;
            _tinhThanhRepository = tinhThanhRepository;
            _dangKyKCBRepository = dangKyKCBRepository;
            _lichSuUploadRepository = lichSuUploadRepository;
            _organizationUnitAppService = organizationUnitAppService;
            _hoSosAppService = hoSosAppService;
            _templateRepository = templateRepository;
            _configEmailsAppService = configEmailsAppService;
            _lichSuLamViecsAppService = lichSuLamViecsAppService;
            _lichSuUploadsAppService = lichSuUploadsAppService;
            _UngVienListExcelDataReader = UngVienListExcelDataReader;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }

        public bool CheckCMND(string cmnd)
        {
            //if (cmnd.IsNullOrEmpty())
            //{
            //    throw new UserFriendlyException(L("Số CMND  không được trống"));
            //}

            var x = _ungVienRepository.GetAll().Any(k => k.SoCMND == cmnd && k.IsDeleted == false);
            return x;
        }

        public async Task SendEmailKH(SendTestEmailInput input)

        {

            string date = DateTime.Today.ToString("dd-MM-yyyy");
            EmailInfo emailInfo = new EmailInfo();
            CoreEmailSender = new CoreEmailSender();
            MailAddress to = new MailAddress(input.EmailAddress);
            MailAddress from = new MailAddress(input.mailForm);

            MailMessage message = new MailMessage(from, to);
            message.Subject = input.subject;
            message.Body = input.body;
            message.Dispose();
            if (input.filedinhkem == null)
            {
                emailInfo.dataAttach = null;
                emailInfo.isAttach = false;
            }
            else
            {
                var path = Path.Combine(_env.WebRootPath, date, input.curentTime, input.filedinhkem);
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
                emailInfo.dataAttach = ms;
                emailInfo.isAttach = true;
            }

            emailInfo.nameAttach = input.filedinhkem;
            emailInfo.Subj = input.subject;
            emailInfo.Message = input.body;
            emailInfo.ToEmail = input.EmailAddress;
            emailInfo.smtpAddress = input.diaChiIP;
            emailInfo.portNumber = input.congSMTP;
            emailInfo.enableSSL = false;
            emailInfo.password = input.matKhau;
            emailInfo.emailFrom = input.mailForm;
            emailInfo.displayName = input.tenTruyCap;

            //ms.Position = 0;
            CoreEmailSender.SendEmail(emailInfo);
        }


        public List<UngVienDto> GetAllCMND()
        {
            return _ungVienRepository.GetAll().Where(t => t.IsDeleted == false).Select(t => new UngVienDto { Id = t.Id, SoCMND = t.SoCMND }).ToList();
        }

        public async Task<PagedFilterResultDto<GetUngVienForViewDto>> GetAll(GetAllUngViensInput input)
        {

            var filteredUngViens = _ungVienRepository.GetAll()
                .Where(x => !x.IsDeleted)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.MaUngVien.Contains(input.Filter) || e.HoVaTen.Contains(input.Filter) ||
                         e.GioiTinhCode.Contains(input.Filter) || e.SoCMND.Contains(input.Filter) ||
                         e.DienThoai.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.MaUngVienFilter),
                    e => e.MaUngVien.ToLower() == input.MaUngVienFilter.ToLower().Trim())
                .WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter),
                    e => e.HoVaTen.ToLower() == input.HoVaTenFilter.ToLower().Trim())
                .WhereIf(!string.IsNullOrWhiteSpace(input.ViTriUngTuyenCodeFilter),
                    e => e.ViTriUngTuyenCode.ToLower() == input.ViTriUngTuyenCodeFilter.ToLower().Trim())
                .WhereIf(!string.IsNullOrWhiteSpace(input.KenhTuyenDungCodeFilter),
                    e => e.KenhTuyenDungCode.ToLower() == input.KenhTuyenDungCodeFilter.ToLower().Trim())
                .WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhCodeFilter),
                    e => e.GioiTinhCode.ToLower() == input.GioiTinhCodeFilter.ToLower().Trim())

                .WhereIf(input.StartDateFilter != null && input.isCheckTimeFilter == false, e => e.CreationTime.Date >= ((DateTime)input.StartDateFilter).Date)
                .WhereIf(input.EndDateFilter != null && input.isCheckTimeFilter == false, e => e.CreationTime.Date <= ((DateTime)input.EndDateFilter).Date)
                //.WhereIf(input.StartDateFilter != null && input.EndDateFilter != null && input.isCheckTimeFilter == false && input.StartDateFilter==input.EndDateFilter, e => e.CreationTime < ((DateTime)input.EndDateFilter).AddDays(1) && e.CreationTime > ((DateTime)input.StartDateFilter).AddDays(-1))
                .WhereIf(input.StartDateFilter != null && input.isCheckTimeFilter == true, e => ((DateTime)e.LastModificationTime).Date >= ((DateTime)input.StartDateFilter).Date)
                .WhereIf(input.EndDateFilter != null && input.isCheckTimeFilter == true, e => ((DateTime)e.LastModificationTime).Date <= ((DateTime)input.EndDateFilter))

                //.WhereIf(input.Day1Filter != null , e => e.Day1 >= ((DateTime)input.Day1Filter).Date)
                //.WhereIf(input.Day2Filter != null, e => e.Day2 >= ((DateTime)input.Day2Filter).Date)
                //.WhereIf(input.Day3Filter != null, e => e.Day3 >= ((DateTime)input.Day3Filter).Date)

                //.WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
                //.WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
                /*.WhereIf(input.StartDateFilter!=null, e=> e.LastModificationTime >=input.StartDateFilter)
                .WhereIf(input.EndDateFilter!=null && input.EndDateFilter> input.StartDateFilter, e=> e.LastModificationTime <=input.EndDateFilter)*/
                //.WhereIf(input.StartDateFilter != null && input.isCheckTimeFilter == false, e => e.CreationTime.Date >= ((DateTime)input.StartDateFilter).Date)
                //.WhereIf(input.EndDateFilter != null && input.isCheckTimeFilter == false, e => e.CreationTime.Date <= ((DateTime)input.EndDateFilter).Date)
                //.WhereIf(input.StartDateFilter != null && input.isCheckTimeFilter == true, e => ((DateTime)e.LastModificationTime).Date >= ((DateTime)input.StartDateFilter).Date)
                //.WhereIf(input.EndDateFilter != null && input.isCheckTimeFilter == true, e => ((DateTime)e.LastModificationTime).Date <= ((DateTime)input.EndDateFilter).Date)
                /*.WhereIf((input.StartDateFilter != null|| input.EndDateFilter != null) && input.isCheckTimeFilter==true, e => e.LastModificationTime >= input.StartDateFilter && e.LastModificationTime<= input.EndDateFilter)*/
                //  .WhereIf(!string.IsNullOrWhiteSpace(input.SoCMNDFilter), e => e.SoCMND.ToLower() == input.SoCMNDFilter.ToLower().Trim())
                // .WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoVanHoaFilter), e => e.TrinhDoVanHoa.ToLower() == input.TrinhDoVanHoaFilter.ToLower().Trim())
                //    .WhereIf(input.MinNamTotNghiepFilter != null, e => e.NamTotNghiep >= input.MinNamTotNghiepFilter)
                //.WhereIf(input.MaxNamTotNghiepFilter != null, e => e.NamTotNghiep <= input.MaxNamTotNghiepFilter)
                .WhereIf(!input.TienDoTuyenDungCodeFilter.IsNullOrEmpty(), e => e.TienDoTuyenDungCode.ToLower() == input.TienDoTuyenDungCodeFilter.ToLower().Trim())
                //.WhereIf(!string.IsNullOrWhiteSpace(input.RECORD_STATUSFilter), e => e.RECORD_STATUS.ToLower() == input.RECORD_STATUSFilter.ToLower().Trim())
                //.WhereIf(input.MinMARKER_IDFilter != null, e => e.MARKER_ID >= input.MinMARKER_IDFilter)
                //.WhereIf(input.MaxMARKER_IDFilter != null, e => e.MARKER_ID <= input.MaxMARKER_IDFilter)
                //.WhereIf(!string.IsNullOrWhiteSpace(input.AUTH_STATUSFilter), e => e.AUTH_STATUS.ToLower() == input.AUTH_STATUSFilter.ToLower().Trim())
                //.WhereIf(!string.IsNullOrWhiteSpace(input.DienThoaiFilter), e => e.DienThoai.ToLower() == input.DienThoaiFilter.ToLower().Trim())
                //.WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.ToLower() == input.EmailFilter.ToLower().Trim())
                //.WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter), e => e.DiaChi.ToLower() == input.DiaChiFilter.ToLower().Trim())
                .WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiCodeFilter),
                    e => e.TrangThaiCode.ToLower() == input.TrangThaiCodeFilter.Trim())
                .WhereIf(input.MinDay1Filter != null, e => ((DateTime)e.Day1).Date >= ((DateTime)input.MinDay1Filter).Date)
                .WhereIf(input.MaxDay1Filter != null, e => ((DateTime)e.Day1).Date <= ((DateTime)input.MaxDay1Filter).Date)
                .WhereIf(input.MinDay2Filter != null, e => ((DateTime)e.Day2).Date >= ((DateTime)input.MinDay2Filter).Date)
                .WhereIf(input.MaxDay2Filter != null, e => ((DateTime)e.Day2).Date <= ((DateTime)input.MaxDay2Filter).Date)
                .WhereIf(input.MinDay3Filter != null, e => ((DateTime)e.Day3).Date >= ((DateTime)input.MinDay3Filter).Date)
                .WhereIf(input.MaxDay3Filter != null, e => ((DateTime)e.Day3).Date <= ((DateTime)input.MaxDay3Filter).Date);



            //   .WhereIf(!string.IsNullOrWhiteSpace(input.Time1Filter), e => e.Time1.ToLower() == input.Time1Filter.Trim())
            //  .WhereIf(!string.IsNullOrWhiteSpace(input.Time2Filter), e => e.Time2.ToLower() == input.Time2Filter.Trim())
            // .WhereIf(!string.IsNullOrWhiteSpace(input.Time3Filter), e => e.Time3.ToLower() == input.Time3Filter.Trim())
            //.WhereIf(!string.IsNullOrWhiteSpace(input.NoteFilter), e => e.Note.ToLower() == input.NoteFilter.Trim())
            //   .WhereIf(!string.IsNullOrWhiteSpace(input.TenCTYFilter), e => e.TenCTY.ToLower() == input.TenCTYFilter.Trim());
            var pagedAndFilteredUngViens = filteredUngViens
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);
            var tgd = _truongGiaoDichRepository.GetAll();
            var units = _organizationUnitRepository.GetAll();

            var ungViens = from o in pagedAndFilteredUngViens

                               //join unit in units on o.ViTriUngTuyenCode equals unit.Code into unitjoin1
                               //from joinedtunit1 in unitjoin1.DefaultIfEmpty()
                           join vtut in tgd.Where(x => x.Code == "VTUT") on o.ViTriUngTuyenCode equals vtut.CDName into vtutJoin
                           from joinedvtut in vtutJoin.DefaultIfEmpty()

                           join ktd in tgd.Where(x => x.Code == "KTD") on o.KenhTuyenDungCode equals ktd.CDName into ktdJoin
                           from joinedktd in ktdJoin.DefaultIfEmpty()

                           join gt in tgd.Where(x => x.Code == "GT") on o.GioiTinhCode equals gt.CDName into gtJoin
                           from joinedgt in gtJoin.DefaultIfEmpty()

                           join tthn in tgd.Where(x => x.Code == "TTHN") on o.TinhTrangHonNhanCode equals tthn.CDName into tthnJoin
                           from joinedtthn in tthnJoin.DefaultIfEmpty()

                           join tddt in tgd.Where(x => x.Code == "TDDT") on o.TrinhDoDaoTaoCode equals tddt.CDName into tddtJoin
                           from joinedtddt in tddtJoin.DefaultIfEmpty()

                           join xl in tgd.Where(x => x.Code == "XLHL") on o.XepLoaiCode equals xl.CDName into xlJoin
                           from joinedxl in xlJoin.DefaultIfEmpty()

                           join tt in tgd.Where(x => x.Code == "TRTH") on o.TrangThaiCode equals tt.CDName into ttJoin
                           from joinedtt in ttJoin.DefaultIfEmpty()

                           join tdtd in tgd.Where(x => x.Code == "TDTD") on o.TienDoTuyenDungCode equals tdtd.CDName into tdtdJoin
                           from joinedtdtd in tdtdJoin.DefaultIfEmpty()

                           join ndt in _noiDaoTaoRepository.GetAll() on o.NoiDaoTaoID equals ndt.Id into ndtJoin
                           from joinedndt in ndtJoin.DefaultIfEmpty()

                           join tinhThanh in _tinhThanhRepository.GetAll() on o.TinhThanhID equals tinhThanh.Id into tinhThanhJoin
                           from joinedtinhThanh in tinhThanhJoin.DefaultIfEmpty()



                           select new GetUngVienForViewDto()
                           {
                               UngVien = new UngVienDto
                               {
                                   MaUngVien = o.MaUngVien,
                                   HoVaTen = o.HoVaTen,
                                   //ViTriUngTuyenCode = t.Value,
                                   ViTriUngTuyenCode = o.ViTriUngTuyenCode,
                                   KenhTuyenDungCode = o.KenhTuyenDungCode,
                                   GioiTinhCode = o.GioiTinhCode,
                                   NgaySinh = o.NgaySinh,
                                   CreateTime = o.CreationTime,
                                   LastModificationTime = o.LastModificationTime,
                                   SoCMND = o.SoCMND,
                                   NgayCap = o.NgayCap,
                                   NoiCap = o.NoiCap,
                                   TinhThanhID = o.TinhThanhID,
                                   TinhTrangHonNhanCode = o.TinhTrangHonNhanCode,
                                   TrinhDoDaoTaoCode = o.TrinhDoDaoTaoCode,
                                   TrinhDoVanHoa = o.TrinhDoVanHoa,
                                   NoiDaoTaoID = o.NoiDaoTaoID,
                                   Khoa = o.Khoa,
                                   ChuyenNganh = o.ChuyenNganh,
                                   XepLoaiCode = o.XepLoaiCode,
                                   NamTotNghiep = o.NamTotNghiep,
                                   TrangThaiCode = o.TrangThaiCode,
                                   TienDoTuyenDungCode = o.TienDoTuyenDungCode,
                                   // TepDinhKem = o.TepDinhKem,
                                   //    RECORD_STATUS = o.RECORD_STATUS,
                                   //MARKER_ID = o.MARKER_ID,
                                   //AUTH_STATUS = o.AUTH_STATUS,
                                   //CHECKER_ID = o.CHECKER_ID,
                                   //APPROVE_DT = o.APPROVE_DT,
                                   DienThoai = o.DienThoai,
                                   NVPhuTrach = o.NVPhuTrach,
                                   Email = o.Email,
                                   DiaChi = o.DiaChi,
                                   Day1 = o.Day1,
                                   Day2 = o.Day2,
                                   Day3 = o.Day3,
                                   Time1 = o.Time1,
                                   Time2 = o.Time2,
                                   Time3 = o.Time3,
                                   Note = o.Note,
                                   TenCTY = o.TenCTY,
                                   Id = o.Id
                               },
                               ViTriUngTuyenValue = joinedvtut == null ? "" : joinedvtut.Value.ToString(),
                               KenhTuyenDungValue = joinedktd == null ? "" : joinedktd.Value.ToString(),
                               GioiTinhValue = joinedgt == null ? "" : joinedgt.Value.ToString(),
                               TinhTrangHonNhanValue = joinedtthn == null ? "" : joinedtthn.Value.ToString(),
                               TrinhDoDaoTaoValue = joinedtddt == null ? "" : joinedtddt.Value.ToString(),
                               XepLoaiValue = joinedxl == null ? "" : joinedxl.Value.ToString(),
                               TrangThaiValue = joinedtt == null ? "" : joinedtt.Value.ToString(),
                               TienDoTuyenDungValue = joinedtdtd == null ? "" : joinedtdtd.Value.ToString(),
                               NoiDaoTaoValue = joinedndt == null ? "" : joinedndt.TenNoiDaoTao.ToString(),
                               TinhThanhValue = joinedtinhThanh == null ? "" : joinedtinhThanh.TenTinhThanh.ToString()
                           };

            var totalCount = await filteredUngViens.CountAsync();
            return new PagedFilterResultDto<GetUngVienForViewDto>(
                totalCount,
                await ungViens.ToListAsync()
            );

        }

        public async Task<GetUngVienForEditOutput> GetListItemSearch()
        {
            var truongGiaoDichList = await _truongGiaoDichRepository.GetAll().ToListAsync();
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            var noiDaoTaoList = _noiDaoTaoRepository.GetAll().ToList();
            var tinhThanhList = _tinhThanhRepository.GetAll().ToList();
            var noiKhamBenhList = _dangKyKCBRepository.GetAll().ToList();
            var templateList = _templateRepository.GetAll().ToList();
            var output = new GetUngVienForEditOutput();

            output.NoiDaoTaoList = ObjectMapper.Map<List<NoiDaoTaoDto>>(noiDaoTaoList.OrderBy(x => x.TenNoiDaoTao));
            output.TinhThanhList = ObjectMapper.Map<List<TinhThanhDto>>(tinhThanhList.OrderBy(x => x.TenTinhThanh));
            output.NoiKhamBenhList = ObjectMapper.Map<List<DangKyKCBDto>>(noiKhamBenhList.OrderBy(x => x.TenNoiKCB));
            output.TemplateList = ObjectMapper.Map<List<TemplateDto>>(templateList.OrderBy(x => x.TenTemplate));
            output.ViTriCongViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value));
            output.TienDoTuyenDung = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TDTD)).OrderBy(x => x.Value));
            output.TrangThai = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TRTH)).OrderBy(x => x.Value));
            output.KenhTuyenDung = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.KTD)).OrderBy(x => x.Value));
            output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            output.GioiTinh = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.GT)).OrderBy(x => x.Value));
            output.TinhTrangHonNhan = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TTHN)).OrderBy(x => x.Value));
            output.TrinhDoDaoTao = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TDDT)).OrderBy(x => x.Value));
            output.XepLoaiHocLuc = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.XLHL)).OrderBy(x => x.Value));
            output.TinhTrangNhanSu = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TTNS)).OrderBy(x => x.Value));
            output.DefaultCbbOption = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.SetDefault).OrderBy(x => x.Value));
            return output;
        }

        public async Task<List<UngVienDto>> GetAllUngVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }
                var result = await conn.QueryAsync<UngVienDto>(sql: "SELECT * FROM  UngVien WHERE IsDeleted = 0");
                return result.ToList();
            }
        }


        public async Task<GetUngVienForViewDto> GetUngVienForView(int id)
        {
            var ungVien = await _ungVienRepository.GetAsync(id);

            var output = new GetUngVienForViewDto { UngVien = ObjectMapper.Map<UngVienDto>(ungVien) };

            return output;
        }

        //[AbpAuthorize(AppPermissions.Pages_UngViens_Edit)]
        public async Task<GetUngVienForEditOutput> GetUngVienForEdit(EntityDto input)
        {
            var ungVien = await _ungVienRepository.FirstOrDefaultAsync(input.Id);
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            var noiDaoTaoList = _noiDaoTaoRepository.GetAll().ToList();
            var tinhThanhList = _tinhThanhRepository.GetAll().ToList();
            var truongGiaoDichList = _truongGiaoDichRepository.GetAll().ToList();

            var output = new GetUngVienForEditOutput();
            if (ungVien != null)
            {
                var lichsuLamViec = await _lichSuLamViecsAppService.GetLichSuLamViecByUngVienAsync(ungVien.Id);
                if (ungVien.TenCTY != null)
                {
                    var templateList = _templateRepository.GetAll()
                        .Where(x => x.MaTemplate.ToUpper().Equals(ungVien.TenCTY.ToUpper()))
                        .ToList();

                    output.ConfigEmail = await _configEmailsAppService.GetConfigEmailByTenCtyForView(ungVien.TenCTY);
                    output.TemplateList = ObjectMapper.Map<List<TemplateDto>>(templateList);
                }
                else
                {
                    output.TemplateList = null;
                    output.ConfigEmail = null;
                }
                output.CongViecList = await _hoSosAppService.GetAllCongViec(ungVien.DonViCongTacID ?? 0);
                output.LichSuUploadList = await _lichSuUploadsAppService.GetListLichSuUploadDto("UV", ungVien.Id.ToString());
                output.LichSuLamViecList = lichsuLamViec.Where(x => x.ChuDe == null).OrderBy(x => x.Id).ToList();

            }

            output.OrganizationUnitList = orgList;
            output.UngVien = ObjectMapper.Map<CreateOrEditUngVienDto>(ungVien);
            output.NoiDaoTaoList = ObjectMapper.Map<List<NoiDaoTaoDto>>(noiDaoTaoList.OrderBy(x => x.TenNoiDaoTao));
            output.TinhThanhList = ObjectMapper.Map<List<TinhThanhDto>>(tinhThanhList.OrderBy(x => x.TenTinhThanh));

            output.ViTriCongViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value));
            output.TienDoTuyenDung = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TDTD)).OrderBy(x => x.Value));
            output.TrangThai = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TRTH)).OrderBy(x => x.Value));
            output.KenhTuyenDung = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.KTD)).OrderBy(x => x.Value));
            output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            output.GioiTinh = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.GT)).OrderBy(x => x.Value));
            output.TinhTrangHonNhan = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TTHN)).OrderBy(x => x.Value));
            output.TrinhDoDaoTao = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TDDT)).OrderBy(x => x.Value));
            output.XepLoaiHocLuc = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.XLHL)).OrderBy(x => x.Value));
            output.DefaultCbbOption = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.SetDefault).OrderBy(x => x.Value));

            return output;
        }

        /// <summary>
        /// chinhqn@gsoft.com.vn
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUngVienForEditOutput> GetUngVienForUpdate(EntityDto input)
        {
            var ungVien = await _ungVienRepository.FirstOrDefaultAsync(input.Id);
            var orgList = await _organizationUnitAppService.GetOrganizationUnits();
            var noiDaoTaoList = _noiDaoTaoRepository.GetAll().ToList();
            var tinhThanhList = _tinhThanhRepository.GetAll().ToList();
            var truongGiaoDichList = _truongGiaoDichRepository.GetAll().ToList();

            var output = new GetUngVienForEditOutput();
            if (ungVien != null)
            {
                var lichsuLamViec = await _lichSuLamViecsAppService.GetLichSuLamViecByUngVienAsync(ungVien.Id);
                var templateList = _templateRepository.GetAll()
                    .Where(x => x.MaTemplate.ToUpper().Equals(ungVien.TenCTY.ToUpper()))
                    .ToList();

                output.ConfigEmail = await _configEmailsAppService.GetConfigEmailByTenCtyForView(ungVien.TenCTY);
                output.CongViecList = await _hoSosAppService.GetAllCongViec(ungVien.DonViCongTacID ?? 0);
                output.LichSuLamViecList = lichsuLamViec.Where(x => x.ChuDe != null).OrderBy(x => x.Id).ToList();
                output.TemplateList = ObjectMapper.Map<List<TemplateDto>>(templateList);
            }

            output.OrganizationUnitList = orgList;
            output.UngVien = ObjectMapper.Map<CreateOrEditUngVienDto>(ungVien);
            output.NoiDaoTaoList = ObjectMapper.Map<List<NoiDaoTaoDto>>(noiDaoTaoList.OrderBy(x => x.TenNoiDaoTao));
            output.TinhThanhList = ObjectMapper.Map<List<TinhThanhDto>>(tinhThanhList.OrderBy(x => x.TenTinhThanh));

            output.ViTriCongViec = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.VTUT)).OrderBy(x => x.Value));
            output.TienDoTuyenDung = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TDTD)).OrderBy(x => x.Value));
            output.TrangThai = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TRTH)).OrderBy(x => x.Value));
            output.KenhTuyenDung = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.KTD)).OrderBy(x => x.Value));
            output.Congty = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.CT)).OrderBy(x => x.Value));
            output.GioiTinh = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.GT)).OrderBy(x => x.Value));
            output.TinhTrangHonNhan = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TTHN)).OrderBy(x => x.Value));
            output.TrinhDoDaoTao = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.TDDT)).OrderBy(x => x.Value));
            output.XepLoaiHocLuc = ObjectMapper.Map<List<TruongGiaoDichDto>>(truongGiaoDichList.Where(x => x.Code.Equals(TruongGiaoDichConsts.XLHL)).OrderBy(x => x.Value));

            return output;
        }



        public async Task<int> CreateOrEdit(CreateOrEditUngVienInput ungVienInput)

        {

            var input = ungVienInput.UngVien;
            DateTime Ngay_Sinh;

            if (input.Id == null)
            {
                if (input.SoCMND.IsNullOrEmpty())
                {
                    input.SoCMND = null;
                }
                else
                {
                    if (CheckCMND(input.SoCMND))
                    {
                        throw new UserFriendlyException("Số CMND đã bị trùng");

                    }
                }
                Ngay_Sinh = Convert.ToDateTime(input.NgaySinh);
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(Ngay_Sinh.ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                if (age < 18)
                {
                    throw new UserFriendlyException("Chưa đủ 18 tuổi ");
                }


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        await conn.OpenAsync();
                    var tableName = "UngVien";
                    var tenCTY = input.TenCTY;
                    var result = await conn.QueryAsync<string>(sql: "dbo.SYS_CodeMasters_Gen ", param: new { tableName, tenCTY }, commandType: CommandType.StoredProcedure);
                    input.MaUngVien = result.ToList().First();
                    var id = await Create(input);
                    var lichSu = ungVienInput.LichSuUpLoad;
                    if (lichSu.Count > 0)
                    {
                        for (int i = 0; i < lichSu.Count; i++)

                        {
                            var lichSuUpload = new LichSuUpload();

                            lichSuUpload.TenFile = lichSu[i].TenFile;
                            lichSuUpload.TieuDe = lichSu[i].TieuDe;
                            lichSuUpload.DungLuong = lichSu[i].DungLuong;
                            lichSuUpload.Type = "UV";
                            lichSuUpload.TypeID = id.ToString();
                            await _lichSuUploadRepository.InsertAsync(lichSuUpload);
                        }
                    }

                    return id;

                }
            }
            else
            {
                await Update(input);
            }

            return 0;
        }

        //[AbpAuthorize(AppPermissions.Pages_UngViens_Create)]
        protected virtual async Task<int> Create(CreateOrEditUngVienDto input)
        {
            var ungVien = ObjectMapper.Map<UngVien>(input);

            return await _ungVienRepository.InsertAndGetIdAsync(ungVien);
        }

        //[AbpAuthorize(AppPermissions.Pages_UngViens_Edit)]
        protected virtual async Task Update(CreateOrEditUngVienDto input)
        {
            var ungVien = await _ungVienRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, ungVien);
        }

        //[AbpAuthorize(AppPermissions.Pages_UngViens_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _ungVienRepository.DeleteAsync(input.Id);
        }

        //public async Task<FileDto> GetUngViensToExcel(GetAllUngViensForExcelInput input)
        //{

        //    var filteredUngViens = _ungVienRepository.GetAll()
        //             .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MaUngVien.Contains(input.Filter) || e.HoVaTen.Contains(input.Filter) || e.ViTriUngTuyenCode.Contains(input.Filter) || e.KenhTuyenDungCode.Contains(input.Filter) || e.GioiTinhCode.Contains(input.Filter) || e.SoCMND.Contains(input.Filter) || e.NoiCap.Contains(input.Filter) || e.TinhTrangHonNhanCode.Contains(input.Filter) || e.TrinhDoDaoTaoCode.Contains(input.Filter) || e.TrinhDoVanHoa.Contains(input.Filter) || e.Khoa.Contains(input.Filter) || e.ChuyenNganh.Contains(input.Filter) || e.XepLoaiCode.Contains(input.Filter) || e.TrangThaiCode.Contains(input.Filter) || e.TienDoTuyenDungCode.Contains(input.Filter) || e.TepDinhKem.Contains(input.Filter) || e.RECORD_STATUS.Contains(input.Filter) || e.AUTH_STATUS.Contains(input.Filter) || e.DienThoai.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.DiaChi.Contains(input.Filter))
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.MaUngVienFilter), e => e.MaUngVien.ToLower() == input.MaUngVienFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.HoVaTenFilter), e => e.HoVaTen.ToLower() == input.HoVaTenFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.ViTriUngTuyenCodeFilter), e => e.ViTriUngTuyenCode.ToLower() == input.ViTriUngTuyenCodeFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.KenhTuyenDungCodeFilter), e => e.KenhTuyenDungCode.ToLower() == input.KenhTuyenDungCodeFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.GioiTinhCodeFilter), e => e.GioiTinhCode.ToLower() == input.GioiTinhCodeFilter.ToLower().Trim())
        //                .WhereIf(input.MinNgaySinhFilter != null, e => e.NgaySinh >= input.MinNgaySinhFilter)
        //                .WhereIf(input.MaxNgaySinhFilter != null, e => e.NgaySinh <= input.MaxNgaySinhFilter)
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.SoCMNDFilter), e => e.SoCMND.ToLower() == input.SoCMNDFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.TrinhDoVanHoaFilter), e => e.TrinhDoVanHoa.ToLower() == input.TrinhDoVanHoaFilter.ToLower().Trim())
        //                .WhereIf(input.MinNamTotNghiepFilter != null, e => e.NamTotNghiep >= input.MinNamTotNghiepFilter)
        //                .WhereIf(input.MaxNamTotNghiepFilter != null, e => e.NamTotNghiep <= input.MaxNamTotNghiepFilter)
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.TienDoTuyenDungCodeFilter), e => e.TienDoTuyenDungCode.ToLower() == input.TienDoTuyenDungCodeFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.RECORD_STATUSFilter), e => e.RECORD_STATUS.ToLower() == input.RECORD_STATUSFilter.ToLower().Trim())
        //                .WhereIf(input.MinMARKER_IDFilter != null, e => e.MARKER_ID >= input.MinMARKER_IDFilter)
        //                .WhereIf(input.MaxMARKER_IDFilter != null, e => e.MARKER_ID <= input.MaxMARKER_IDFilter)
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.AUTH_STATUSFilter), e => e.AUTH_STATUS.ToLower() == input.AUTH_STATUSFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.DienThoaiFilter), e => e.DienThoai.ToLower() == input.DienThoaiFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.ToLower() == input.EmailFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.DiaChiFilter), e => e.DiaChi.ToLower() == input.DiaChiFilter.ToLower().Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.TrangThaiCodeFilter), e => e.TrangThaiCode.ToLower() == input.TrangThaiCodeFilter.Trim())
        //                .WhereIf(input.MinDay1Filter != null, e => e.Day1 >= input.MinDay1Filter)
        //                .WhereIf(input.MaxDay1Filter != null, e => e.Day1 <= input.MaxDay1Filter)
        //                   .WhereIf(input.MinDay2Filter != null, e => e.Day2 >= input.MinDay2Filter)
        //                .WhereIf(input.MaxDay2Filter != null, e => e.Day2 <= input.MaxDay2Filter)
        //                   .WhereIf(input.MinDay3Filter != null, e => e.Day3 >= input.MinDay3Filter)
        //                .WhereIf(input.MaxDay3Filter != null, e => e.Day3 <= input.MaxDay3Filter)
        //                  .WhereIf(!string.IsNullOrWhiteSpace(input.Time1Filter), e => e.Time1.ToLower() == input.Time1Filter.Trim())
        //                 .WhereIf(!string.IsNullOrWhiteSpace(input.Time2Filter), e => e.Time2.ToLower() == input.Time2Filter.Trim())
        //                .WhereIf(!string.IsNullOrWhiteSpace(input.Time3Filter), e => e.Time3.ToLower() == input.Time3Filter.Trim())
        //               .WhereIf(!string.IsNullOrWhiteSpace(input.NoteFilter), e => e.Note.ToLower() == input.NoteFilter.Trim());

        //    var tgd = _truongGiaoDichRepository.GetAll();

        //    var query = (from o in filteredUngViens
        //                 join vtut in tgd.Where(x => x.Code == "VTUT")
        //                   on o.ViTriUngTuyenCode equals vtut.CDName into vtutJoin
        //                 from joinedvtut in vtutJoin.DefaultIfEmpty()

        //                 join ktd in tgd.Where(x => x.Code == "KTD") on o.KenhTuyenDungCode equals ktd.CDName into ktdJoin
        //                 from joinedktd in ktdJoin.DefaultIfEmpty()

        //                 join gt in tgd.Where(x => x.Code == "GT") on o.GioiTinhCode equals gt.CDName into gtJoin
        //                 from joinedgt in gtJoin.DefaultIfEmpty()

        //                 join tthn in tgd.Where(x => x.Code == "TTHN") on o.TinhTrangHonNhanCode equals tthn.CDName into tthnJoin
        //                 from joinedtthn in tthnJoin.DefaultIfEmpty()

        //                 join tddt in tgd.Where(x => x.Code == "TDDT") on o.TrinhDoDaoTaoCode equals tddt.CDName into tddtJoin
        //                 from joinedtddt in tddtJoin.DefaultIfEmpty()

        //                 join xl in tgd.Where(x => x.Code == "XLHL") on o.XepLoaiCode equals xl.CDName into xlJoin
        //                 from joinedxl in xlJoin.DefaultIfEmpty()

        //                 join tt in tgd.Where(x => x.Code == "TRTH") on o.TrangThaiCode equals tt.CDName into ttJoin
        //                 from joinedtt in ttJoin.DefaultIfEmpty()

        //                 join tdtd in tgd.Where(x => x.Code == "TDTD") on o.TienDoTuyenDungCode equals tdtd.CDName into tdtdJoin
        //                 from joinedtdtd in tdtdJoin.DefaultIfEmpty()

        //                 join ndt in _noiDaoTaoRepository.GetAll() on o.NoiDaoTaoID equals ndt.Id into ndtJoin
        //                 from joinedndt in ndtJoin.DefaultIfEmpty()

        //                 join tinhThanh in _tinhThanhRepository.GetAll() on o.TinhThanhID equals tinhThanh.Id into tinhThanhJoin
        //                 from joinedtinhThanh in tinhThanhJoin.DefaultIfEmpty()

        //                 select new GetUngVienForViewDto()
        //                 {
        //                     UngVien = new UngVienDto
        //                     {
        //                         MaUngVien = o.MaUngVien,
        //                         HoVaTen = o.HoVaTen,
        //                         ViTriUngTuyenCode = o.ViTriUngTuyenCode,
        //                         KenhTuyenDungCode = o.KenhTuyenDungCode,
        //                         GioiTinhCode = o.GioiTinhCode,
        //                         NgaySinh = o.NgaySinh,
        //                         SoCMND = o.SoCMND,
        //                         NgayCap = o.NgayCap,
        //                         NoiCap = o.NoiCap,
        //                         TinhThanhID = o.TinhThanhID,
        //                         TinhTrangHonNhanCode = o.TinhTrangHonNhanCode,
        //                         TrinhDoDaoTaoCode = o.TrinhDoDaoTaoCode,
        //                         TrinhDoVanHoa = o.TrinhDoVanHoa,
        //                         NoiDaoTaoID = o.NoiDaoTaoID,
        //                         Khoa = o.Khoa,
        //                         ChuyenNganh = o.ChuyenNganh,
        //                         XepLoaiCode = o.XepLoaiCode,
        //                         NamTotNghiep = o.NamTotNghiep,
        //                         TrangThaiCode = o.TrangThaiCode,
        //                         TienDoTuyenDungCode = o.TienDoTuyenDungCode,
        //                         TepDinhKem = o.TepDinhKem,
        //                         RECORD_STATUS = o.RECORD_STATUS,
        //                         MARKER_ID = o.MARKER_ID,
        //                         AUTH_STATUS = o.AUTH_STATUS,
        //                         CHECKER_ID = o.CHECKER_ID,
        //                         APPROVE_DT = o.APPROVE_DT,
        //                         DienThoai = o.DienThoai,
        //                         Email = o.Email,
        //                         DiaChi = o.DiaChi,
        //                         Day1 = o.Day1,
        //                         Day2 = o.Day2,
        //                         Day3 = o.Day3,
        //                         Time1 = o.Time1,
        //                         Time2 = o.Time2,
        //                         Time3 = o.Time3,
        //                         Note = o.Note,
        //                         Id = o.Id
        //                     },
        //                     ViTriUngTuyenValue = joinedvtut == null ? "" : joinedvtut.Value.ToString(),
        //                     KenhTuyenDungValue = joinedktd == null ? "" : joinedktd.Value.ToString(),
        //                     GioiTinhValue = joinedgt == null ? "" : joinedgt.Value.ToString(),
        //                     TinhTrangHonNhanValue = joinedtthn == null ? "" : joinedtthn.Value.ToString(),
        //                     TrinhDoDaoTaoValue = joinedtddt == null ? "" : joinedtddt.Value.ToString(),
        //                     XepLoaiValue = joinedxl == null ? "" : joinedxl.Value.ToString(),
        //                     TrangThaiValue = joinedtt == null ? "" : joinedtt.Value.ToString(),
        //                     TienDoTuyenDungValue = joinedtdtd == null ? "" : joinedtdtd.Value.ToString(),
        //                     NoiDaoTaoValue = joinedndt == null ? "" : joinedndt.TenNoiDaoTao.ToString(),
        //                     TinhThanhValue = joinedtinhThanh == null ? "" : joinedtinhThanh.TenTinhThanh.ToString()
        //                 });


        //    var ungVienListDtos = await query.ToListAsync();

        //    return _ungViensExcelExporter.ExportToFile(ungVienListDtos);

        //}

        public async Task<int> GetMaTinhThanh(string tenTP)
        {

            var TP = await _tinhThanhRepository.FirstOrDefaultAsync(x => x.TenTinhThanh == tenTP);
            return TP.Id;
        }
        public async Task<int> GetMaNoiDaotao(string tenNDT)
        {
            var NDT = await _noiDaoTaoRepository.FirstOrDefaultAsync(x => x.TenNoiDaoTao.Contains(tenNDT));
            if (NDT == null)
            {
                return 926;
            }
            return NDT.Id;
        }


        public async Task<string> importToExcel(string currentTime, string path)
        {
            try
            {

                string date = DateTime.Today.ToString("dd-MM-yyyy");
                SpreadsheetInfo.SetLicense("ELAP-G41W-CZA2-XNNC");
                var pathExcelImport = Path.Combine(_env.WebRootPath, date, currentTime, path);
                var excelImportByteArray = await File.ReadAllBytesAsync(pathExcelImport);

                var TGD = _truongGiaoDichRepository.GetAll();
                var dvct = _organizationUnitRepository.GetAll();
                var dataImport = _UngVienListExcelDataReader.GetUngVienFromExcel(excelImportByteArray);

                foreach (var data in dataImport)
                {
                    var ungVien = ObjectMapper.Map<CreateOrEditUngVienDto>(data);
                    ungVien.ViTriUngTuyenCode = dvct.FirstOrDefault(x => x.DisplayName.Equals(data.ViTriUngTuyenCode))?.Code;
                    ungVien.KenhTuyenDungCode = TGD.FirstOrDefault(x => x.Code == "KTD" && x.Value.Equals(data.KenhTuyenDungCode))?.CDName;
                    ungVien.GioiTinhCode = TGD.FirstOrDefault(x => x.Code == "GT" && x.Value.Equals(data.GioiTinhCode))?.CDName;
                    if (!ungVien.SoCMND.IsNullOrWhiteSpace() && CheckCMND(ungVien.SoCMND))
                    {
                        throw new UserFriendlyException(L("Số CMND đã bị trùng"));
                    }
                    ungVien.TinhThanhID = await GetMaTinhThanh(data.TinhThanhName);
                    ungVien.TinhTrangHonNhanCode = TGD.FirstOrDefault(x => x.Code == "TTHN" && x.Value.Equals(data.TinhTrangHonNhanCode))?.CDName;
                    ungVien.TrinhDoDaoTaoCode = TGD.FirstOrDefault(x => x.Code == "TDDT" && x.Value.Equals(data.TrinhDoDaoTaoCode))?.CDName;
                    ungVien.NoiDaoTaoID = await GetMaNoiDaotao(data.NoiDaoTaoName);
                    ungVien.XepLoaiCode = TGD.FirstOrDefault(x => x.Code == "XLHL" && x.Value.Equals(data.XepLoaiCode))?.CDName;
                    ungVien.TienDoTuyenDungCode = TGD.FirstOrDefault(x => x.Code == "TDTD" && x.Value.Equals(data.TienDoTuyenDungCode))?.CDName;
                    ungVien.TrangThaiCode = TGD.FirstOrDefault(x => x.Code == "TRTH" && x.Value.Equals(data.TrangThaiCode))?.CDName;
                    CreateOrEditUngVienInput createOrEditUngVienInput = new CreateOrEditUngVienInput();
                    createOrEditUngVienInput.UngVien = ungVien;
                    await CreateOrEdit(createOrEditUngVienInput);
                }
                return mes;
            }

            catch (Exception ex)
            {
                Logger.Error(ex.StackTrace);
                return "";
            }
        }
        //public async Task<string> importToExcel(string currentTime, string path)
        //{
        //    //var ungvien = new UngVien();
        //    var hoten = "";
        //    try
        //    {

        //        string date = DateTime.Today.ToString("dd-MM-yyyy");
        //        SpreadsheetInfo.SetLicense("ELAP-G41W-CZA2-XNNC");
        //        var path1 = Path.Combine(_env.WebRootPath + "\\" + date + "\\" + currentTime + "\\" + path);
        //        var workbook = ExcelFile.Load(path1);

        //        var dataTable = new DataTable();

        //        dataTable.Columns.Add("Mã ứng viên", typeof(string));
        //        dataTable.Columns.Add("Họ và tên", typeof(string));
        //        dataTable.Columns.Add("Vị trí ứng tuyển", typeof(string));
        //        dataTable.Columns.Add("Kênh tuyển dụng", typeof(string));
        //        dataTable.Columns.Add("Giới tính", typeof(string));

        //        dataTable.Columns.Add("Ngày sinh", typeof(string));
        //        dataTable.Columns.Add("Số CMND", typeof(string));
        //        dataTable.Columns.Add("Ngày cấp", typeof(string));
        //        dataTable.Columns.Add("Nơi cấp", typeof(string));
        //        dataTable.Columns.Add("Tỉnh/Thành phố", typeof(string));

        //        dataTable.Columns.Add("Tình trạng hôn nhân", typeof(string));
        //        dataTable.Columns.Add("Trình độ đào tạo", typeof(string));
        //        dataTable.Columns.Add("Trình độ văn hóa", typeof(string));
        //        dataTable.Columns.Add("Nơi đào tạo", typeof(string));
        //        dataTable.Columns.Add("Khoa", typeof(string));

        //        dataTable.Columns.Add("Chuyên ngành", typeof(string));
        //        dataTable.Columns.Add("Xếp loại", typeof(string));
        //        dataTable.Columns.Add("Năm tốt nghiệp", typeof(string));
        //        dataTable.Columns.Add("Trạng thái", typeof(string));
        //        dataTable.Columns.Add("Tiến độ tuyển dụng", typeof(string));

        //        dataTable.Columns.Add("Tệp đính kèm", typeof(string));
        //        dataTable.Columns.Add("Record status", typeof(string));
        //        dataTable.Columns.Add("Marker id", typeof(string));
        //        dataTable.Columns.Add("Auth status", typeof(string));
        //        dataTable.Columns.Add("Checker id", typeof(string));

        //        dataTable.Columns.Add("Approve dt", typeof(string));
        //        dataTable.Columns.Add("Điện thoại", typeof(string));
        //        dataTable.Columns.Add("E-mail", typeof(string));
        //        dataTable.Columns.Add("Địa chỉ", typeof(string));
        //        dataTable.Columns.Add("Time1", typeof(string));
        //        dataTable.Columns.Add("Day1", typeof(string));
        //        dataTable.Columns.Add("Time2", typeof(string));

        //        dataTable.Columns.Add("Day2", typeof(string));
        //        dataTable.Columns.Add("Time3", typeof(string));
        //        dataTable.Columns.Add("Day3", typeof(string));
        //        dataTable.Columns.Add("Note", typeof(string));
        //        dataTable.Columns.Add("Tên công ty", typeof(string));

        //        // Select the first worksheet from the file.
        //        var worksheet = workbook.Worksheets[0];
        //        var options = new ExtractToDataTableOptions(2, 0, 10000);
        //        options.ExtractDataOptions = ExtractDataOptions.StopAtFirstEmptyRow;
        //        options.ExcelCellToDataTableCellConverting += (sender, e) =>
        //        {
        //            if (!e.IsDataTableValueValid)
        //            {

        //                e.DataTableValue = e.ExcelCell.Value == null ? null : e.ExcelCell.Value.ToString();
        //                e.Action = ExtractDataEventAction.Continue;
        //            }
        //        };
        //        worksheet.ExtractToDataTable(dataTable, options);

        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            string NS, NC, NTN, AP, TP, NoiDT, Time1, Time2, Time3, ViTri, KenhTD, GioiTinh;
        //            DateTime Ngay_Sinh, Ngay_Cap, NamTN, APPROVEDT, Day1, Day2, Day3;
        //            var ungVien = new CreateOrEditUngVienDto();
        //            var TGD = _truongGiaoDichRepository.GetAll();
        //            var dvct = _organizationUnitRepository.GetAll();

        //            if (row[1].ToString().IsNullOrEmpty())
        //            {
        //                throw new UserFriendlyException(L(name: "Họ tên không được để trống"));
        //            }
        //            else
        //            {

        //                ungVien.HoVaTen = row[1].ToString();
        //            }

        //            hoten = ungVien.HoVaTen;

        //            if (row[2].ToString() != null)
        //            {
        //                var customerQuery = from tgd in dvct
        //                                    where tgd.DisplayName == row[2].ToString()
        //                                    select tgd.Code;

        //                foreach (var Customer in customerQuery)
        //                {


        //                    ungVien.ViTriUngTuyenCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.ViTriUngTuyenCode = null;
        //            }
        //            KenhTD = row[3].ToString();
        //            if (KenhTD != null)
        //            {

        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "KTD" && tgd.Value == KenhTD
        //                                    select tgd.CDName;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.KenhTuyenDungCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.KenhTuyenDungCode = null;
        //            }
        //            GioiTinh = row[4].ToString();
        //            if (GioiTinh != null)

        //            {
        //                //  ungVien.GioiTinhCode = TGD.FirstOrDefault(x => x.Code == "GT" && x.Value == GioiTinh).CDName;
        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "GT" && tgd.Value == GioiTinh
        //                                    select tgd.CDName;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.GioiTinhCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.GioiTinhCode = null;
        //            }

        //            ungVien.NgaySinh = row[5].ToString().ToDatetimeFormat();

        //            var cmnd = row[6].ToString();

        //            if (!cmnd.IsNullOrWhiteSpace() && CheckCMND(cmnd))
        //            {
        //                throw new UserFriendlyException(L("Số CMND đã bị trùng"));
        //            }
        //            else
        //            {
        //                ungVien.SoCMND = row[6].ToString();
        //            }

        //            ungVien.NgayCap = row[7].ToString().ToDatetimeFormat();

        //            ungVien.NoiCap = row[8].ToString();
        //            TP = row[9].ToString();
        //            if (!TP.IsNullOrEmpty())
        //            {
        //                int id = await GetMaTinhThanh(TP);
        //                ungVien.TinhThanhID = id;
        //            }



        //            string TinhTrangHonNhanCode = row[10].ToString();
        //            if (TinhTrangHonNhanCode != null)
        //            {
        //                // ungVien.TinhTrangHonNhanCode = TGD.FirstOrDefault(x => x.Code == "TTHN" && x.Value == TinhTrangHonNhanCode).CDName;
        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "TTHN" && tgd.Value == TinhTrangHonNhanCode
        //                                    select tgd.CDName;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.TinhTrangHonNhanCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.TinhTrangHonNhanCode = null;
        //            }

        //            string TrinhDoDaoTaoCode = row[11].ToString();
        //            if (TrinhDoDaoTaoCode != null)
        //            {
        //                //   ungVien.TrinhDoDaoTaoCode = TGD.FirstOrDefault(x => x.Code == "TDDT" && x.Value == TrinhDoDaoTaoCode).CDName;
        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "TDDT" && tgd.Value == TrinhDoDaoTaoCode
        //                                    select tgd.Code;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.TrinhDoDaoTaoCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.TrinhDoDaoTaoCode = null;
        //            }
        //            ungVien.TrinhDoVanHoa = row[12].ToString();
        //            NoiDT = row[13].ToString();
        //            if (NoiDT != "")
        //            {
        //                int id = await GetMaNoiDaotao(NoiDT);
        //                ungVien.NoiDaoTaoID = id;
        //            }
        //            else
        //            {
        //                ungVien.NoiDaoTaoID = null;
        //            }

        //            ungVien.Khoa = row[14].ToString();

        //            ungVien.ChuyenNganh = row[15].ToString();


        //            string XepLoaiCode = row[16].ToString();
        //            if (XepLoaiCode != null)
        //            {

        //                // ungVien.XepLoaiCode = TGD.FirstOrDefault(x => x.Code == "XLHL" && x.Value == XepLoaiCode).CDName;
        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "XLHL" && tgd.Value == XepLoaiCode
        //                                    select tgd.CDName;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.XepLoaiCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.XepLoaiCode = null;
        //            }

        //            NTN = row[17].ToString();
        //            if (NTN != "")
        //            {

        //                ungVien.NamTotNghiep = int.Parse((NTN).ToString());
        //            }
        //            else
        //            {
        //                ungVien.NamTotNghiep = null;
        //            }

        //            string TienDoTuyenDungCode = row[19].ToString();
        //            if (TienDoTuyenDungCode != null)
        //            {
        //                //  ungVien.TienDoTuyenDungCode = TGD.FirstOrDefault(x => x.Code == "TDTD" && x.Value == TienDoTuyenDungCode).CDName;
        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "TDTD" && tgd.Value == TienDoTuyenDungCode
        //                                    select tgd.CDName;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.TienDoTuyenDungCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.TienDoTuyenDungCode = null;
        //            }

        //            string TrangThaiCode = row[18].ToString();
        //            if (TrangThaiCode != null)
        //            {
        //                //  ungVien.TrangThaiCode = TGD.FirstOrDefault(x => x.Code == "TRTH" && x.Value == TrangThaiCode).CDName;
        //                var customerQuery = from tgd in TGD
        //                                    where tgd.Code == "TRTH" && tgd.Value == TrangThaiCode
        //                                    select tgd.CDName;

        //                foreach (var Customer in customerQuery)
        //                {
        //                    ungVien.TrangThaiCode = Customer;
        //                }
        //            }
        //            else
        //            {
        //                ungVien.TrangThaiCode = null;
        //            }
        //            ungVien.TepDinhKem = row[20].ToString();
        //            ungVien.RECORD_STATUS = row[21].ToString();
        //            if (row[22].ToString() != "")
        //            {
        //                ungVien.MARKER_ID = int.Parse(row[22].ToString());
        //            }
        //            else
        //            {
        //                ungVien.MARKER_ID = null;
        //            }
        //            ungVien.AUTH_STATUS = row[23].ToString();
        //            if (row[24].ToString() != "")
        //            {
        //                ungVien.CHECKER_ID = int.Parse(row[24].ToString());
        //            }
        //            else
        //            {
        //                ungVien.CHECKER_ID = null;
        //            }

        //            ungVien.APPROVE_DT = row[25].ToString().ToDatetimeFormat();
        //            ungVien.DienThoai = row[26].ToString();
        //            ungVien.Email = row[27].ToString();
        //            ungVien.DiaChi = row[28].ToString();

        //            ungVien.Time1 = row[29].ToString();

        //            string day1 = row[30].ToString();
        //            if (day1 != "")
        //            {
        //                //Day1 = Convert.ToDateTime(day1);

        //                DateTime datePV1 = DateTime.Parse(day1 + " " + ungVien.Time1, new CultureInfo("en-GB"));
        //                ungVien.Day1 = datePV1;
        //            }
        //            else
        //            {
        //                ungVien.Day1 = null;
        //            }
        //            ungVien.Time2 = row[31].ToString();

        //            string day2 = row[32].ToString();
        //            if (day2 != "")
        //            {
        //                DateTime datePV2 = DateTime.Parse(day2 + " " + ungVien.Time2 ,new CultureInfo("en-GB"));
        //                ungVien.Day2 = datePV2;
        //            }
        //            else
        //            {
        //                ungVien.Day2 = null;
        //            }
        //            ungVien.Time3 = row[33].ToString();
        //            string day3 = row[34].ToString();
        //            if (day3 != "")
        //            {
        //                DateTime datePV3 = DateTime.Parse(day3 + " " + ungVien.Time3, new CultureInfo("en-GB"));
        //                ungVien.Day3 = datePV3;
        //            }
        //            else
        //            {
        //                ungVien.Day3 = null;
        //            }

        //            ungVien.Note = row[35].ToString();

        //            ungVien.TenCTY = row[36].ToString();

        //            CreateOrEditUngVienInput createOrEditUngVienInput = new CreateOrEditUngVienInput();
        //            createOrEditUngVienInput.UngVien = ungVien;
        //            await CreateOrEdit(createOrEditUngVienInput);



        //        }

        //        return mes;
        //    }

        //    catch (Exception ex)
        //    {

        //        Logger.Error("+hoten" + hoten);
        //        Logger.Error(ex.StackTrace);
        //        return "";
        //    }
        //}

    }

}
