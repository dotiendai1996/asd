import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { UngViensServiceProxy, CreateOrEditUngVienDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, SessionServiceProxy, UngVienDto, LichSuLamViecDto, ListResultDtoOfOrganizationUnitDto, OrganizationUnitServiceProxy, HoSosServiceProxy, CreateOrEditLichSuUploadDto, LichSuUploadsServiceProxy, CreateOrEditUngVienInput, LichSuUploadNewDto, UserLoginInfoDto, UserLoginServiceProxy, CreateOrEditQuanLyNghiPhepDto, QuanLyNghiPhepsServiceProxy, CreateOrEditNghiPhepInput, LichSuLamViecsServiceProxy, QuanLyTrucTiepPNPDto, CreateOrEditQuanLyTrucTiepPNPDto, QuanLyTrucTiepPNPsServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { formatDate } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';
import { DxFileUploaderComponent, DxFormComponent } from 'devextreme-angular';
import { Router, ActivatedRoute } from '@angular/router';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import * as _ from 'lodash';
// import { Location } from '@angular/common';
@Component({
    selector: 'editNghiPhep',

    styleUrls: ['./edit-nghiPhep.component.less'],
    templateUrl: './edit-nghiPhep.component.html'
})


export class EditNghiPhepComponent extends AppComponentBase implements OnInit {

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
    tangCa: any;
    quanLyTrucTiepID: any;
    truongBoPhanID: any;
    lichSu_Upload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();

    qltt: CreateOrEditQuanLyTrucTiepPNPDto = new CreateOrEditQuanLyTrucTiepPNPDto();
    fileDinhKemComment: any;
    fileComment: string;
    phieuNghiPhepId: number;
    currentUserId = this.getCurrentUser().id;
    comment = '';
    currentUserFullName = this.getFullNameCurrentUser();
    uploadUrlImage: any;
    phieuNghiPhepID: number;
    ngayBatDau: any;
    ngayKetThuc: any;
    selectedItems: any[] = [];
    parentID:number;
    quanLyTrucTiep: any[] = [];
    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private router: Router,
        private _quanLyNghiPhepsServiceProxy: QuanLyNghiPhepsServiceProxy,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _userLoginServiceProxy: UserLoginServiceProxy,
        private _session: SessionServiceProxy,
        private _lichSuLamViecsServiceProxy: LichSuLamViecsServiceProxy ,
        private _quanLyTrucTiepPNPsServiceProxy: QuanLyTrucTiepPNPsServiceProxy,
    ) {
        super(injector);
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
        this.uploadUrlImage = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_fileImage';
    }

    cancel() {
        window.history.back(); // <-- go back to previous location on cancel
    }
    ngOnInit(): void {

        this.getCurrentUser();
        this.show();

    }

    showDetailFile(tenFile: string) {
        //console.log(tenFile)
        this.link = this.rootUrl + "/" + tenFile;
        window.open(this.link, '_blank');

    }

    troVe() {
        this.router.navigate(['/app/main/qlns/quanLyNghiPheps']);
    }

    //  ghep chuoi ten file
    setFullNameFile() {
        this.dataDisplay.length = 0;
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

        //console.log(this.currentTime)
        this.value.forEach((ele) => {
            if (ele.size / 1024 / 1024 <= 25) {
                if ((ele.name).split('.').reverse()[0] == 'jpg' || (ele.name).split('.').reverse()[0] == 'png' || (ele.name).split('.').reverse()[0] == 'doc' || (ele.name).split('.').reverse()[0] == 'docx' || (ele.name).split('.').reverse()[0] == 'xlsx' || (ele.name).split('.').reverse()[0] == 'pdf') {
                    this.dataDisplay.push({ tenFile: cValue + "/" + this.currentTime + "/" + ele.name, dungLuong: Math.ceil(ele.size / 1024) + "  kb", tieuDe: "" });
                }
            }
        });
        this.value.length = 0;
        this.selectedRows = this.selectedRows.concat(this.dataDisplay);
        //console.log(this.selectedRows)
        this.lichSu_Upload.type = "NP";
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("NP", Id).subscribe(res => {

        })
    }

    getCurrentUser(): UserLoginInfoDto {
        console.log(this.appSession.userId)
        this._userLoginServiceProxy.getUserForView(this.appSession.userId).subscribe(res => {

            this.thongTinUser = res.user;
            console.log(this.thongTinUser);
            console.log(this.thongTinUser.employeeCode);
            this._hoSosServiceProxy.getHoSoForViewNghiPhep(this.thongTinUser.employeeCode).subscribe(res => {
                console.log(res[0])
                this.thongTinHoSo = res[0];
                // this.thongTinNghiPhep.tenNhanVien = this.thongTinHoSo.hoVaTen;
                // this.thongTinNghiPhep.maNV = this.thongTinHoSo.maNhanVien;
                // this.thongTinNghiPhep.tenCTY = this.thongTinHoSo.tenCty;
                // this.thongTinNghiPhep.donViCongTacID = this.thongTinHoSo.donViCongTacID;

                if (this.thongTinHoSo.donViCongTacID) {
                    this._quanLyNghiPhepsServiceProxy.getOrganizationUnitForEdit(this.thongTinHoSo.donViCongTacID).subscribe(res => {
                        this.parentID = res.organizationUnit.parentId;
                        console.log(this.parentID)
                        this._hoSosServiceProxy.getAllUserPhongBan(this.thongTinHoSo.donViCongTacID ,  this.parentID ).subscribe(res => {
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

    orgLevelValueChanged(e:any)
    {

    }
    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        if (Id != null) {
            this.phieuNghiPhepId = Number.parseInt(Id);
            console.log(this.phieuNghiPhepId);
            this._quanLyNghiPhepsServiceProxy.getQuanLyNghiPhepForEdit(this.phieuNghiPhepId).subscribe(res => {
                this.thongTinNghiPhep = res.quanLyNghiPhep;
                console.log(this.thongTinNghiPhep)
                this.nghiPhep = this.thongTinNghiPhep.nghiPhep;
                this.congTac = this.thongTinNghiPhep.congTac;
                this._quanLyNghiPhepsServiceProxy.getOrganizationUnitForEdit(this.thongTinNghiPhep.donViCongTacID).subscribe(res => {


                  this.tenPhongBan = res.organizationUnit.displayName;


                })
               // this.selectedItems = res.quanLyNghiPhep.quanLyTrucTiepID.split(';');
                this.tenPhongBan =this.thongTinNghiPhep.tenPhongBan;
                this.truongBoPhanID = this.thongTinNghiPhep.truongBoPhanID;
                this.ngayBatDau = this.thongTinNghiPhep.ngayBatDau;
                this.ngayKetThuc = this.thongTinNghiPhep.ngayKetThuc;
                this.selectedRows = res.lichSuUploadList;
                // this.selectedItems =res.quanLyTrucTiepList;
                res.quanLyTrucTiepList.forEach(ele =>{
                    this.selectedItems.push(ele.qltt)
                })
                console.log(this.selectedItems)
                this._lichSuLamViecsServiceProxy.getLichSuLamViecByPhieuNghiPhep(this.phieuNghiPhepId).subscribe(res => {
                    console.log(res);
                    this.listComment =res;
                })
            })

        }
    }

    save(): void {
        this.lichSuUploadList.length = 0;

        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.phieuNghiPhepId = Number.parseInt(Id);
        this.createOrEditNghiPhepInput.nghiPhep = this.thongTinNghiPhep;
        this.lichSu_Upload.type = "NP"
        this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("NP", Id).subscribe(res => {

        })

        this._quanLyTrucTiepPNPsServiceProxy.xoaQuanLyTrucTiepId(this.phieuNghiPhepId).subscribe(res => {

        })
        if (this.selectedRows)
            this.selectedRows.forEach(ele => {
                this.lichSu_Upload.tenFile = ele.tenFile;
                this.lichSu_Upload.tieuDe = ele.tieuDe;
                this.lichSu_Upload.dungLuong = ele.dungLuong;
                this.lichSu_Upload.type = "NP";
                this.lichSu_Upload.typeID = Id;
                this.lichSuUploadList.push(this.lichSu_Upload);
                this._lichSuUploadsServiceProxy.createOrEdit(this.lichSu_Upload).pipe(finalize(() => { this.saving = false; }))
                    .subscribe(() => {

                    });
            });

            if(this.selectedItems)
            {
                this.selectedItems.forEach(ele =>{
                    console.log(ele);
                    this.qltt.qltt = ele;
                    this.qltt.phieuNghiPhepID =  this.phieuNghiPhepId;
                    console.log(this.thongTinNghiPhep.maNV)
                    this.qltt.maHoSo =  this.thongTinNghiPhep.maNV;
                    this.quanLyTrucTiep.push( this.qltt);
                    this._quanLyTrucTiepPNPsServiceProxy.createOrEdit( this.qltt).pipe(finalize(() => { this.saving = false; }))
                    .subscribe(() => {

                    });
                })



            }

        let result = this.documentForm.instance.validate();
        if (result.isValid) {

            this.saving = true;
            if (!this.thongTinNghiPhep.tenNhanVien) {
                this.message.warn(" Họ và tên không được bỏ trống!");
                return;
            }
            if (!this.thongTinNghiPhep.tenCTY) {
                this.message.warn(" Tên công ty không được bỏ trống!");
                return;
            }


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

            if (this.nghiPhep) {
                this.thongTinNghiPhep.nghiPhep = this.nghiPhep;

            }
            else {
                this.thongTinNghiPhep.nghiPhep = 0;
            }
            if (this.congTac) {
                this.thongTinNghiPhep.congTac = this.congTac;

            }
            else {
                this.thongTinNghiPhep.congTac = 0;
            }
            if (this.tangCa) {
                this.thongTinNghiPhep.tangCa = this.tangCa;

            }
            else {
                this.thongTinNghiPhep.tangCa = false;
            }
            this.thongTinNghiPhep.ngayBatDau = moment(this.thongTinNghiPhep.ngayBatDau).utc(true);
            this.thongTinNghiPhep.ngayKetThuc = moment(this.thongTinNghiPhep.ngayKetThuc).utc(true);

            console.log(this.selectedItems)
            for (var j = 0; j < this.selectedItems.length; j++) {
                let file = new QuanLyTrucTiepPNPDto();
             //   file.phieuNghiPhepID = this.selectedItems[j].phieuNghiPhepID;
                file.qltt = this.selectedItems[j];
                console.log(this.thongTinNghiPhep.maNV)
                file.maHoSo = this.thongTinNghiPhep.maNV;

             this.quanLyTrucTiep.push(file);

            }
            this.createOrEditNghiPhepInput.nghiPhep = this.thongTinNghiPhep;

            console.log(this.thongTinNghiPhep)
            for (var j = 0; j < this.selectedRows.length; j++) {
                let file = new LichSuUploadNewDto();
                file.tenFile = this.selectedRows[j].tenFile;
                file.tieuDe = this.selectedRows[j].tieuDe;
                file.dungLuong = this.selectedRows[j].dungLuong;
                this.lichSuUploadList.push(file);
            }
            this.createOrEditNghiPhepInput.quanLyTrucTiepPNP = this.quanLyTrucTiep;
            this.createOrEditNghiPhepInput.lichSuUpLoad = this.lichSuUploadList;
            this._quanLyNghiPhepsServiceProxy.createOrEdit(this.createOrEditNghiPhepInput)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.documentForm.instance.resetValues();
                    this.router.navigate(['/app/main/qlns/quanLyNghiPheps']);
                });

        }
    }

    deleteComment(id: number) {
        this.message.confirm(
            '', this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.listComment = this.listComment.filter(x => x.id !== id);
                    this._lichSuLamViecsServiceProxy.delete(id).subscribe((res) => {
                        //console.log(res);
                    }, (err) => {
                        //console.log(err);
                    }
                    );
                }
            }
        );
    }
    uploadFileComment() {
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');

        this.value.forEach((ele) => {
            if (ele.size / 1024 / 1024 <= 25) {
                if ((ele.name).split('.').reverse()[0] == 'jpg' || (ele.name).split('.').reverse()[0] == 'png' || (ele.name).split('.').reverse()[0] == 'doc' || (ele.name).split('.').reverse()[0] == 'docx' || (ele.name).split('.').reverse()[0] == 'xlsx' || (ele.name).split('.').reverse()[0] == 'pdf') {
                    this.dataDisplay.push({ tenFile: cValue + "/" + this.currentTime + "/" + ele.name, dungLuong: Math.ceil(ele.size / 1024) + "  kb", tieuDe: "" });
                    this.fileDinhKemComment = ele.name;
                    this.fileComment = cValue + "\\" + ele.name;
                }
            }
        });
        // this.value.forEach((x) => {
        //     this.fileDinhKemComment = x.name;
        //     this.fileComment = cValue + "\\" + x.name;
        // });
        this.value.length = 0;
    }


    sendComment() {
        if (this.comment.trim().length == 0)
            return;
        else {
            let l = new LichSuLamViecDto();
            l.noiDung = this.comment;
            l.fullName = this.currentUserFullName;
            l.phieuNghiPhepID = this.phieuNghiPhepId;
            l.creatorUserId = this.currentUserId;
            l.tepDinhKem = this.fileComment;
            l.creationTime = moment(new Date());
            this.thongTinNghiPhep.lastModificationTime = moment(new Date());
            this._lichSuLamViecsServiceProxy.createAndGetIdComment(l).subscribe(res => {
                l.id = res;
                this.listComment.push(l);
                this.notify.info(this.l("Bình luận thành công"));
                // l = new LichSuLamViecDto();
                this.fileComment = null;

            }, (err) => {
                //console.log(err);
            });
            this.comment = '';
            this.fileDinhKemComment = '';
        }
    }

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
