using Hinnova.QLNS.Dtos;
using Hinnova.QLNSDtos;
using Hinnova.QLNS;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using Hinnova.Auditing.Dto;
using Hinnova.Authorization.Accounts.Dto;
using Hinnova.Authorization.Permissions.Dto;
using Hinnova.Authorization.Roles;
using Hinnova.Authorization.Roles.Dto;
using Hinnova.Authorization.Users;
using Hinnova.Authorization.Users.Dto;
using Hinnova.Authorization.Users.Importing.Dto;
using Hinnova.Authorization.Users.Profile.Dto;
using Hinnova.Chat;
using Hinnova.Chat.Dto;
using Hinnova.Editions;
using Hinnova.Editions.Dto;
using Hinnova.Friendships;
using Hinnova.Friendships.Cache;
using Hinnova.Friendships.Dto;
using Hinnova.Localization.Dto;
using Hinnova.Management;
using Hinnova.Management.Dtos;
using Hinnova.MultiTenancy;
using Hinnova.MultiTenancy.Dto;
using Hinnova.MultiTenancy.HostDashboard.Dto;
using Hinnova.MultiTenancy.Payments;
using Hinnova.MultiTenancy.Payments.Dto;
using Hinnova.Notifications.Dto;
using Hinnova.Organizations.Dto;
using Hinnova.QLVB;
using Hinnova.QLVB.Dtos;
using Hinnova.Sessions.Dto;
using Hinnova.QLNS.Importing;

namespace Hinnova
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditQuanLyTrucTiepPNPDto, QuanLyTrucTiepPNP>().ReverseMap();
            configuration.CreateMap<QuanLyTrucTiepPNPDto, QuanLyTrucTiepPNP>().ReverseMap();
            configuration.CreateMap<CreateOrEditQuanLyNghiPhepDto, QuanLyNghiPhep>().ReverseMap();
            configuration.CreateMap<QuanLyNghiPhepDto, QuanLyNghiPhep>().ReverseMap();
            configuration.CreateMap<CreateOrEditDataChamCongDto, DataChamCong>().ReverseMap();
            configuration.CreateMap<DataChamCongDto, DataChamCong>().ReverseMap();
            configuration.CreateMap<CreateOrEditQuyTrinhCongTacDto, QuyTrinhCongTac>().ReverseMap();
            configuration.CreateMap<QuyTrinhCongTacDto, QuyTrinhCongTac>().ReverseMap();
            configuration.CreateMap<CreateOrEditLichSuUploadDto, LichSuUpload>().ReverseMap();
            configuration.CreateMap<LichSuUploadDto, LichSuUpload>().ReverseMap();
            configuration.CreateMap<CreateOrEditViTriCongViecDto, ViTriCongViec>().ReverseMap();
            configuration.CreateMap<ViTriCongViecDto, ViTriCongViec>().ReverseMap();
            configuration.CreateMap<CreateOrEditConfigEmailDto, ConfigEmail>().ReverseMap();
            configuration.CreateMap<ConfigEmailDto, ConfigEmail>().ReverseMap();
            configuration.CreateMap<CreateOrEditTemplateDto, Template>().ReverseMap();
            configuration.CreateMap<TemplateDto, Template>().ReverseMap();
           configuration.CreateMap<CreateOrEditSYS_PREFIXDto, SYS_PREFIX>().ReverseMap();
           configuration.CreateMap<SYS_PREFIXDto, SYS_PREFIX>().ReverseMap();
           configuration.CreateMap<CreateOrEditSYS_CODEMASTERSDto, SYS_CODEMASTERS>().ReverseMap();
           configuration.CreateMap<SYS_CODEMASTERSDto, SYS_CODEMASTERS>().ReverseMap();
           configuration.CreateMap<CreateOrEditHoSoDto, HoSo>().ReverseMap();
           configuration.CreateMap<HoSoImportDto, CreateOrEditHoSoDto > ().ReverseMap();
           configuration.CreateMap<HoSoDto, HoSo>().ReverseMap();
           configuration.CreateMap<CreateOrEditHopDongDto, HopDong>().ReverseMap();
           configuration.CreateMap<HopDongDto, HopDong>().ReverseMap();
           configuration.CreateMap<CreateOrEditUngVienDto, UngVien>().ReverseMap();
           configuration.CreateMap<UngVienDto, UngVien>().ReverseMap();
           configuration.CreateMap<CreateOrEditDangKyKCBDto, DangKyKCB>().ReverseMap();
           configuration.CreateMap<DangKyKCBDto, DangKyKCB>().ReverseMap();
           configuration.CreateMap<CreateOrEditLichSuLamViecDto, LichSuLamViec>().ReverseMap();
           configuration.CreateMap<LichSuLamViecDto, LichSuLamViec>().ReverseMap();
           configuration.CreateMap<CreateOrEditTinhThanhDto, TinhThanh>().ReverseMap();
           configuration.CreateMap<TinhThanhDto, TinhThanh>().ReverseMap();
           configuration.CreateMap<CreateOrEditTruongGiaoDichDto, TruongGiaoDich>().ReverseMap();
           configuration.CreateMap<TruongGiaoDichDto, TruongGiaoDich>().ReverseMap();
           configuration.CreateMap<CreateOrEditNoiDaoTaoDto, NoiDaoTao>().ReverseMap();
           configuration.CreateMap<NoiDaoTaoDto, NoiDaoTao>().ReverseMap();
            configuration.CreateMap<CreateOrEditDynamicActionDto, DynamicAction>().ReverseMap();
            configuration.CreateMap<DynamicActionDto, DynamicAction>().ReverseMap();
            configuration.CreateMap<CreateOrEditRoleMapperDto, RoleMapper>().ReverseMap();
            configuration.CreateMap<RoleMapperDto, RoleMapper>().ReverseMap();
            configuration.CreateMap<CreateOrEditKeywordDetailDto, KeywordDetail>().ReverseMap();
            configuration.CreateMap<KeywordDetailDto, KeywordDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentStatusDto, DocumentStatus>().ReverseMap();
            configuration.CreateMap<DocumentStatusDto, DocumentStatus>().ReverseMap();
           configuration.CreateMap<CreateOrEditPriorityDto, Priority>().ReverseMap();
           configuration.CreateMap<PriorityDto, Priority>().ReverseMap();
            configuration.CreateMap<CreateOrEditMemorize_KeywordsDto, Memorize_Keywords>().ReverseMap();
            configuration.CreateMap<Memorize_KeywordsDto, Memorize_Keywords>().ReverseMap();
           configuration.CreateMap<CreateOrEditPriorityDto, Priority>().ReverseMap();
           configuration.CreateMap<PriorityDto, Priority>().ReverseMap();
           configuration.CreateMap<CreateOrEditTextBookDto, TextBook>().ReverseMap();
           configuration.CreateMap<TextBookDto, TextBook>().ReverseMap();
           configuration.CreateMap<CreateOrEditStoreDatasourceDto, StoreDatasource>().ReverseMap();
           configuration.CreateMap<StoreDatasourceDto, StoreDatasource>().ReverseMap();
           configuration.CreateMap<CreateOrEditHardDatasourceDto, HardDatasource>().ReverseMap();
           configuration.CreateMap<HardDatasourceDto, HardDatasource>().ReverseMap();
           configuration.CreateMap<CreateOrEditCommandDatasourceDto, CommandDatasource>().ReverseMap();
           configuration.CreateMap<CommandDatasourceDto, CommandDatasource>().ReverseMap();
           configuration.CreateMap<CreateOrEditDynamicDatasourceDto, DynamicDatasource>().ReverseMap();
           configuration.CreateMap<DynamicDatasourceDto, DynamicDatasource>().ReverseMap();
           configuration.CreateMap<CreateOrEditLabelDto, Label>().ReverseMap();
           configuration.CreateMap<LabelDto, Label>().ReverseMap();
           configuration.CreateMap<CreateOrEditDynamicValueDto, DynamicValue>().ReverseMap();
           configuration.CreateMap<DynamicValueDto, DynamicValue>().ReverseMap();
           configuration.CreateMap<CreateOrEditDynamicFieldDto, DynamicField>().ReverseMap();
           configuration.CreateMap<DynamicFieldDto, DynamicField>().ReverseMap();
           configuration.CreateMap<CreateOrEditSqlStoreParamDto, SqlStoreParam>().ReverseMap();
           configuration.CreateMap<SqlStoreParamDto, SqlStoreParam>().ReverseMap();
            configuration.CreateMap<CreateOrEditSqlConfigDetailDto, SqlConfigDetailDto>().ReverseMap();
            configuration.CreateMap<CreateOrEditSqlConfigDetailDto, SqlConfigDetail>().ReverseMap();
            configuration.CreateMap<SqlConfigDetailDto, SqlConfigDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditSqlConfigDto, SqlConfig>().ReverseMap();
            configuration.CreateMap<SqlConfigDto, SqlConfig>().ReverseMap();
            configuration.CreateMap<CreateOrEditSettingConfigDto, SettingConfig>().ReverseMap();
            configuration.CreateMap<SettingConfigDto, SettingConfig>().ReverseMap();
           configuration.CreateMap<CreateOrEditHistoryUploadDto, HistoryUpload>().ReverseMap();
           configuration.CreateMap<HistoryUploadDto, HistoryUpload>().ReverseMap();
           configuration.CreateMap<CreateOrEditSettingConfigDto, SettingConfig>().ReverseMap();
           configuration.CreateMap<SettingConfigDto, SettingConfig>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentHandlingDto, DocumentHandling>().ReverseMap();
            configuration.CreateMap<DocumentHandlingDto, DocumentHandling>().ReverseMap();
            configuration.CreateMap<CreateOrEditVanbanDto, Vanban>().ReverseMap();
            configuration.CreateMap<VanbanDto, Vanban>().ReverseMap();

            configuration.CreateMap<CreateOrEditScheduleDto, Schedule>().ReverseMap();
            configuration.CreateMap<ScheduleDto, Schedule>().ReverseMap();
            configuration.CreateMap<CreateOrEditPromulgatedDto, Promulgated>().ReverseMap();
            configuration.CreateMap<PromulgatedDto, Promulgated>().ReverseMap();
            configuration.CreateMap<CreateOrEditReceiveUnitDto, ReceiveUnit>().ReverseMap();
            configuration.CreateMap<ReceiveUnitDto, ReceiveUnit>().ReverseMap();
            configuration.CreateMap<CreateOrEditTypeHandeDto, TypeHandle>().ReverseMap();
            configuration.CreateMap<TypeHandeDto, TypeHandle>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentHandlingDetailDto, DocumentHandlingDetail>().ReverseMap();
            configuration.CreateMap<DocumentHandlingDetailDto, DocumentHandlingDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditMemorizeKeywordDto, MemorizeKeyword>().ReverseMap();
            configuration.CreateMap<MemorizeKeywordDto, MemorizeKeyword>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentDetailDto, DocumentDetail>().ReverseMap();
            configuration.CreateMap<DocumentDetailDto, DocumentDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkDetailDto, WorkDetail>().ReverseMap();
            configuration.CreateMap<WorkDetailDto, WorkDetail>().ReverseMap();
            configuration.CreateMap<CreateOrEditWordProcessingDto, WorkHandling>().ReverseMap();
            configuration.CreateMap<WordProcessingDto, WorkHandling>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkAssignDto, WorkAssign>().ReverseMap();
            configuration.CreateMap<WorkAssignDto, WorkAssign>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentTypeDto, DocumentType>().ReverseMap();
            configuration.CreateMap<DocumentTypeDto, DocumentType>().ReverseMap();
            configuration.CreateMap<CreateOrEditDocumentsDto, Documents>().ReverseMap();
            configuration.CreateMap<DocumentsDto, Documents>().ReverseMap();
            configuration.CreateMap<CreateOrEditMenuDto, Menu>().ReverseMap();
            configuration.CreateMap<MenuDto, Menu>().ReverseMap();
            configuration.CreateMap<PermissionDto, Permission>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}