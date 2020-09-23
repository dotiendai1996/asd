import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { HoSosServiceProxy, CreateOrEditHoSoDto, TruongGiaoDichDto, TinhThanhDto, NoiDaoTaoDto, NoiDaoTaosServiceProxy, TinhThanhsServiceProxy, OrganizationUnitDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, DangKyKCBsServiceProxy, HopDongsServiceProxy, TemplatesServiceProxy, TemplateDto, CreateOrEditHoSoInput, LichSuUploadNewDto, LichSuUploadsServiceProxy, CreateOrEditLichSuUploadDto, CreateOrEditQuyTrinhCongTacDto, QuyTrinhCongTacsServiceProxy, UngViensServiceProxy, DangKyKCBDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { OrganizationUnitsTreeComponent, IOrganizationUnitsTreeComponentData } from '@app/admin/shared/organization-unit-tree.component';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import { CustomInputDropdownComponent } from '@app/shared/common/customControl/custom-inputDropdown.component';
import { AppConsts } from '@shared/AppConsts';
import { formatDate } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import * as _ from 'lodash';
import { DxFormComponent } from 'devextreme-angular';
@Component({
    selector: 'editHoSo',
    templateUrl: './edit-hoSo.component.html'
})
export class EditHoSoComponent extends AppComponentBase implements OnInit {

    @ViewChild('documentForm', { static: true }) documentForm: DxFormComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('organizationUnitTree', { static: false }) organizationUnitTree: OrganizationUnitsTreeComponent;
    @ViewChild('inputDropdown', { static: true }) private inputDropdown: CustomInputDropdownComponent;
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    active = false;
    currentTime: any;
    uploadUrl: any;
    saving = false;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    gioiTinh: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    tinhThanh: TinhThanhDto[] = [];
    noiDaoTao: NoiDaoTaoDto[] = [];
    hinhThucLamViec: TruongGiaoDichDto[] = [];
    giongHKTT = false;
    allOrganizationUnits: OrganizationUnitDto[];
    memberedOrganizationUnits: string[];
    totalUnitCount = 0;
    treeData: any;
    uploadUrlImage: any;
    currentDate = new Date();
    rootUrl: string;
    profileImage: any;
    link: string;
    nameArr: any[] = [];
    num: string;
    dataDisplay = [];
    idDV: any;
    hoSoID: any;
    pathFileTemplate: any;
    value: any[] = [];
    idNDT: any;
    cValue: any;
    name: any;
    selectedRows = [];
    popupVisible = false;
    valueImage: any;
    nameNDT: any;
    noiDangKy: any;
    hopDong: any;
    tepDinhKemSave = '';
    nameHD: any;
    idHD: any;
    hoSocheckCMND: any;
    publisherPopupVisible = false;
    hoSo: CreateOrEditHoSoDto = new CreateOrEditHoSoDto();
    hopDongDTO: TruongGiaoDichDto[] = [];
    ngaySinh: Date;
    ngayCap: Date;
    ngayHetHan: Date;
    ngayTapSu: Date;
    ngayThuViec: Date;
    ngayChinhThuc: Date;
    ngayThamGiaBH: Date;
    ngayHetHanBHYT: Date;
    approvE_DT: Date;
    list_year: any[];
    idHN: any;
    idHKTT: any;
    dataRowDetail: any;
    nameKCB: any;
    idKCB: any;
    popupVisibleKCB = false;
    linkHD: any;
    idTH: any;
    namTN: number;
    popupVisibleLHD = false;
    loaiHopDong: any;
    congTy: TruongGiaoDichDto[] = [];
    selected: any;
    nameHopDong: any;
    dateToDateFrom: any;
    quatrinhcongtac = [
        { key: 1, tenCty: 'GSOFT', tungay: '2/7/2019 - 2/9/2019', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Thực tập sinh', bac: '', donvicongtac: 'phòng dự án 1', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Bán thời gian' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/9/2019 - 2/11/2019', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên', bac: '', donvicongtac: 'phòng dự án 1', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Thử việc' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/11/2019 - 2/11/2020', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên chính thức', bac: '', donvicongtac: 'phòng dự án 1', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Đang làm việc' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/11/2020 - 2/5/2021', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên chính thức', bac: '', donvicongtac: 'phòng dự án 2', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Đang làm việc' },
        { key: 1, tenCty: 'GSOFT', tungay: '2/5/2021', vitricongviec: 'Nhân viên lập trình .Net', cap: 'Nhân viên chính thức', bac: '', donvicongtac: 'phòng dự án 2', quanlytructiep: 'Phạm Tri Thức', trangthai: 'Nghỉ việc' },
    ];

    idHopDong: any;
    danhSachCV: any;
    danhSachDV: any;
    tenCongTy: any;
    danhSach_KCB: any;
    tenNoiKCB: any;
    templateLHD: TemplateDto[] = [];
    lichSu_Upload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();
    createOrEditHoSoInput: CreateOrEditHoSoInput = new CreateOrEditHoSoInput();
    quytrinhcongtac: CreateOrEditQuyTrinhCongTacDto = new CreateOrEditQuyTrinhCongTacDto();
    lichSuUploadList: any[] = [];
    donVi: any;
    years: any;
    listYear: [] = [];
    quaTrinhCongTac: any;
    tieuDe: string;
    dateTo: any;
    dateFrom: any;
    tinhThanhIdSelected: number;
    noiDaoTaoIdSelected: number;
    noiDaoTaoList: NoiDaoTaoDto[]= [];
    tinhThanhList: TinhThanhDto[]= [];

    noiKhamBenhList :DangKyKCBDto [] =[];
    templateList : TemplateDto [] = [];
    constructor(
        injector: Injector,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _templatesServiceProxy: TemplatesServiceProxy,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _quyTrinhCongTacsServiceProxy: QuyTrinhCongTacsServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private router: Router,
        private _hopDongsServiceProxy: HopDongsServiceProxy,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private _dangKyKCBsServiceProxy: DangKyKCBsServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _hoSosServiceProxy: HoSosServiceProxy
    ) {
        super(injector);
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
        this.uploadUrlImage = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_fileImage';
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.hoSoID = Number.parseInt(Id);
    }

    ngOnInit(): void {
        this.getTreeDataFromServer();
        this.getListItemSearch();
        this.show();
        this.years = function (startYear) {
            var currentYear = new Date().getFullYear(), years = [];
            startYear = startYear || 2000;
            while (startYear <= currentYear) {
                years.push({ tennam: startYear++ });
            }
            return years;
        }
        this.listYear = this.years(2019 - 20);

    }


    chonDonVi() {
        this.popupVisible = true
    }

    uploadImage() {
        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        this.value.forEach((x) => {
            this.profilePicture = AppConsts.remoteServiceBaseUrl + "\\" + cValue + "\\" + x.name;
        });
        this.value.length = 0;
    }
    chonThongTinKCB() {
        this.popupVisibleKCB = true;
    }

    editQTCongTac(e: any) {
        console.log(e);
        this.tieuDe = "Chỉnh sửa thông tin"
        this.popupVisibleLHD = true;
        this.quytrinhcongtac.dateTo = e.data.quyTrinhCongTac.dateTo;
        this.quytrinhcongtac.dateFrom = e.data.quyTrinhCongTac.dateFrom;
        this.quytrinhcongtac.donViCongTacID = e.data.quyTrinhCongTac.donViCongTacID;
        this.quytrinhcongtac.tenCty = e.data.quyTrinhCongTac.tenCty;
        this.quytrinhcongtac.viTriCongViecCode = e.data.quyTrinhCongTac.viTriCongViecCode;
        this.quytrinhcongtac.trangThaiCode = e.data.quyTrinhCongTac.trangThaiCode;
        this.quytrinhcongtac.ghiChu = e.data.quyTrinhCongTac.ghiChu;
        this.quytrinhcongtac.id = e.data.quyTrinhCongTac.id;



    }

    deleteQTCongTac(e: any): void {
        this.message.confirm(
            '', this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._quyTrinhCongTacsServiceProxy.delete(e.data.quyTrinhCongTac.id)
                        .subscribe(() => {
                            this.danhSachQuaTrinhCongTac();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }




    saveQTCT() {

        this.quytrinhcongtac.dateTo = moment(this.quytrinhcongtac.dateTo).utc(true);
        this.quytrinhcongtac.dateFrom = moment(this.quytrinhcongtac.dateFrom).utc(true);
        this.quytrinhcongtac.maHoSo = this.hoSoID;
        this._quyTrinhCongTacsServiceProxy.createOrEdit(this.quytrinhcongtac).pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                if(this.tieuDe =="Chỉnh sửa thông tin")
                {
                    this.notify.success(this.l("Chỉnh sửa thành công"));
                    this.popupVisibleLHD = false;

                }
                else
                {
                    this.notify.success(this.l("Thêm mới thành công"));
                    this.popupVisibleLHD = false;
                }
               this.danhSachQuaTrinhCongTac();


            }, (err) => {
                if(this.tieuDe =="Chỉnh sửa thông tin")
                {
                    this.notify.info(this.l("Chỉnh sửa không thành công"));
                    this.popupVisibleLHD = true;

                }
                else
                {
                    this.notify.info(this.l("Thêm mới không thành công"));
                    this.popupVisibleLHD = true;
                }

            });
            this.hoSo.tenCty = this.quytrinhcongtac.tenCty;
            this.hoSo.trangThaiLamViecCode = this.quytrinhcongtac.trangThaiCode;
            this.hoSo.viTriCongViecCode = this.quytrinhcongtac.viTriCongViecCode;
            this.hoSo.donViCongTacID= this.quytrinhcongtac.donViCongTacID;
            this.createOrEditHoSoInput.hoSo = this.hoSo;

            this.createOrEditHoSoInput.lichSuUpLoad = this.lichSuUploadList;
            console.log(this.createOrEditHoSoInput)
            this._hoSosServiceProxy.createOrEdit(this.createOrEditHoSoInput)


                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                  this.show();

                });
    }
    startEdit(e: any) {


        console.log(e)
        this.dataRowDetail = e.data;
        this.hoSo.maSoNoiKCB = this.dataRowDetail.maNoiKCB;

        this.hoSo.noiDangKyKCBID = this.dataRowDetail.id;
        this.tenNoiKCB = this.dataRowDetail.tenNoiKCB;
        this.hoSo.maTinhCap = this.dataRowDetail.tinhThanhID;
        this.popupVisibleKCB = false;
    }

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
        this.lichSu_Upload.type = "UV";
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("UV", Id).subscribe(res => {

        })
    }

    nodeSelect(event: any) {
        console.log(event);
    }
    // chức năng  xóa file
    xoaFile(e: any) {

        console.log(this.selectedRows)
        this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
        console.log(this.selectedRows)
        this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');

    }
    // chức năng xem file chi tiết
    showDetail(e: any) {
        console.log(e)
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.link = this.rootUrl + "/" + e.value;
        console.log(this.link)
        window.open(this.link, '_blank');
    }


    showPublisherPopup() {
        this.publisherPopupVisible = true;
    }

    private getTreeDataFromServer(): void {
        let self = this;
        this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
            this.totalUnitCount = result.items.length;
            console.log(result)
            this.treeData = this._arrayToTreeConverterService.createTree(result.items,
                'parentId',
                'id',
                null,
                'children',
                [
                    {
                        target: 'label',
                        targetFunction(item) {
                            return item.displayName;
                        }
                    }, {
                        target: 'expandedIcon',
                        value: 'fa fa-folder-open m--font-warning'
                    },
                    {
                        target: 'collapsedIcon',
                        value: 'fa fa-building-o m--font-warning'
                    },
                    {
                        target: 'selectable',
                        value: true
                    },
                    {
                        target: 'memberCount',
                        targetFunction(item) {
                            return item.memberCount;
                        }
                    },
                    {
                        target: 'roleCount',
                        targetFunction(item) {
                            return item.roleCount;
                        }
                    }
                ]);

        });
    }

    onChangeGiongHKTTCheckBox(e) {
        console.log(e)
        if (e.value == true) {
            this.hoSo.diaChiHN = this.hoSo.diaChiHKTT;
            this.hoSo.tinhThanhIDHN = this.hoSo.tinhThanhIDHKTT;
            this.hoSo.quocGiaHN = this.hoSo.quocGiaHKTT;
        }

        else {
            this.hoSo.diaChiHN = "";
            this.hoSo.tinhThanhIDHKTT = null;
            this.hoSo.quocGiaHN = "";
        }

    }

  

    chonLoaiHopDong(e: any) {
        console.log(e)
        this.hoSo.hopDongHienTai = e.data.id;
        this.idHopDong = e.data.id;
        this.nameHopDong = e.data.tenTemplate;
        this.linkHD = e.data.linkTemplate;
        this.popupVisibleLHD = false;
    }
    chonLoaiHD() {
        this.tieuDe = "Thêm mới thông tin"
        this.popupVisibleLHD = true;
    }
    changeCTY(e) {
        console.log(e.value)
        this._templatesServiceProxy.getListTemplate(e.value).subscribe(res => {

            this.loaiHopDong = res;
            let count = 0;

            for (var i = 0, len = this.loaiHopDong.length; i < len; i++) {
                this.loaiHopDong[i]["stt"] = ++count;
                console.log(this.loaiHopDong)
            }
        })


        this.danhSachDV = this.treeData.filter(x => x.label == e.value)[0].children;
        console.log(this.danhSachDV)
    }
    selectItem(e) {

        console.log(e)
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {
            this.danhSachCV = res;
            console.log(this.danhSachCV)

        })


    }

    getListItemSearch() {
        this._ungViensServiceProxy.getListItemSearch().subscribe(result => {
            this.noiDaoTaoList = result.noiDaoTaoList;
            this.tinhThanhList = result.tinhThanhList;
            this.templateList = result.templateList;
            this.noiKhamBenhList=result.noiKhamBenhList;
            this.viTriCongViec = result.viTriCongViec;
            this.tienDoTuyenDung = result.tienDoTuyenDung;
            this.trangThai = result.trangThai;
            this.kenhTuyenDung = result.kenhTuyenDung;
            this.gioiTinh = result.gioiTinh;
            this.hinhThucLamViec = result.tinhTrangNhanSu;
            this.tinhTrangHonNhan = result.tinhTrangHonNhan;
            this.trinhDoDaoTao = result.trinhDoDaoTao;
            this.xepLoaiHocLuc = result.xepLoaiHocLuc;
            this.congTy=result.congty ;
        });
    }
   

    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.hoSoID = Number.parseInt(Id);
        this._hoSosServiceProxy.getHoSoForEdit(this.hoSoID).subscribe(result => {
            this.hoSo = result.hoSo;
            console.log(this.hoSo)
        
            this.danhSachDV = this.treeData.filter(x => x.label == this.hoSo.tenCty)[0].children;
            console.log(this.danhSachDV);
            this.danhSachCV = result.danhSachCV;
            this.selectedRows=result.lichSuUpload;
            this.quaTrinhCongTac=result.quaTrinhCongTac;
            // this._hoSosServiceProxy.getAllCongViec(this.hoSo.donViCongTacID).subscribe(res => {
            //     this.danhSachCV = res;
            // })

            // this._lichSuUploadsServiceProxy.getListLichSuUploadDto("HS", this.hoSoID).subscribe(res => {
            //     this.selectedRows = res;
            // })
            this.profilePicture = this.hoSo.anhDaiDien;

        });
        // this.danhSachQuaTrinhCongTac();

    }

    danhSachQuaTrinhCongTac() {
        this._quyTrinhCongTacsServiceProxy.getAll(this.hoSoID).subscribe(res => {
            this.quaTrinhCongTac = res.items;
            console.log(this.quaTrinhCongTac)
        })

    }

    troVe() {
        this.router.navigate(['/app/main/qlns/hoSos']);
    }
    save(): void {
        let result = this.documentForm.instance.validate();
        if (result.isValid && this.validateNgaySinh(this.hoSo.ngaySinh)) {
        this.lichSuUploadList.length = 0;

        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.createOrEditHoSoInput.hoSo = this.hoSo;
        this.lichSu_Upload.type = "HS"
        this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("HS", Id).subscribe(res => {

        })
        if (this.selectedRows)
            this.selectedRows.forEach(ele => {
                this.lichSu_Upload.tenFile = ele.tenFile;
                this.lichSu_Upload.tieuDe = ele.tieuDe;
                this.lichSu_Upload.dungLuong = ele.dungLuong;
                this.lichSu_Upload.type = "HS";
                this.lichSu_Upload.typeID = Id;
                this.lichSuUploadList.push(this.lichSu_Upload);
                this._lichSuUploadsServiceProxy.createOrEdit(this.lichSu_Upload).pipe(finalize(() => { this.saving = false; }))
                    .subscribe(() => {

                    });
            });


        this._hoSosServiceProxy.getAllCMND().subscribe(res => {
            if (this.hoSo.soCMND) {
                this.hoSocheckCMND = res;
                if (this.hoSocheckCMND.findIndex(x => x.soCMND == this.hoSo.soCMND && x.id !== this.hoSoID) > -1) {
                    this.message.warn("Số CMND đã bị trùng!");
                    return;
                }
            }
        })

        this.saving = true;
        if (!this.hoSo.hoVaTen) {
            this.message.warn("Họ tên không được bỏ trống!");
            return;
        }
        if (!this.hoSo.soCMND) {
            this.message.warn("CMND không được bỏ trống!");
            return;
        }
        if (!this.hoSo.donViCongTacID) {
            this.message.warn("Đơn vị công tác không được bỏ trống!");
            return;
        }
        if (!this.hoSo.viTriCongViecCode) {
            this.message.warn("Vị trí công việc không được bỏ trống!");
            return;
        }
        if (!this.hoSo.gioiTinhCode) {
            this.message.warn("Giới tính không được bỏ trống!");
            return;
        }
        if (!this.hoSo.quocTich) {
            this.message.warn("Quốc tịch không được bỏ trống!");
            return;
        }
        if (!this.hoSo.danToc) {
            this.message.warn("Dân tộc không được bỏ trống!");
            return;
        }
        if (!this.hoSo.ngaySinh) {
            this.message.warn("Ngày sinh không được bỏ trống!");
            return;
        }
     
       
        if (!this.hoSo.loaiHopDongID) {
            this.message.warn("Loại HD không được bỏ trống!");
            return;
        }
        
        if (this.profilePicture) {
            this.hoSo.anhDaiDien = this.profilePicture;
        }


        if (this.idKCB) {
            this.hoSo.noiDangKyKCBID = this.idKCB;
        }
        if (this.tepDinhKemSave) {
            this.hoSo.tepDinhKem = this.tepDinhKemSave;
        }

        this.hoSo.ngaySinh = moment(this.hoSo.ngaySinh).utc(true);
        this.hoSo.ngayCap = moment(this.hoSo.ngayCap).utc(true);
        this.hoSo.ngayHetHan = moment(this.hoSo.ngayHetHan).utc(true);
        this.hoSo.ngayTapSu = moment(this.hoSo.ngayTapSu).utc(true);
        this.hoSo.ngayThuViec = moment(this.hoSo.ngayThuViec).utc(true);
        this.hoSo.ngayChinhThuc = moment(this.hoSo.ngayChinhThuc).utc(true);

        this.createOrEditHoSoInput.hoSo = this.hoSo;
        for (var j = 0; j < this.selectedRows.length; j++) {
            let file = new LichSuUploadNewDto();
            file.tenFile = this.selectedRows[j].tenFile;
            file.tieuDe = this.selectedRows[j].tieuDe;
            file.dungLuong = this.selectedRows[j].dungLuong;
            this.lichSuUploadList.push(file);
        }
        this.createOrEditHoSoInput.lichSuUpLoad = this.lichSuUploadList;
        console.log(this.createOrEditHoSoInput)
        this._hoSosServiceProxy.createOrEdit(this.createOrEditHoSoInput)


            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.router.navigate(['/app/main/qlns/hoSos']);
                this.modalSave.emit(null);
            });
        }
    }

    validateNgaySinh(event: any){
        var dateNow = moment().startOf('day').utc(true);
        var ngaySinhDate = moment(event).utc(true);
        if(dateNow.year() - ngaySinhDate.year() <= 18){
            this.notify.error(this.l('Ứng viên lớn hơn 18 tuổi'));
            return false;
        }
        return true;
    }
}
