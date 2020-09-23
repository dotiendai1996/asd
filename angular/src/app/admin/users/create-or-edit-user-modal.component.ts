import { AfterViewChecked, Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';
import { CreateOrUpdateUserInput, OrganizationUnitDto, PasswordComplexitySetting, ProfileServiceProxy, UserEditDto, UserRoleDto, UserServiceProxy, HoSosServiceProxy, ListResultDtoOfOrganizationUnitDto, OrganizationUnitServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { IOrganizationUnitsTreeComponentData, OrganizationUnitsTreeComponent } from '../shared/organization-unit-tree.component';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { HoSosComponent } from '@app/main/qlns/hoSos/hoSos.component';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import moment from 'moment';

@Component({
    selector: 'createOrEditUserModal',
    templateUrl: './create-or-edit-user-modal.component.html',
    styles: [`.user-edit-dialog-profile-image {
             margin-bottom: 20px;
        }`
    ]
})
export class CreateOrEditUserModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('organizationUnitTree', { static: false }) organizationUnitTree: OrganizationUnitsTreeComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    canChangeUserName = true;
    canChangeEmployeeCode = true;
    isTwoFactorEnabled: boolean = this.setting.getBoolean('Abp.Zero.UserManagement.TwoFactorLogin.IsEnabled');
    isLockoutEnabled: boolean = this.setting.getBoolean('Abp.Zero.UserManagement.UserLockOut.IsEnabled');
    passwordComplexitySetting: PasswordComplexitySetting = new PasswordComplexitySetting();

    user: UserEditDto = new UserEditDto();
    roles: UserRoleDto[];
    sendActivationEmail = true;
    setRandomPassword = true;
    passwordComplexityInfo = '';
    profilePicture: string;

    allOrganizationUnits: OrganizationUnitDto[];
    memberedOrganizationUnits: string[];
    userPasswordRepeat = '';
    userProfileStatusList: any;
    popupVisibleHoSo: boolean = false;
    isReadonly: boolean = true;

    maNhanVien: any;
    selectedRowsData: any[] = [];
    dataRowDetail: any;
    listDataHoSo: any;
    filterText = '';
    tenCtyFilter = '';
    hopDongHienTaiFilter = '';
    soHDFilter = '';
    donViCongTacNameFilter = '';
    choNgoiFilter = '';
    maxNoiDaoTaoIDFilter: number;
    maxNoiDaoTaoIDFilterEmpty: number;
    minNoiDaoTaoIDFilter: number;
    minNoiDaoTaoIDFilterEmpty: number;
    maxLoaiHopDongIDFilter: number;
    maxLoaiHopDongIDFilterEmpty: number;
    minLoaiHopDongIDFilter: number;
    minLoaiHopDongIDFilterEmpty: number;
    maSoNoiKCBFilter = '';
    maxNoiDangKyKCBIDFilter: number;
    maxNoiDangKyKCBIDFilterEmpty: number;
    minNoiDangKyKCBIDFilter: number;
    minNoiDangKyKCBIDFilterEmpty: number;
    maxNgayHetHanBHYTFilter: moment.Moment;
    minNgayHetHanBHYTFilter: moment.Moment;
    soTheBHYTFilter = '';
    maTinhCapFilter = '';
    maSoBHXHFilter = '';
    soSoBHXHFilter = '';
    maxTyLeDongBHFilter: number;
    maxTyLeDongBHFilterEmpty: number;
    minTyLeDongBHFilter: number;
    minTyLeDongBHFilterEmpty: number;
    maxNgayThamGiaBHFilter: moment.Moment;
    minNgayThamGiaBHFilter: moment.Moment;
    thamGiaCongDoanFilter = -1;
    nganHangCodeFilter = '';
    tkNganHangFilter = '';
    donViSoCongChuanCodeFilter = '';
    soCongChuanFilter = '';
    luongDongBHFilter = '';
    luongCoBanFilter = '';
    bacLuongCodeFilter = '';
    maxSoNgayPhepFilter: number;
    maxSoNgayPhepFilterEmpty: number;
    minSoNgayPhepFilter: number;
    minSoNgayPhepFilterEmpty: number;
    maxNgayChinhThucFilter: moment.Moment;
    minNgayChinhThucFilter: moment.Moment;
    maxNgayThuViecFilter: moment.Moment;
    minNgayThuViecFilter: moment.Moment;
    maxNgayTapSuFilter: moment.Moment;
    minNgayTapSuFilter: moment.Moment;
    soSoQLLaoDongFilter = '';
    diaDiemLamViecCodeFilter = '';
    quanLyGianTiepFilter = '';
    quanLyTrucTiepFilter = '';
    trangThaiLamViecCodeFilter = '';
    bacFilter = '';
    capFilter = '';
    chucDanhFilter = '';
    maChamCongFilter = '';
    diaChiLHKCFilter = '';
    emailLHKCFilter = '';
    dtDiDongLHKCFilter = '';
    dtNhaRiengLHKCFilter = '';
    quanHeLHKCFilter = '';
    hoVaTenLHKCFilter = '';
    diaChiHNFilter = '';
    tinhThanhIDHNFilter = '';
    quocGiaHNFilter = '';
    laChuHoFilter = -1;
    maSoHoGiaDinhFilter = '';
    soSoHoKhauFilter = '';
    diaChiHKTTFilter = '';
    tinhThanhIDHKTTFilter = '';
    quocGiaHKTTFilter = '';
    facebookFilter = '';
    skypeFilter = '';
    noiSinhFilter = '';
    tinhThanhIDFilter = '';
    nguyenQuanFilter = '';
    emailKhacFilter = '';
    emailCoQuanFilter = '';
    emailCaNhanFilter = '';
    dtKhacFilter = '';
    dtNhaRiengFilter = '';
    dtCoQuanFilter = '';
    dtDiDongFilter = '';
    tepDinhKemFilter = '';
    tinhTrangHonNhanCodeFilter = '';
    xepLoaiCodeFilter = '';
    maxNamTotNghiepFilter: number;
    maxNamTotNghiepFilterEmpty: number;
    minNamTotNghiepFilter: number;
    minNamTotNghiepFilterEmpty: number;
    chuyenNganhFilter = '';
    khoaFilter = '';
    trinhDoDaoTaoCodeFilter = '';
    trinhDoVanHoaFilter = '';
    maxNgayHetHanFilter: moment.Moment;
    minNgayHetHanFilter: moment.Moment;
    noiCapFilter = '';
    maxNgayCapFilter: moment.Moment;
    minNgayCapFilter: moment.Moment;
    soCMNDFilter = '';
    quocTichFilter = '';
    tonGiaoFilter = '';
    danTocFilter = '';
    viTriCongViecCodeFilter = '';
    maxDonViCongTacIDFilter: number;
    maxDonViCongTacIDFilterEmpty: number;
    minDonViCongTacIDFilter: number;
    minDonViCongTacIDFilterEmpty: number;
    mstCaNhanFilter = '';
    maxNgaySinhFilter: moment.Moment;
    minNgaySinhFilter: moment.Moment;
    gioiTinhCodeFilter = '';
    anhDaiDienFilter = '';
    hoVaTenFilter = '';
    maNhanVienFilter = '';
    maxTinhThanhIDHNFilter: number;
    maxTinhThanhIDHNFilterEmpty: number;
    minTinhThanhIDHNFilter: number;
    minTinhThanhIDHNFilterEmpty: number;
    maxTinhThanhIDHKTTFilter: number;
    maxTinhThanhIDHKTTFilterEmpty: number;
    minTinhThanhIDHKTTFilter: number;
    minTinhThanhIDHKTTFilterEmpty: number;
    maxTinhThanhIDFilter: number;
    maxTinhThanhIDFilterEmpty: number;
    minTinhThanhIDFilter: number;
    minTinhThanhIDFilterEmpty: number;
    chiNhanhFilter = '';
    dvtFilter = '';
    maxNgayKyHDKTHFilter: moment.Moment;
    minNgayKyHDKTHFilter: moment.Moment;
    maxNgayKyHD36THFilter: moment.Moment;
    minNgayKyHD36THFilter: moment.Moment;
    maxNgayKyHD12THFilter: moment.Moment;
    minNgayKyHD12THFilter: moment.Moment;
    maxNgayKyHDTVFilter: moment.Moment;
    minNgayKyHDTVFilter: moment.Moment;
    maxNgayKYHDCTVFilter: moment.Moment;
    minNgayKYHDCTVFilter: moment.Moment;
    maxNgayKyHDKVFilter: moment.Moment;
    minNgayKyHDKVFilter: moment.Moment;
    maxNgayKYHDTTFilter: moment.Moment;
    minNgayKYHDTTFilter: moment.Moment;
    maxNgayKyHDFilter: moment.Moment;
    minNgayKyHDFilter: moment.Moment;

    constructor(
        injector: Injector,
        private _userService: UserServiceProxy,
        private _profileService: ProfileServiceProxy,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _hoSosServiceProxy: HoSosServiceProxy,
    ) {
        super(injector);
    }
    ngOnInit(): void {
        this.getHoSos();
    }

    show(userId?: number): void {
        if (!userId) {
            this.active = true;
            this.setRandomPassword = true;
            this.sendActivationEmail = true;
        }
        this.getProfileStatus();

        this._userService.getUserForEdit(userId).subscribe(userResult => {
            this.user = userResult.user;
            console.log(this.user)


            this.roles = userResult.roles;
            this.canChangeUserName = this.user.userName !== AppConsts.userManagement.defaultAdminUserName;

            this.allOrganizationUnits = userResult.allOrganizationUnits;
            this.memberedOrganizationUnits = userResult.memberedOrganizationUnits;

            this.getProfilePicture(userResult.profilePictureId);

            if (userId) {
                this.active = true;

                setTimeout(() => {
                    this.setRandomPassword = false;
                }, 0);

                this.sendActivationEmail = false;
            }

            this._profileService.getPasswordComplexitySetting().subscribe(passwordComplexityResult => {
                this.passwordComplexitySetting = passwordComplexityResult.setting;
                this.setPasswordComplexityInfo();
                this.modal.show();
            });
        });
    }

    setPasswordComplexityInfo(): void {

        this.passwordComplexityInfo = '<ul>';

        if (this.passwordComplexitySetting.requireDigit) {
            this.passwordComplexityInfo += '<li>' + this.l('PasswordComplexity_RequireDigit_Hint') + '</li>';
        }

        if (this.passwordComplexitySetting.requireLowercase) {
            this.passwordComplexityInfo += '<li>' + this.l('PasswordComplexity_RequireLowercase_Hint') + '</li>';
        }

        if (this.passwordComplexitySetting.requireUppercase) {
            this.passwordComplexityInfo += '<li>' + this.l('PasswordComplexity_RequireUppercase_Hint') + '</li>';
        }

        if (this.passwordComplexitySetting.requireNonAlphanumeric) {
            this.passwordComplexityInfo += '<li>' + this.l('PasswordComplexity_RequireNonAlphanumeric_Hint') + '</li>';
        }

        if (this.passwordComplexitySetting.requiredLength) {
            this.passwordComplexityInfo += '<li>' + this.l('PasswordComplexity_RequiredLength_Hint', this.passwordComplexitySetting.requiredLength) + '</li>';
        }

        this.passwordComplexityInfo += '</ul>';
    }

    getProfilePicture(profilePictureId: string): void {
        if (!profilePictureId) {
            this.profilePicture = this.appRootUrl() + 'assets/common/images/default-profile-picture.png';
        } else {
            this._profileService.getProfilePictureById(profilePictureId).subscribe(result => {

                if (result && result.profilePicture) {
                    this.profilePicture = 'data:image/jpeg;base64,' + result.profilePicture;
                } else {
                    this.profilePicture = this.appRootUrl() + 'assets/common/images/default-profile-picture.png';
                }
            });
        }
    }

    getProfileStatus(): void {
        this._userService.getProfileStatus().subscribe((result) => {
            this.userProfileStatusList = result;
        }, error => {
            console.error();
        })
    }

    onShown(): void {
        this.organizationUnitTree.data = <IOrganizationUnitsTreeComponentData>{
            allOrganizationUnits: this.allOrganizationUnits,
            selectedOrganizationUnits: this.memberedOrganizationUnits
        };

        document.getElementById('Name').focus();
    }

    save(): void {
        let input = new CreateOrUpdateUserInput();
        input.user = this.user;
        console.log(input.user.employeeCode);

        input.setRandomPassword = this.setRandomPassword;
        input.sendActivationEmail = this.sendActivationEmail;
        input.assignedRoleNames =
            _.map(
                _.filter(this.roles, { isAssigned: true, inheritedFromOrganizationUnit: false }), role => role.roleName
            );

        input.organizationUnits = this.organizationUnitTree.getSelectedOrganizations();


        console.log(input);

        this.saving = true;
        this._userService.createOrUpdateUser(input)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.userPasswordRepeat = '';
        this.modal.hide();
    }

    getAssignedRoleCount(): number {
        return _.filter(this.roles, { isAssigned: true }).length;
    }

    showPublisherPopup() {
        this.popupVisibleHoSo = true;

        this.isReadonly = !this.isReadonly;
        console.log(this.isReadonly)
    }

    startEdit(e: any) {
        this.dataRowDetail = e.data;
        console.log(e.data);
        this.user.employeeCode = this.dataRowDetail.hoSo.maNhanVien;

        this.user.name = this.dataRowDetail.hoSo.hoVaTen;
        this.user.emailAddress = this.dataRowDetail.hoSo.emailCoQuan;
        this.user.phoneNumber = this.dataRowDetail.hoSo.dtCoQuan;
        this.user.surname = "";

        this.popupVisibleHoSo = false;
    }
    getHoSos() {

        this._hoSosServiceProxy.getAll(
            this.filterText,
            this.tenCtyFilter,
            this.hopDongHienTaiFilter,
            this.soHDFilter,
            this.donViCongTacNameFilter,
            this.choNgoiFilter,
            this.maxNoiDaoTaoIDFilter == null ? this.maxNoiDaoTaoIDFilterEmpty : this.maxNoiDaoTaoIDFilter,
            this.minNoiDaoTaoIDFilter == null ? this.minNoiDaoTaoIDFilterEmpty : this.minNoiDaoTaoIDFilter,
            this.maxLoaiHopDongIDFilter == null ? this.maxLoaiHopDongIDFilterEmpty : this.maxLoaiHopDongIDFilter,
            this.minLoaiHopDongIDFilter == null ? this.minLoaiHopDongIDFilterEmpty : this.minLoaiHopDongIDFilter,
            this.maSoNoiKCBFilter,
            this.maxNoiDangKyKCBIDFilter == null ? this.maxNoiDangKyKCBIDFilterEmpty : this.maxNoiDangKyKCBIDFilter,
            this.minNoiDangKyKCBIDFilter == null ? this.minNoiDangKyKCBIDFilterEmpty : this.minNoiDangKyKCBIDFilter,
            this.maxNgayHetHanBHYTFilter,
            this.minNgayHetHanBHYTFilter,
            this.soTheBHYTFilter,
            this.maTinhCapFilter,
            this.maSoBHXHFilter,
            this.soSoBHXHFilter,
            this.maxTyLeDongBHFilter == null ? this.maxTyLeDongBHFilterEmpty : this.maxTyLeDongBHFilter,
            this.minTyLeDongBHFilter == null ? this.minTyLeDongBHFilterEmpty : this.minTyLeDongBHFilter,
            this.maxNgayThamGiaBHFilter,
            this.minNgayThamGiaBHFilter,
            this.thamGiaCongDoanFilter,
            this.nganHangCodeFilter,
            this.tkNganHangFilter,
            this.donViSoCongChuanCodeFilter,
            this.soCongChuanFilter,
            this.luongDongBHFilter,
            this.luongCoBanFilter,
            this.bacLuongCodeFilter,
            this.maxSoNgayPhepFilter == null ? this.maxSoNgayPhepFilterEmpty : this.maxSoNgayPhepFilter,
            this.minSoNgayPhepFilter == null ? this.minSoNgayPhepFilterEmpty : this.minSoNgayPhepFilter,
            this.maxNgayChinhThucFilter,
            this.minNgayChinhThucFilter,
            this.maxNgayThuViecFilter,
            this.minNgayThuViecFilter,
            this.maxNgayTapSuFilter,
            this.minNgayTapSuFilter,
            this.soSoQLLaoDongFilter,
            this.diaDiemLamViecCodeFilter,
            this.quanLyGianTiepFilter,
            this.quanLyTrucTiepFilter,
            this.trangThaiLamViecCodeFilter,
            this.bacFilter,
            this.capFilter,
            this.chucDanhFilter,
            this.maChamCongFilter,
            this.diaChiLHKCFilter,
            this.emailLHKCFilter,
            this.dtDiDongLHKCFilter,
            this.dtNhaRiengLHKCFilter,
            this.quanHeLHKCFilter,
            this.hoVaTenLHKCFilter,
            this.diaChiHNFilter,
            this.maxTinhThanhIDHNFilter == null ? this.maxTinhThanhIDHNFilterEmpty : this.maxTinhThanhIDHNFilter,
            this.minTinhThanhIDHNFilter == null ? this.minTinhThanhIDHNFilterEmpty : this.minTinhThanhIDHNFilter,
            this.quocGiaHNFilter,
            this.laChuHoFilter,
            this.maSoHoGiaDinhFilter,
            this.soSoHoKhauFilter,
            this.diaChiHKTTFilter,
            this.maxTinhThanhIDHKTTFilter == null ? this.maxTinhThanhIDHKTTFilterEmpty : this.maxTinhThanhIDHKTTFilter,
            this.minTinhThanhIDHKTTFilter == null ? this.minTinhThanhIDHKTTFilterEmpty : this.minTinhThanhIDHKTTFilter,
            this.quocGiaHKTTFilter,
            this.facebookFilter,
            this.skypeFilter,
            this.noiSinhFilter,
            this.maxTinhThanhIDFilter == null ? this.maxTinhThanhIDFilterEmpty : this.maxTinhThanhIDFilter,
            this.minTinhThanhIDFilter == null ? this.minTinhThanhIDFilterEmpty : this.minTinhThanhIDFilter,
            this.nguyenQuanFilter,
            this.emailKhacFilter,
            this.emailCoQuanFilter,
            this.emailCaNhanFilter,
            this.dtKhacFilter,
            this.dtNhaRiengFilter,
            this.dtCoQuanFilter,
            this.dtDiDongFilter,
            this.tepDinhKemFilter,
            this.tinhTrangHonNhanCodeFilter,
            this.xepLoaiCodeFilter,
            this.maxNamTotNghiepFilter == null ? this.maxNamTotNghiepFilterEmpty : this.maxNamTotNghiepFilter,
            this.minNamTotNghiepFilter == null ? this.minNamTotNghiepFilterEmpty : this.minNamTotNghiepFilter,
            this.chuyenNganhFilter,
            this.khoaFilter,
            this.trinhDoDaoTaoCodeFilter,
            this.trinhDoVanHoaFilter,
            this.maxNgayHetHanFilter,
            this.minNgayHetHanFilter,
            this.noiCapFilter,
            this.maxNgayCapFilter,
            this.minNgayCapFilter,
            this.soCMNDFilter,
            this.quocTichFilter,
            this.tonGiaoFilter,
            this.danTocFilter,
            this.viTriCongViecCodeFilter,
            this.maxDonViCongTacIDFilter == null ? this.maxDonViCongTacIDFilterEmpty : this.maxDonViCongTacIDFilter,
            this.minDonViCongTacIDFilter == null ? this.minDonViCongTacIDFilterEmpty : this.minDonViCongTacIDFilter,
            this.mstCaNhanFilter,
            this.maxNgaySinhFilter,
            this.minNgaySinhFilter,
            this.gioiTinhCodeFilter,
            this.anhDaiDienFilter,
            this.hoVaTenFilter,
            this.maNhanVienFilter,
            this.chiNhanhFilter,
            this.dvtFilter,
            this.maxNgayKyHDKTHFilter,
            this.minNgayKyHDKTHFilter,
            this.maxNgayKyHD36THFilter,
            this.minNgayKyHD36THFilter,
            this.maxNgayKyHD12THFilter,
            this.minNgayKyHD12THFilter,
            this.maxNgayKyHDTVFilter,
            this.minNgayKyHDTVFilter,
            this.maxNgayKYHDCTVFilter,
            this.minNgayKYHDCTVFilter,
            this.maxNgayKyHDKVFilter,
            this.minNgayKyHDKVFilter,
            this.maxNgayKYHDTTFilter,
            this.minNgayKYHDTTFilter,
            this.maxNgayKyHDFilter,
            this.minNgayKyHDFilter,
            "1",
            0,
            100000
        ).subscribe(result => {

            this.listDataHoSo = result.items;

        });
    }
}
