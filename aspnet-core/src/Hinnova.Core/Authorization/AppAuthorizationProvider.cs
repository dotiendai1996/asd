using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Hinnova.Authorization
{
    /// <summary>
    /// Application's authorization provider
    /// Defines permissions for the application
    /// See <see cref="AppPermissions"/> for all permission names
    /// </summary>

    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var quanLyTrucTiepPNPs = pages.CreateChildPermission(AppPermissions.Pages_QuanLyTrucTiepPNPs, L("QuanLyTrucTiepPNPs"), multiTenancySides: MultiTenancySides.Host);
            quanLyTrucTiepPNPs.CreateChildPermission(AppPermissions.Pages_QuanLyTrucTiepPNPs_Create, L("CreateNewQuanLyTrucTiepPNP"), multiTenancySides: MultiTenancySides.Host);
            quanLyTrucTiepPNPs.CreateChildPermission(AppPermissions.Pages_QuanLyTrucTiepPNPs_Edit, L("EditQuanLyTrucTiepPNP"), multiTenancySides: MultiTenancySides.Host);
            quanLyTrucTiepPNPs.CreateChildPermission(AppPermissions.Pages_QuanLyTrucTiepPNPs_Delete, L("DeleteQuanLyTrucTiepPNP"), multiTenancySides: MultiTenancySides.Host);



            var quanLyNghiPheps = pages.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps, L("QuanLyNghiPheps"), multiTenancySides: MultiTenancySides.Host);
            quanLyNghiPheps.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps_Create, L("CreateNewQuanLyNghiPhep"), multiTenancySides: MultiTenancySides.Host);
            quanLyNghiPheps.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps_Edit, L("EditQuanLyNghiPhep"), multiTenancySides: MultiTenancySides.Host);
            quanLyNghiPheps.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps_Delete, L("DeleteQuanLyNghiPhep"), multiTenancySides: MultiTenancySides.Host);
            quanLyNghiPheps.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps_Approve, L("Approve"), multiTenancySides: MultiTenancySides.Host);
            quanLyNghiPheps.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps_Import, L("Import"), multiTenancySides: MultiTenancySides.Host);
            quanLyNghiPheps.CreateChildPermission(AppPermissions.Pages_QuanLyNghiPheps_Export, L("Export"), multiTenancySides: MultiTenancySides.Host);



            var dataChamCongs = pages.CreateChildPermission(AppPermissions.Pages_DataChamCongs, L("DataChamCongs"), multiTenancySides: MultiTenancySides.Host);
            dataChamCongs.CreateChildPermission(AppPermissions.Pages_DataChamCongs_Create, L("CreateNewDataChamCong"), multiTenancySides: MultiTenancySides.Host);
            dataChamCongs.CreateChildPermission(AppPermissions.Pages_DataChamCongs_Edit, L("EditDataChamCong"), multiTenancySides: MultiTenancySides.Host);
            dataChamCongs.CreateChildPermission(AppPermissions.Pages_DataChamCongs_Delete, L("DeleteDataChamCong"), multiTenancySides: MultiTenancySides.Host);



            var quyTrinhCongTacs = pages.CreateChildPermission(AppPermissions.Pages_QuyTrinhCongTacs, L("QuyTrinhCongTacs"), multiTenancySides: MultiTenancySides.Host);
            quyTrinhCongTacs.CreateChildPermission(AppPermissions.Pages_QuyTrinhCongTacs_Create, L("CreateNewQuyTrinhCongTac"), multiTenancySides: MultiTenancySides.Host);
            quyTrinhCongTacs.CreateChildPermission(AppPermissions.Pages_QuyTrinhCongTacs_Edit, L("EditQuyTrinhCongTac"), multiTenancySides: MultiTenancySides.Host);
            quyTrinhCongTacs.CreateChildPermission(AppPermissions.Pages_QuyTrinhCongTacs_Delete, L("DeleteQuyTrinhCongTac"), multiTenancySides: MultiTenancySides.Host);



            var lichSuUploads = pages.CreateChildPermission(AppPermissions.Pages_LichSuUploads, L("LichSuUploads"), multiTenancySides: MultiTenancySides.Host);
            lichSuUploads.CreateChildPermission(AppPermissions.Pages_LichSuUploads_Create, L("CreateNewLichSuUpload"), multiTenancySides: MultiTenancySides.Host);
            lichSuUploads.CreateChildPermission(AppPermissions.Pages_LichSuUploads_Edit, L("EditLichSuUpload"), multiTenancySides: MultiTenancySides.Host);
            lichSuUploads.CreateChildPermission(AppPermissions.Pages_LichSuUploads_Delete, L("DeleteLichSuUpload"), multiTenancySides: MultiTenancySides.Host);



            //var viTriCongViecs = pages.CreateChildPermission(AppPermissions.Pages_ViTriCongViecs, L("ViTriCongViecs"), multiTenancySides: MultiTenancySides.Host);
            //viTriCongViecs.CreateChildPermission(AppPermissions.Pages_ViTriCongViecs_Create, L("CreateNewViTriCongViec"), multiTenancySides: MultiTenancySides.Host);
            //viTriCongViecs.CreateChildPermission(AppPermissions.Pages_ViTriCongViecs_Edit, L("EditViTriCongViec"), multiTenancySides: MultiTenancySides.Host);
            //viTriCongViecs.CreateChildPermission(AppPermissions.Pages_ViTriCongViecs_Delete, L("DeleteViTriCongViec"), multiTenancySides: MultiTenancySides.Host);



            var configEmails = pages.CreateChildPermission(AppPermissions.Pages_ConfigEmails, L("ConfigEmails"), multiTenancySides: MultiTenancySides.Host);
            configEmails.CreateChildPermission(AppPermissions.Pages_ConfigEmails_Create, L("CreateNewConfigEmail"), multiTenancySides: MultiTenancySides.Host);
            configEmails.CreateChildPermission(AppPermissions.Pages_ConfigEmails_Edit, L("EditConfigEmail"), multiTenancySides: MultiTenancySides.Host);
            configEmails.CreateChildPermission(AppPermissions.Pages_ConfigEmails_Delete, L("DeleteConfigEmail"), multiTenancySides: MultiTenancySides.Host);



            //var Pages_QLTD = pages.CreateChildPermission(AppPermissions.Pages_QLTD, L("Pages_QLTD"), multiTenancySides: MultiTenancySides.Host);
            //var Pages_QuanTri = pages.CreateChildPermission(AppPermissions.Pages_QuanTri, L("Pages_QuanTri"), multiTenancySides: MultiTenancySides.Host);
            //var Pages_QuanTriCot = pages.CreateChildPermission(AppPermissions.Pages_QuanTriCot, L("Pages_QuanTriCot"), multiTenancySides: MultiTenancySides.Host);
            //var Pages_HTQT = pages.CreateChildPermission(AppPermissions.Pages_HTQT, L("Pages_HTQT"), multiTenancySides: MultiTenancySides.Host);


            var syS_PREFIXs = pages.CreateChildPermission(AppPermissions.Pages_SYS_PREFIXs, L("SYS_PREFIXs"), multiTenancySides: MultiTenancySides.Host);

            syS_PREFIXs.CreateChildPermission(AppPermissions.Pages_SYS_PREFIXs_Create, L("CreateNewSYS_PREFIX"), multiTenancySides: MultiTenancySides.Host);
            syS_PREFIXs.CreateChildPermission(AppPermissions.Pages_SYS_PREFIXs_Edit, L("EditSYS_PREFIX"), multiTenancySides: MultiTenancySides.Host);
            syS_PREFIXs.CreateChildPermission(AppPermissions.Pages_SYS_PREFIXs_Delete, L("DeleteSYS_PREFIX"), multiTenancySides: MultiTenancySides.Host);



            var syS_CODEMASTERSs = pages.CreateChildPermission(AppPermissions.Pages_SYS_CODEMASTERSs, L("SYS_CODEMASTERSs"), multiTenancySides: MultiTenancySides.Host);
            syS_CODEMASTERSs.CreateChildPermission(AppPermissions.Pages_SYS_CODEMASTERSs_Create, L("CreateNewSYS_CODEMASTERS"), multiTenancySides: MultiTenancySides.Host);
            syS_CODEMASTERSs.CreateChildPermission(AppPermissions.Pages_SYS_CODEMASTERSs_Edit, L("EditSYS_CODEMASTERS"), multiTenancySides: MultiTenancySides.Host);
            syS_CODEMASTERSs.CreateChildPermission(AppPermissions.Pages_SYS_CODEMASTERSs_Delete, L("DeleteSYS_CODEMASTERS"), multiTenancySides: MultiTenancySides.Host);



            var hoSos = pages.CreateChildPermission(AppPermissions.Pages_HoSos, L("HoSos"), multiTenancySides: MultiTenancySides.Host);
            hoSos.CreateChildPermission(AppPermissions.Pages_HoSos_Create, L("CreateNewHoSo"), multiTenancySides: MultiTenancySides.Host);
            hoSos.CreateChildPermission(AppPermissions.Pages_HoSos_Edit, L("EditHoSo"), multiTenancySides: MultiTenancySides.Host);
            hoSos.CreateChildPermission(AppPermissions.Pages_HoSos_Delete, L("DeleteHoSo"), multiTenancySides: MultiTenancySides.Host);
            hoSos.CreateChildPermission(AppPermissions.Pages_HoSos_Approve, L("Approve"), multiTenancySides: MultiTenancySides.Host);
            hoSos.CreateChildPermission(AppPermissions.Pages_HoSos_Import, L("Import"), multiTenancySides: MultiTenancySides.Host);
            hoSos.CreateChildPermission(AppPermissions.Pages_HoSos_Export, L("Export"), multiTenancySides: MultiTenancySides.Host);



            var hopDongs = pages.CreateChildPermission(AppPermissions.Pages_HopDongs, L("HopDongs"), multiTenancySides: MultiTenancySides.Host);
            hopDongs.CreateChildPermission(AppPermissions.Pages_HopDongs_Create, L("CreateNewHopDong"), multiTenancySides: MultiTenancySides.Host);
            hopDongs.CreateChildPermission(AppPermissions.Pages_HopDongs_Edit, L("EditHopDong"), multiTenancySides: MultiTenancySides.Host);
            hopDongs.CreateChildPermission(AppPermissions.Pages_HopDongs_Delete, L("DeleteHopDong"), multiTenancySides: MultiTenancySides.Host);
            hopDongs.CreateChildPermission(AppPermissions.Pages_HopDongs_Approve, L("Approve"), multiTenancySides: MultiTenancySides.Host);
            hopDongs.CreateChildPermission(AppPermissions.Pages_HopDongs_Import, L("Import"), multiTenancySides: MultiTenancySides.Host);
            hopDongs.CreateChildPermission(AppPermissions.Pages_HopDongs_Export, L("Export"), multiTenancySides: MultiTenancySides.Host);



            var ungViens = pages.CreateChildPermission(AppPermissions.Pages_UngViens, L("UngViens"), multiTenancySides: MultiTenancySides.Host);
            ungViens.CreateChildPermission(AppPermissions.Pages_UngViens_Create, L("CreateNewUngVien"), multiTenancySides: MultiTenancySides.Host);
            ungViens.CreateChildPermission(AppPermissions.Pages_UngViens_Edit, L("EditUngVien"), multiTenancySides: MultiTenancySides.Host);
            ungViens.CreateChildPermission(AppPermissions.Pages_UngViens_Delete, L("DeleteUngVien"), multiTenancySides: MultiTenancySides.Host);
            ungViens.CreateChildPermission(AppPermissions.Pages_UngViens_Approve, L("Approve"), multiTenancySides: MultiTenancySides.Host);
            ungViens.CreateChildPermission(AppPermissions.Pages_UngViens_Import, L("Import"), multiTenancySides: MultiTenancySides.Host);
            ungViens.CreateChildPermission(AppPermissions.Pages_UngViens_Export, L("Export"), multiTenancySides: MultiTenancySides.Host);

            var dangKyKCBs = pages.CreateChildPermission(AppPermissions.Pages_DangKyKCBs, L("DangKyKCBs"), multiTenancySides: MultiTenancySides.Host);
            dangKyKCBs.CreateChildPermission(AppPermissions.Pages_DangKyKCBs_Create, L("CreateNewDangKyKCB"), multiTenancySides: MultiTenancySides.Host);
            dangKyKCBs.CreateChildPermission(AppPermissions.Pages_DangKyKCBs_Edit, L("EditDangKyKCB"), multiTenancySides: MultiTenancySides.Host);
            dangKyKCBs.CreateChildPermission(AppPermissions.Pages_DangKyKCBs_Delete, L("DeleteDangKyKCB"), multiTenancySides: MultiTenancySides.Host);



            var lichSuLamViecs = pages.CreateChildPermission(AppPermissions.Pages_LichSuLamViecs, L("LichSuLamViecs"), multiTenancySides: MultiTenancySides.Host);
            lichSuLamViecs.CreateChildPermission(AppPermissions.Pages_LichSuLamViecs_Create, L("CreateNewLichSuLamViec"), multiTenancySides: MultiTenancySides.Host);
            lichSuLamViecs.CreateChildPermission(AppPermissions.Pages_LichSuLamViecs_Edit, L("EditLichSuLamViec"), multiTenancySides: MultiTenancySides.Host);
            lichSuLamViecs.CreateChildPermission(AppPermissions.Pages_LichSuLamViecs_Delete, L("DeleteLichSuLamViec"), multiTenancySides: MultiTenancySides.Host);



            var tinhThanhs = pages.CreateChildPermission(AppPermissions.Pages_TinhThanhs, L("TinhThanhs"), multiTenancySides: MultiTenancySides.Host);
            tinhThanhs.CreateChildPermission(AppPermissions.Pages_TinhThanhs_Create, L("CreateNewTinhThanh"), multiTenancySides: MultiTenancySides.Host);
            tinhThanhs.CreateChildPermission(AppPermissions.Pages_TinhThanhs_Edit, L("EditTinhThanh"), multiTenancySides: MultiTenancySides.Host);
            tinhThanhs.CreateChildPermission(AppPermissions.Pages_TinhThanhs_Delete, L("DeleteTinhThanh"), multiTenancySides: MultiTenancySides.Host);



            var truongGiaoDichs = pages.CreateChildPermission(AppPermissions.Pages_TruongGiaoDichs, L("TruongGiaoDichs"), multiTenancySides: MultiTenancySides.Host);
            truongGiaoDichs.CreateChildPermission(AppPermissions.Pages_TruongGiaoDichs_Create, L("CreateNewTruongGiaoDich"), multiTenancySides: MultiTenancySides.Host);
            truongGiaoDichs.CreateChildPermission(AppPermissions.Pages_TruongGiaoDichs_Edit, L("EditTruongGiaoDich"), multiTenancySides: MultiTenancySides.Host);
            truongGiaoDichs.CreateChildPermission(AppPermissions.Pages_TruongGiaoDichs_Delete, L("DeleteTruongGiaoDich"), multiTenancySides: MultiTenancySides.Host);



            var noiDaoTaos = pages.CreateChildPermission(AppPermissions.Pages_NoiDaoTaos, L("NoiDaoTaos"), multiTenancySides: MultiTenancySides.Host);
            noiDaoTaos.CreateChildPermission(AppPermissions.Pages_NoiDaoTaos_Create, L("CreateNewNoiDaoTao"), multiTenancySides: MultiTenancySides.Host);
            noiDaoTaos.CreateChildPermission(AppPermissions.Pages_NoiDaoTaos_Edit, L("EditNoiDaoTao"), multiTenancySides: MultiTenancySides.Host);
            noiDaoTaos.CreateChildPermission(AppPermissions.Pages_NoiDaoTaos_Delete, L("Delete.NoiDaoTao"), multiTenancySides: MultiTenancySides.Host);



            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var masterLists = administration.CreateChildPermission(AppPermissions.Pages_Administration_MasterLists, L("MasterLists"), multiTenancySides: MultiTenancySides.Host);
            masterLists.CreateChildPermission(AppPermissions.Pages_Administration_MasterLists_Create, L("CreateNewMasterList"), multiTenancySides: MultiTenancySides.Host);
            masterLists.CreateChildPermission(AppPermissions.Pages_Administration_MasterLists_Edit, L("EditMasterList"), multiTenancySides: MultiTenancySides.Host);
            masterLists.CreateChildPermission(AppPermissions.Pages_Administration_MasterLists_Delete, L("DeleteMasterList"), multiTenancySides: MultiTenancySides.Host);



            var templates = administration.CreateChildPermission(AppPermissions.Pages_Administration_Templates, L("Templates"), multiTenancySides: MultiTenancySides.Host);
            templates.CreateChildPermission(AppPermissions.Pages_Administration_Templates_Create, L("CreateNewTemplate"), multiTenancySides: MultiTenancySides.Host);
            templates.CreateChildPermission(AppPermissions.Pages_Administration_Templates_Edit, L("EditTemplate"), multiTenancySides: MultiTenancySides.Host);
            templates.CreateChildPermission(AppPermissions.Pages_Administration_Templates_Delete, L("DeleteTemplate"), multiTenancySides: MultiTenancySides.Host);













            //var dynamicActions = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicActions, L("DynamicActions"));
            //dynamicActions.CreateChildPermission(AppPermissions.Pages_Administration_DynamicActions_Create, L("CreateNewDynamicAction"));
            //dynamicActions.CreateChildPermission(AppPermissions.Pages_Administration_DynamicActions_Edit, L("EditDynamicAction"));
            //dynamicActions.CreateChildPermission(AppPermissions.Pages_Administration_DynamicActions_Delete, L("DeleteDynamicAction"));



            var roleMappers = administration.CreateChildPermission(AppPermissions.Pages_Administration_RoleMappers, L("RoleMappers"));
            roleMappers.CreateChildPermission(AppPermissions.Pages_Administration_RoleMappers_Create, L("CreateNewRoleMapper"));
            roleMappers.CreateChildPermission(AppPermissions.Pages_Administration_RoleMappers_Edit, L("EditRoleMapper"));
            roleMappers.CreateChildPermission(AppPermissions.Pages_Administration_RoleMappers_Delete, L("DeleteRoleMapper"));


            //var keywordDetails = pages.CreateChildPermission(AppPermissions.Pages_KeywordDetails, L("KeywordDetails"));
            //keywordDetails.CreateChildPermission(AppPermissions.Pages_KeywordDetails_Create, L("CreateNewKeywordDetail"));
            //keywordDetails.CreateChildPermission(AppPermissions.Pages_KeywordDetails_Edit, L("EditKeywordDetail"));
            //keywordDetails.CreateChildPermission(AppPermissions.Pages_KeywordDetails_Delete, L("DeleteKeywordDetail"));



            //var documentStatuses = pages.CreateChildPermission(AppPermissions.Pages_DocumentStatuses, L("DocumentStatuses"));
            //documentStatuses.CreateChildPermission(AppPermissions.Pages_DocumentStatuses_Create, L("CreateNewDocumentStatus"));
            //documentStatuses.CreateChildPermission(AppPermissions.Pages_DocumentStatuses_Edit, L("EditDocumentStatus"));
            //documentStatuses.CreateChildPermission(AppPermissions.Pages_DocumentStatuses_Delete, L("DeleteDocumentStatus"));



            //var memorize_Keywordses = pages.CreateChildPermission(AppPermissions.Pages_Memorize_Keywordses, L("Memorize_Keywordses"));
            //memorize_Keywordses.CreateChildPermission(AppPermissions.Pages_Memorize_Keywordses_Create, L("CreateNewMemorize_Keywords"));
            //memorize_Keywordses.CreateChildPermission(AppPermissions.Pages_Memorize_Keywordses_Edit, L("EditMemorize_Keywords"));
            //memorize_Keywordses.CreateChildPermission(AppPermissions.Pages_Memorize_Keywordses_Delete, L("DeleteMemorize_Keywords"));



            //var textBooks = pages.CreateChildPermission(AppPermissions.Pages_TextBooks, L("TextBooks"));
            //textBooks.CreateChildPermission(AppPermissions.Pages_TextBooks_Create, L("CreateNewTextBook"));
            //textBooks.CreateChildPermission(AppPermissions.Pages_TextBooks_Edit, L("EditTextBook"));
            //textBooks.CreateChildPermission(AppPermissions.Pages_TextBooks_Delete, L("DeleteTextBook"));



            //var priorities = pages.CreateChildPermission(AppPermissions.Pages_Priorities, L("Priorities"));
            //priorities.CreateChildPermission(AppPermissions.Pages_Priorities_Create, L("CreateNewPriority"));
            //priorities.CreateChildPermission(AppPermissions.Pages_Priorities_Edit, L("EditPriority"));
            //priorities.CreateChildPermission(AppPermissions.Pages_Priorities_Delete, L("DeletePriority"));



            //var storeDatasources = pages.CreateChildPermission(AppPermissions.Pages_StoreDatasources, L("StoreDatasources"));
            //storeDatasources.CreateChildPermission(AppPermissions.Pages_StoreDatasources_Create, L("CreateNewStoreDatasource"));
            //storeDatasources.CreateChildPermission(AppPermissions.Pages_StoreDatasources_Edit, L("EditStoreDatasource"));
            //storeDatasources.CreateChildPermission(AppPermissions.Pages_StoreDatasources_Delete, L("DeleteStoreDatasource"));



            //var hardDatasources = pages.CreateChildPermission(AppPermissions.Pages_HardDatasources, L("HardDatasources"));
            //hardDatasources.CreateChildPermission(AppPermissions.Pages_HardDatasources_Create, L("CreateNewHardDatasource"));
            //hardDatasources.CreateChildPermission(AppPermissions.Pages_HardDatasources_Edit, L("EditHardDatasource"));
            //hardDatasources.CreateChildPermission(AppPermissions.Pages_HardDatasources_Delete, L("DeleteHardDatasource"));



            //var commandDatasources = pages.CreateChildPermission(AppPermissions.Pages_CommandDatasources, L("CommandDatasources"));
            //commandDatasources.CreateChildPermission(AppPermissions.Pages_CommandDatasources_Create, L("CreateNewCommandDatasource"));
            //commandDatasources.CreateChildPermission(AppPermissions.Pages_CommandDatasources_Edit, L("EditCommandDatasource"));
            //commandDatasources.CreateChildPermission(AppPermissions.Pages_CommandDatasources_Delete, L("DeleteCommandDatasource"));



            //var dynamicDatasource = pages.CreateChildPermission(AppPermissions.Pages_DynamicDatasource, L("DynamicDatasource"));
            //dynamicDatasource.CreateChildPermission(AppPermissions.Pages_DynamicDatasource_Create, L("CreateNewDynamicDatasource"));
            //dynamicDatasource.CreateChildPermission(AppPermissions.Pages_DynamicDatasource_Edit, L("EditDynamicDatasource"));
            //dynamicDatasource.CreateChildPermission(AppPermissions.Pages_DynamicDatasource_Delete, L("DeleteDynamicDatasource"));



            //var dynamicValues = pages.CreateChildPermission(AppPermissions.Pages_DynamicValues, L("DynamicValues"));
            //dynamicValues.CreateChildPermission(AppPermissions.Pages_DynamicValues_Create, L("CreateNewDynamicValue"));
            //dynamicValues.CreateChildPermission(AppPermissions.Pages_DynamicValues_Edit, L("EditDynamicValue"));
            //dynamicValues.CreateChildPermission(AppPermissions.Pages_DynamicValues_Delete, L("DeleteDynamicValue"));



            //var dynamicFields = pages.CreateChildPermission(AppPermissions.Pages_DynamicFields, L("DynamicFields"));
            //dynamicFields.CreateChildPermission(AppPermissions.Pages_DynamicFields_Create, L("CreateNewDynamicField"));
            //dynamicFields.CreateChildPermission(AppPermissions.Pages_DynamicFields_Edit, L("EditDynamicField"));
            //dynamicFields.CreateChildPermission(AppPermissions.Pages_DynamicFields_Delete, L("DeleteDynamicField"));



            //var sqlStoreParams = pages.CreateChildPermission(AppPermissions.Pages_SqlStoreParams, L("SqlStoreParams"));
            //sqlStoreParams.CreateChildPermission(AppPermissions.Pages_SqlStoreParams_Create, L("CreateNewSqlStoreParam"));
            //sqlStoreParams.CreateChildPermission(AppPermissions.Pages_SqlStoreParams_Edit, L("EditSqlStoreParam"));
            //sqlStoreParams.CreateChildPermission(AppPermissions.Pages_SqlStoreParams_Delete, L("DeleteSqlStoreParam"));



            //var sqlConfigDetails = pages.CreateChildPermission(AppPermissions.Pages_SqlConfigDetails, L("SqlConfigDetails"));
            //sqlConfigDetails.CreateChildPermission(AppPermissions.Pages_SqlConfigDetails_Create, L("CreateNewSqlConfigDetail"));
            //sqlConfigDetails.CreateChildPermission(AppPermissions.Pages_SqlConfigDetails_Edit, L("EditSqlConfigDetail"));
            //sqlConfigDetails.CreateChildPermission(AppPermissions.Pages_SqlConfigDetails_Delete, L("DeleteSqlConfigDetail"));



            //var sqlConfigs = pages.CreateChildPermission(AppPermissions.Pages_SqlConfigs, L("SqlConfigs"));
            //sqlConfigs.CreateChildPermission(AppPermissions.Pages_SqlConfigs_Create, L("CreateNewSqlConfig"));
            //sqlConfigs.CreateChildPermission(AppPermissions.Pages_SqlConfigs_Edit, L("EditSqlConfig"));
            //sqlConfigs.CreateChildPermission(AppPermissions.Pages_SqlConfigs_Delete, L("DeleteSqlConfig"));



            //var historyUploads = pages.CreateChildPermission(AppPermissions.Pages_HistoryUploads, L("HistoryUploads"));
            //historyUploads.CreateChildPermission(AppPermissions.Pages_HistoryUploads_Create, L("CreateNewHistoryUpload"));
            //historyUploads.CreateChildPermission(AppPermissions.Pages_HistoryUploads_Edit, L("EditHistoryUpload"));
            //historyUploads.CreateChildPermission(AppPermissions.Pages_HistoryUploads_Delete, L("DeleteHistoryUpload"));



            //var vanbans = pages.CreateChildPermission(AppPermissions.Pages_Vanbans, L("Vanbans"), multiTenancySides: MultiTenancySides.Host);
            //vanbans.CreateChildPermission(AppPermissions.Pages_Vanbans_Create, L("CreateNewVanban"), multiTenancySides: MultiTenancySides.Host);
            //vanbans.CreateChildPermission(AppPermissions.Pages_Vanbans_Edit, L("EditVanban"), multiTenancySides: MultiTenancySides.Host);
            //vanbans.CreateChildPermission(AppPermissions.Pages_Vanbans_Delete, L("DeleteVanban"), multiTenancySides: MultiTenancySides.Host);



            //var documentHandlings = pages.CreateChildPermission(AppPermissions.Pages_DocumentHandlings, L("DocumentHandlings"));
            //documentHandlings.CreateChildPermission(AppPermissions.Pages_DocumentHandlings_Create, L("CreateNewDocumentHandling"));
            //documentHandlings.CreateChildPermission(AppPermissions.Pages_DocumentHandlings_Edit, L("EditDocumentHandling"));
            //documentHandlings.CreateChildPermission(AppPermissions.Pages_DocumentHandlings_Delete, L("DeleteDocumentHandling"));



            //var schedules = pages.CreateChildPermission(AppPermissions.Pages_Schedules, L("Schedules"));
            //schedules.CreateChildPermission(AppPermissions.Pages_Schedules_Create, L("CreateNewSchedule"));
            //schedules.CreateChildPermission(AppPermissions.Pages_Schedules_Edit, L("EditSchedule"));
            //schedules.CreateChildPermission(AppPermissions.Pages_Schedules_Delete, L("DeleteSchedule"));



            //var promulgateds = pages.CreateChildPermission(AppPermissions.Pages_Promulgateds, L("Promulgateds"));
            //promulgateds.CreateChildPermission(AppPermissions.Pages_Promulgateds_Create, L("CreateNewPromulgated"));
            //promulgateds.CreateChildPermission(AppPermissions.Pages_Promulgateds_Edit, L("EditPromulgated"));
            //promulgateds.CreateChildPermission(AppPermissions.Pages_Promulgateds_Delete, L("DeletePromulgated"));



            //var receiveUnits = pages.CreateChildPermission(AppPermissions.Pages_ReceiveUnits, L("ReceiveUnits"));
            //receiveUnits.CreateChildPermission(AppPermissions.Pages_ReceiveUnits_Create, L("CreateNewReceiveUnit"));
            //receiveUnits.CreateChildPermission(AppPermissions.Pages_ReceiveUnits_Edit, L("EditReceiveUnit"));
            //receiveUnits.CreateChildPermission(AppPermissions.Pages_ReceiveUnits_Delete, L("DeleteReceiveUnit"));



            //var typeHandes = pages.CreateChildPermission(AppPermissions.Pages_TypeHandes, L("TypeHandes"));
            //typeHandes.CreateChildPermission(AppPermissions.Pages_TypeHandes_Create, L("CreateNewTypeHande"));
            //typeHandes.CreateChildPermission(AppPermissions.Pages_TypeHandes_Edit, L("EditTypeHande"));
            //typeHandes.CreateChildPermission(AppPermissions.Pages_TypeHandes_Delete, L("DeleteTypeHande"));



            //var documentHandlingDetails = pages.CreateChildPermission(AppPermissions.Pages_DocumentHandlingDetails, L("DocumentHandlingDetails"));
            //documentHandlingDetails.CreateChildPermission(AppPermissions.Pages_DocumentHandlingDetails_Create, L("CreateNewDocumentHandlingDetail"));
            //documentHandlingDetails.CreateChildPermission(AppPermissions.Pages_DocumentHandlingDetails_Edit, L("EditDocumentHandlingDetail"));
            //documentHandlingDetails.CreateChildPermission(AppPermissions.Pages_DocumentHandlingDetails_Delete, L("DeleteDocumentHandlingDetail"));



            //var memorizeKeywords = pages.CreateChildPermission(AppPermissions.Pages_MemorizeKeywords, L("MemorizeKeywords"));
            //memorizeKeywords.CreateChildPermission(AppPermissions.Pages_MemorizeKeywords_Create, L("CreateNewMemorizeKeyword"));
            //memorizeKeywords.CreateChildPermission(AppPermissions.Pages_MemorizeKeywords_Edit, L("EditMemorizeKeyword"));
            //memorizeKeywords.CreateChildPermission(AppPermissions.Pages_MemorizeKeywords_Delete, L("DeleteMemorizeKeyword"));



            //var documentDetails = pages.CreateChildPermission(AppPermissions.Pages_DocumentDetails, L("DocumentDetails"));
            //documentDetails.CreateChildPermission(AppPermissions.Pages_DocumentDetails_Create, L("CreateNewDocumentDetail"));
            //documentDetails.CreateChildPermission(AppPermissions.Pages_DocumentDetails_Edit, L("EditDocumentDetail"));
            //documentDetails.CreateChildPermission(AppPermissions.Pages_DocumentDetails_Delete, L("DeleteDocumentDetail"));



            //var workDetails = pages.CreateChildPermission(AppPermissions.Pages_WorkDetails, L("WorkDetails"));
            //workDetails.CreateChildPermission(AppPermissions.Pages_WorkDetails_Create, L("CreateNewWorkDetail"));
            //workDetails.CreateChildPermission(AppPermissions.Pages_WorkDetails_Edit, L("EditWorkDetail"));
            //workDetails.CreateChildPermission(AppPermissions.Pages_WorkDetails_Delete, L("DeleteWorkDetail"));



            //var wordProcessings = pages.CreateChildPermission(AppPermissions.Pages_WordProcessings, L("WordProcessings"));
            //wordProcessings.CreateChildPermission(AppPermissions.Pages_WordProcessings_Create, L("CreateNewWordProcessing"));
            //wordProcessings.CreateChildPermission(AppPermissions.Pages_WordProcessings_Edit, L("EditWordProcessing"));
            //wordProcessings.CreateChildPermission(AppPermissions.Pages_WordProcessings_Delete, L("DeleteWordProcessing"));



            //var workAssigns = pages.CreateChildPermission(AppPermissions.Pages_WorkAssigns, L("WorkAssigns"));
            //workAssigns.CreateChildPermission(AppPermissions.Pages_WorkAssigns_Create, L("CreateNewWorkAssign"));
            //workAssigns.CreateChildPermission(AppPermissions.Pages_WorkAssigns_Edit, L("EditWorkAssign"));
            //workAssigns.CreateChildPermission(AppPermissions.Pages_WorkAssigns_Delete, L("DeleteWorkAssign"));



            //var documentTypes = pages.CreateChildPermission(AppPermissions.Pages_DocumentTypes, L("DocumentTypes"));
            //documentTypes.CreateChildPermission(AppPermissions.Pages_DocumentTypes_Create, L("CreateNewDocumentType"));
            //documentTypes.CreateChildPermission(AppPermissions.Pages_DocumentTypes_Edit, L("EditDocumentType"));
            //documentTypes.CreateChildPermission(AppPermissions.Pages_DocumentTypes_Delete, L("DeleteDocumentType"));



            //var documentses = pages.CreateChildPermission(AppPermissions.Pages_Documents, L("Documentses"));
            //documentses.CreateChildPermission(AppPermissions.Pages_Documents_Create, L("CreateNewDocuments"));
            //documentses.CreateChildPermission(AppPermissions.Pages_Documents_Edit, L("EditDocuments"));
            //documentses.CreateChildPermission(AppPermissions.Pages_Documents_Delete, L("DeleteDocuments"));


            //pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));



            var labels = administration.CreateChildPermission(AppPermissions.Pages_Administration_Labels, L("Labels"), multiTenancySides: MultiTenancySides.Host);
            labels.CreateChildPermission(AppPermissions.Pages_Administration_Labels_Create, L("CreateNewLabel"), multiTenancySides: MultiTenancySides.Host);
            labels.CreateChildPermission(AppPermissions.Pages_Administration_Labels_Edit, L("EditLabel"), multiTenancySides: MultiTenancySides.Host);
            labels.CreateChildPermission(AppPermissions.Pages_Administration_Labels_Delete, L("DeleteLabel"), multiTenancySides: MultiTenancySides.Host);



            //var settingConfigs = administration.CreateChildPermission(AppPermissions.Pages_Administration_SettingConfigs, L("SettingConfigs"), multiTenancySides: MultiTenancySides.Host);
            //settingConfigs.CreateChildPermission(AppPermissions.Pages_Administration_SettingConfigs_Create, L("CreateNewSettingConfig"), multiTenancySides: MultiTenancySides.Host);
            //settingConfigs.CreateChildPermission(AppPermissions.Pages_Administration_SettingConfigs_Edit, L("EditSettingConfig"), multiTenancySides: MultiTenancySides.Host);
            //settingConfigs.CreateChildPermission(AppPermissions.Pages_Administration_SettingConfigs_Delete, L("DeleteSettingConfig"), multiTenancySides: MultiTenancySides.Host);



            var menus = administration.CreateChildPermission(AppPermissions.Pages_Administration_Menus, L("Menus"));
            menus.CreateChildPermission(AppPermissions.Pages_Administration_Menus_Create, L("CreateNewMenu"));
            menus.CreateChildPermission(AppPermissions.Pages_Administration_Menus_Edit, L("EditMenu"));
            menus.CreateChildPermission(AppPermissions.Pages_Administration_Menus_Delete, L("DeleteMenu"));



            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, HinnovaConsts.LocalizationSourceName);
        }
    }
}
