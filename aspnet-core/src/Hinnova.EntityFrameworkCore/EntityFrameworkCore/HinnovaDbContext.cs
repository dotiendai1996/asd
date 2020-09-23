using Hinnova.QLVB;
using Hinnova.Management;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Hinnova.Authorization.Roles;
using Hinnova.Authorization.Users;
using Hinnova.Chat;
using Hinnova.Editions;
using Hinnova.Friendships;
using Hinnova.MultiTenancy;
using Hinnova.MultiTenancy.Accounting;
using Hinnova.MultiTenancy.Payments;
using Hinnova.Storage;
using Hinnova.QLNS;

namespace Hinnova.EntityFrameworkCore
{
    public class HinnovaDbContext : AbpZeroDbContext<Tenant, Role, User, HinnovaDbContext>, IAbpPersistedGrantDbContext
    {

        public virtual DbSet<QuanLyTrucTiepPNP> QuanLyTrucTiepPNPs { get; set; }

        public virtual DbSet<QuanLyNghiPhep> QuanLyNghiPheps { get; set; }

        public virtual DbSet<DataChamCong> DataChamCongs { get; set; }

        public virtual DbSet<QuyTrinhCongTac> QuyTrinhCongTacs { get; set; }

        public virtual DbSet<LichSuUpload> LichSuUploads { get; set; }

        //public virtual DbSet<ViTriCongViec> ViTriCongViecs { get; set; }

        public virtual DbSet<ConfigEmail> ConfigEmails { get; set; }

        public virtual DbSet<Template> Templates { get; set; }

		public virtual DbSet<SYS_PREFIX> SYS_PREFIXs { get; set; }

        public virtual DbSet<SYS_CODEMASTERS> SYS_CODEMASTERSs { get; set; }

        public virtual DbSet<HoSo> HoSos { get; set; }

        public virtual DbSet<HopDong> HopDongs { get; set; }

        public virtual DbSet<UngVien> UngViens { get; set; }

        public virtual DbSet<DangKyKCB> DangKyKCBs { get; set; }

        public virtual DbSet<LichSuLamViec> LichSuLamViecs { get; set; }

        public virtual DbSet<TinhThanh> TinhThanhs { get; set; }

        public virtual DbSet<TruongGiaoDich> TruongGiaoDichs { get; set; }

        public virtual DbSet<NoiDaoTao> NoiDaoTaos { get; set; }
		
        public virtual DbSet<DynamicAction> DynamicActions { get; set; }

        public virtual DbSet<RoleMapper> RoleMappers { get; set; }

        public virtual DbSet<KeywordDetail> KeywordDetails { get; set; }

        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }

        public virtual DbSet<Memorize_Keywords> Memorize_Keywordses { get; set; }

        public virtual DbSet<TextBook> TextBooks { get; set; }

        public virtual DbSet<Priority> Priorities { get; set; }

        public virtual DbSet<StoreDatasource> StoreDatasources { get; set; }

        public virtual DbSet<HardDatasource> HardDatasources { get; set; }

        public virtual DbSet<CommandDatasource> CommandDatasources { get; set; }

        public virtual DbSet<DynamicDatasource> DynamicDatasource { get; set; }

        public virtual DbSet<Label> Labels { get; set; }

        public virtual DbSet<DynamicValue> DynamicValues { get; set; }

        public virtual DbSet<DynamicField> DynamicFields { get; set; }

        public virtual DbSet<SqlStoreParam> SqlStoreParams { get; set; }

        public virtual DbSet<SqlConfigDetail> SqlConfigDetails { get; set; }

        public virtual DbSet<SqlConfig> SqlConfigs { get; set; }

        public virtual DbSet<HistoryUpload> HistoryUploads { get; set; }

        public virtual DbSet<SettingConfig> SettingConfigs { get; set; }

        public virtual DbSet<Vanban> Vanbans { get; set; }


        public virtual DbSet<DocumentHandling> DocumentHandlings { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }

        public virtual DbSet<Promulgated> Promulgateds { get; set; }

        public virtual DbSet<ReceiveUnit> ReceiveUnits { get; set; }

        public virtual DbSet<TypeHandle> TypeHandles { get; set; }

        public virtual DbSet<DocumentHandlingDetail> DocumentHandlingDetails { get; set; }

        public virtual DbSet<MemorizeKeyword> MemorizeKeywords { get; set; }

        public virtual DbSet<DocumentDetail> DocumentDetails { get; set; }

        public virtual DbSet<WorkDetail> WorkDetails { get; set; }

        public virtual DbSet<WorkHandling> WorkHandlings { get; set; }

        public virtual DbSet<WorkAssign> WorkAssigns { get; set; }

        public virtual DbSet<DocumentType> DocumentTypes { get; set; }

        public virtual DbSet<Documents> Documents { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public HinnovaDbContext(DbContextOptions<HinnovaDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
           
          

            
//            modelBuilder.Entity<ViTriCongViec>(v =>
//            {
//                //v.HasIndex(e => new { e.TenantId });
//            });
//modelBuilder.Entity<ConfigEmail>(c =>
//            //{
//            //    c.HasIndex(e => new { e.TenantId });
//            //});
// modelBuilder.Entity<DynamicAction>(d =>
//            //{
//            //    //d.HasIndex(e => new { e.TenantId });
//            //});
// modelBuilder.Entity<RoleMapper>(r =>
//            //{
//            //    r.HasIndex(e => new { e.TenantId });
//            //});
// modelBuilder.Entity<MemorizeKeyword>()
//                .HasIndex(u => u.KeyWord)
//                .IsUnique();

//            modelBuilder.Entity<KeywordDetail>(k =>
//            {
//                k.HasIndex(e => new { e.TenantId });
//            });
// modelBuilder.Entity<DocumentStatus>(d =>
//            {
//                d.HasIndex(e => new { e.TenantId });
//            });
 modelBuilder.Entity<TextBook>(t =>
           
            modelBuilder.Entity<Memorize_Keywords>(m =>
            {
                m.HasIndex(e => new { e.TenantId });
            }));
 modelBuilder.Entity<TextBook>(t =>
            {
                //t.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<DynamicDatasource>(d => 
            {
                d.HasIndex(e => new { e.TenantId });
            });
           
           
            modelBuilder.Entity<StoreDatasource>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<HardDatasource>(h =>
            {
                h.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<CommandDatasource>(c =>
            {
                c.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<Priority>(p =>
            {
                p.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<DynamicDatasource>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<DynamicValue>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<DynamicField>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<SqlStoreParam>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<SqlConfigDetail>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<SqlConfig>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<Schedule>(s =>
            {
                s.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<Promulgated>(p =>
            {
                p.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<ReceiveUnit>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<TypeHandle>(t =>
            {
                t.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<DocumentHandlingDetail>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<MemorizeKeyword>(m =>
            {
                m.HasIndex(e => new { e.TenantId });
                m.HasIndex(e => e.KeyWord).IsUnique();
            });
            modelBuilder.Entity<DocumentDetail>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<WorkDetail>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<WorkHandling>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<WorkAssign>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<DocumentType>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<Documents>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<Menu>(m =>
            {
                m.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
