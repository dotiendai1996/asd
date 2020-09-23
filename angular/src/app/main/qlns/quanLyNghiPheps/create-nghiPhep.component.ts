import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, SessionServiceProxy, UngVienDto, LichSuLamViecDto, ListResultDtoOfOrganizationUnitDto, OrganizationUnitServiceProxy, HoSosServiceProxy, CreateOrEditLichSuUploadDto, LichSuUploadsServiceProxy, CreateOrEditUngVienInput, LichSuUploadNewDto, UserLoginInfoDto, UserLoginServiceProxy, CreateOrEditQuanLyNghiPhepDto, QuanLyNghiPhepsServiceProxy, CreateOrEditNghiPhepInput, QuanLyTrucTiepPNPsServiceProxy, QuanLyTrucTiepPNPDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { formatDate } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';
import { DxFileUploaderComponent, DxFormComponent } from 'devextreme-angular';
import { Router } from '@angular/router';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import * as _ from 'lodash';
// import { Location } from '@angular/common';
// import 'devextreme/localization/globalize/number';
// import 'devextreme/localization/globalize/date';
// import 'devextreme/localization/globalize/currency';
// import 'devextreme/localization/globalize/message';
// import Globalize from 'globalize';

// const sendRequest = function(value) {
//     const validEmail = "test@dx-gmail.com";
//     return new Promise((resolve) => {
//         setTimeout(function() {
//             resolve(value === validEmail);
//         }, 1000);
//     });
// }
@Component({
    selector: 'createNghiPhep',

    styleUrls: ['./create-nghiPhep.component.less'],
    templateUrl: './create-nghiPhep.component.html'
})


export class CreateNghiPhepComponent extends AppComponentBase implements OnInit {

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
    thongTinNghiPhep: CreateOrEditQuanLyNghiPhepDto = new CreateOrEditQuanLyNghiPhepDto();
    // thongTinNghiPhep: any;
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

    quanLyTrucTiep: any[] = [];
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
    tenPhongBan: any;
    hoTenNhanVienPB: any;
    nghiPhep: any;
    congTac: any;
    tangCa: any;
    quanLyTrucTiepID: any;


    parentID: number;
    truongBoPhanID: any;
    danhSachUserPhongBanGĐ: any;
    ngayBatDau: any;
    ngayKetThuc: any;
    selectedItems: any[] = [];

    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
        private _quanLyNghiPhepsServiceProxy: QuanLyNghiPhepsServiceProxy,

        private _hoSosServiceProxy: HoSosServiceProxy,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _userLoginServiceProxy: UserLoginServiceProxy,
        private _quanLyTrucTiepPNPsServiceProxy: QuanLyTrucTiepPNPsServiceProxy,
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
    }

    troVe() {
        this.router.navigate(['/app/main/qlns/quanLyNghiPheps']);
    }

    //  ghep chuoi ten file
    setFullNameFile() {
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

        console.log(this.currentTime)
        this.value.forEach((ele) => {
            if (ele.size / 1024 / 1024 <= 25) {
                if ((ele.name).split('.').reverse()[0] == 'jpg' || (ele.name).split('.').reverse()[0] == 'png' || (ele.name).split('.').reverse()[0] == 'doc' || (ele.name).split('.').reverse()[0] == 'docx' || (ele.name).split('.').reverse()[0] == 'xlsx' || (ele.name).split('.').reverse()[0] == 'pdf') {
                    this.dataDisplay.push({ tenFile: cValue + "/" + this.currentTime + "/" + ele.name, dungLuong: Math.ceil(ele.size / 1024) + "  kb", tieuDe: "" });
                }
            }

        });
        console.log(this.value)

        this.value.length = 0;
        this.selectedRows = this.selectedRows.concat(this.dataDisplay);
        console.log(this.selectedRows)

    }


    orgLevelValueChanged(e: any) {
        console.log(e);
    }

    getCurrentUser(): UserLoginInfoDto {
        console.log(this.appSession.userId)
        this._userLoginServiceProxy.getUserForView(this.appSession.userId).subscribe(res => {

            // console.log(res)
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
                        this.parentID = res.organizationUnit.parentId;
                        console.log(this.parentID)
                        this.tenPhongBan = res.organizationUnit.displayName;
                        console.log(this.tenPhongBan)
                        this._hoSosServiceProxy.getAllUserPhongBan(this.thongTinHoSo.donViCongTacID, this.parentID).subscribe(res => {
                            this.danhSachUserPhongBan = res;
                            console.log(this.danhSachUserPhongBan)

                        })
                    })
                }
                else {
                    this.tenPhongBan = ""
                }
            })
        })

        return this.appSession.user;
    }




    save(): void {


        let result = this.documentForm.instance.validate();
        if (result.isValid) {



            if (this.nghiPhep) {
                this.thongTinNghiPhep.nghiPhep = this.nghiPhep;

            }
            else {
                this.thongTinNghiPhep.nghiPhep = false;
            }

            if (this.congTac) {
                this.thongTinNghiPhep.congTac = this.congTac;

            }
            else {
                this.thongTinNghiPhep.congTac = false;
            }

            if (this.tangCa) {
                this.thongTinNghiPhep.tangCa = this.tangCa;

            }
            else {
                this.thongTinNghiPhep.tangCa = false;
            }
            this.saving = true;

            // if (!this.quanLyTrucTiepID) {
            //     this.message.warn(" Điện thoại không được bỏ trống!");
            //     return;
            // }
            // else {
            //     this.thongTinNghiPhep.quanLyTrucTiepID = this.quanLyTrucTiepID;
            // }

            if (!this.truongBoPhanID) {

                this.message.warn(" Trưởng bộ phận không được bỏ trống!");
                return;
            }
            else {
                this.thongTinNghiPhep.truongBoPhanID = this.truongBoPhanID;
            }

            //  this.thongTinNghiPhep.quanLyTrucTiepID = [];
            this.thongTinNghiPhep.trangThaiID = 4743;
            //   this.thongTinNghiPhep.quanLyTrucTiepID =this.selectedItems.join(';');

            // this.selectedItems.forEach(x => {
            //     console.log(x)
            //     this.thongTinNghiPhep.quanLyTrucTiepID.push(x.id);
            // });
            console.log(this.selectedItems)
            for (var j = 0; j < this.selectedItems.length; j++) {
                let file = new QuanLyTrucTiepPNPDto();
             //   file.phieuNghiPhepID = this.selectedItems[j].phieuNghiPhepID;
                file.qltt = this.selectedItems[j];
                console.log(this.thongTinNghiPhep.maNV)
              file.maHoSo = this.thongTinNghiPhep.maNV;

             this.quanLyTrucTiep.push(file);

            }
            console.log(this.thongTinNghiPhep.quanLyTrucTiepID)

            this.thongTinNghiPhep.ngayBatDau = moment(this.thongTinNghiPhep.ngayBatDau).utc(true);
            this.thongTinNghiPhep.ngayKetThuc = moment(this.thongTinNghiPhep.ngayKetThuc).utc(true);

            this.createOrEditNghiPhepInput.nghiPhep = this.thongTinNghiPhep;
            console.log(this.thongTinNghiPhep);

            for (var j = 0; j < this.selectedRows.length; j++) {
                let file = new LichSuUploadNewDto();
                file.tenFile = this.selectedRows[j].tenFile;
                file.tieuDe = this.selectedRows[j].tieuDe;
                file.dungLuong = this.selectedRows[j].dungLuong;
                this.lichSuUploadList.push(file);
            }
            this.createOrEditNghiPhepInput.quanLyTrucTiepPNP = this.quanLyTrucTiep;
            this.createOrEditNghiPhepInput.lichSuUpLoad = this.lichSuUploadList;

            console.log(this.createOrEditNghiPhepInput);
            this._quanLyNghiPhepsServiceProxy.createOrEdit(this.createOrEditNghiPhepInput)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.documentForm.instance.resetValues();
                    this.router.navigate(['/app/main/qlns/quanLyNghiPheps']);
                });

        }
    }



    // // chức năng  xóa file
    // xoaFile(e: any) {

    //     console.log(this.selectedRows)
    //     this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
    //     console.log(this.selectedRows)
    //     this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');

    // }
    // // chức năng xem file chi tiết
    showDetail(e: any) {
        console.log(e)
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.link = this.rootUrl + "/" + e.data.tenFile;
        console.log(this.link)
        window.open(this.link, '_blank');
    }

    close(): void {

        this.active = false;
    }
}
