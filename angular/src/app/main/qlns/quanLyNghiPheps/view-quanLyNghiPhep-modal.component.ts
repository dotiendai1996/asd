import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, SessionServiceProxy, UngVienDto, LichSuLamViecDto, ListResultDtoOfOrganizationUnitDto, OrganizationUnitServiceProxy, HoSosServiceProxy, CreateOrEditLichSuUploadDto, LichSuUploadsServiceProxy, CreateOrEditUngVienInput, LichSuUploadNewDto, UserLoginInfoDto, UserLoginServiceProxy, CreateOrEditQuanLyNghiPhepDto, QuanLyNghiPhepsServiceProxy, CreateOrEditNghiPhepInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { formatDate } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';
import { DxFileUploaderComponent, DxFormComponent } from 'devextreme-angular';
import { Router, ActivatedRoute } from '@angular/router';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import * as _ from 'lodash';
import { Location } from '@angular/common';

@Component({
    selector: 'viewQuanLyNghiPhepModal',
    templateUrl: './view-quanLyNghiPhep-modal.component.html'
})
export class ViewQuanLyNghiPhepModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('documentForm', { static: true }) documentForm: DxFormComponent;

    active = false;
    saving = false;
    rootUrl: string;
    link: string;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    listComment: LichSuLamViecDto[] = [];
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    gioiTinh: TruongGiaoDichDto[] = [];
    congty: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    tinhThanh: TinhThanhDto[] = [];
    noiDaoTao: NoiDaoTaoDto[] = [];
    createOrEditNghiPhepInput: CreateOrEditNghiPhepInput = new CreateOrEditNghiPhepInput();

    ungVien: CreateOrEditUngVienDto = new CreateOrEditUngVienDto();
    //  thongTinNghiPhep: CreateOrEditQuanLyNghiPhepDto = new CreateOrEditQuanLyNghiPhepDto();
    ngaySinh: Date;
    ngayCap: Date;
    approvE_DT: Date;
    time: string;
    uploadUrl: string;
    selectedRows = [];
    userId: number;
    currentDate = new Date();
    nameArr: any[] = [];
    dataDisplay = [];
    value: any[] = [];
    currentTime: any;
    tepDinhKemSave = '';
    years: any;
    totalUnitCount = 0;
    treeData: any;
    cmndExists = false;
    ungVienForCheckCMND: UngVienDto[] = [];
    tinhThanhIdSelected: number;
    listYear: [] = [];
    defaultCbbOption: TruongGiaoDichDto[] = [];
    popupVisible = false;
    danhSachCV: any;
    idDV: any;
    nam: [] = [];
    tennam: string;
    incommDateOptions: any;
    lichSuUploadList: any[] = [];
    selectedItems: any[] = [];
    a = false;
    //  lichSuUpload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();
    lichSuUpload: LichSuUploadNewDto = new LichSuUploadNewDto();
    danhSachDV: any;
    time2: any;
    time3: any;
    dayTime2: any;
    dayTime1: any;
    dayTime3: any;
    currentYear: any;
    thongTinHoSo: any;
    thongTinUser: any;
    danhSachUserPhongBan: any;
    thongTinNghiPhep: any;
    tenPhongBan: any;
    nghiPhep: any;
    congTac: any;
    quanLyTrucTiepID: any;
    truongBoPhanID: any;
    lichSu_Upload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();
    tangCa: any;
    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
        private location: Location,
        private _quanLyNghiPhepsServiceProxy: QuanLyNghiPhepsServiceProxy,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _userLoginServiceProxy: UserLoginServiceProxy,
        private _session: SessionServiceProxy
    ) {
        super(injector);
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;

    }

    cancel() {
        window.history.back(); // <-- go back to previous location on cancel
    }
    ngOnInit(): void {

        this.getCurrentUser();
        this.show();

    }

    troVe() {
        this.router.navigate(['/app/main/qlns/quanLyNghiPheps']);
    }

    getCurrentUser(): UserLoginInfoDto {

        console.log(this.appSession.userId)
        this._userLoginServiceProxy.getUserForView(this.appSession.userId).subscribe(res => {
            console.log(res)
            this.thongTinUser = res.user;
            console.log(this.thongTinUser.employeeCode);
            this._hoSosServiceProxy.getHoSoForViewNghiPhep(this.thongTinUser.employeeCode).subscribe(res => {
                console.log(res[0])
                this.thongTinHoSo = res[0];
                this.thongTinNghiPhep.tenNhanVien = this.thongTinHoSo.hoVaTen;
                this.thongTinNghiPhep.maNV = this.thongTinHoSo.maNhanVien;
                this.thongTinNghiPhep.tenCTY = this.thongTinHoSo.tenCty;
                this.thongTinNghiPhep.donViCongTacID = this.thongTinHoSo.donViCongTacID;

                if (this.thongTinHoSo.donViCongTacID) {
                    this._quanLyNghiPhepsServiceProxy.getOrganizationUnitForEdit(this.thongTinHoSo.donViCongTacID).subscribe(res => {

                        this.tenPhongBan = res.organizationUnit.displayName;
                        console.log(this.tenPhongBan)
                    })
                }
                else {
                    this.tenPhongBan = ""
                }
                this._hoSosServiceProxy.getAllUserPhongBan(this.thongTinHoSo.donViCongTacID , 144).subscribe(res => {
                    this.danhSachUserPhongBan = res;
                    console.log(this.danhSachUserPhongBan)

                })

            })
        })
        return this.appSession.user;
    }
    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this._quanLyNghiPhepsServiceProxy.getQuanLyNghiPhepForEdit(Number.parseInt(Id)).subscribe(res => {
            this.thongTinNghiPhep = res.quanLyNghiPhep;
            if (this.thongTinNghiPhep.trangThaiID == 4742) {
                this.a = !this.a;
            }
            else {
                this.a = this.a;
            }
            this.nghiPhep = this.thongTinNghiPhep.nghiPhep;
            this.congTac = this.thongTinNghiPhep.congTac;
            res.quanLyTrucTiepList.forEach(ele =>{
                this.selectedItems.push(ele.qltt)
            })
            console.log(this.selectedItems)
            this.truongBoPhanID = this.thongTinNghiPhep.truongBoPhanID;
            this.selectedRows = res.lichSuUploadList;
        })
    }
    pheDuyet() {

        this.thongTinNghiPhep.nguoiDuyetID = this.appSession.userId;
        this.thongTinNghiPhep.ngayDuyet = moment(this.currentDate).utc(true);
        this.thongTinNghiPhep.trangThaiID = 4742;
        this.createOrEditNghiPhepInput.nghiPhep = this.thongTinNghiPhep;
        for (var j = 0; j < this.selectedRows.length; j++) {
            let file = new LichSuUploadNewDto();
            file.tenFile = this.selectedRows[j].tenFile;
            file.tieuDe = this.selectedRows[j].tieuDe;
            file.dungLuong = this.selectedRows[j].dungLuong;
            this.lichSuUploadList.push(file);
        }
        this.createOrEditNghiPhepInput.lichSuUpLoad = this.lichSuUploadList;
        this._quanLyNghiPhepsServiceProxy.createOrEdit(this.createOrEditNghiPhepInput)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {

                this.notify.info(this.l('Đã duyệt thành công'));
                this.a = !this.a;

            });
    }

    close(): void {
        this.active = false;
    }
}
