import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DxFormComponent, DxDataGridComponent, DxButtonComponent, DxSwitchComponent, DxNumberBoxComponent, DxDateBoxComponent, DxSelectBoxComponent, DxFileUploaderComponent } from 'devextreme-angular';
import { HopDongsServiceProxy, CreateOrEditHopDongDto, TruongGiaoDichDto, HoSosServiceProxy, DangKyKCBsServiceProxy, TemplatesServiceProxy, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, CreateOrEditHopDongInput, LichSuUploadNewDto, CreateOrEditLichSuUploadDto, LichSuUploadsServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { formatDate } from '@angular/common';
import { AppConsts } from '@shared/AppConsts';
import { ActivatedRoute, Router } from '@angular/router';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import { result } from 'lodash';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/primeng';

@Component({
    selector: 'editHopDong',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './edit-hoDong.component.html'
})
export class EditHopDongComponent extends AppComponentBase implements OnInit {
    @ViewChild('documentForm', { static: true }) documentForm: DxFormComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('fileUploader1', { static: false }) fileUploader1: DxFileUploaderComponent;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    active = false;
    saving = false;
    popupVisibleHoSo = false;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    // hopDong: hopDongDto[] = [];
    viTriCongViec: TruongGiaoDichDto[] = [];
    loaiHopDong: any;
    hinhThucLamViec: TruongGiaoDichDto[] = [];
    thoiHanHopDong: TruongGiaoDichDto[] = [];
    linkHD: any;
    ngaySinh: Date;
    ngayCap: Date;
    popupVisible = false;
    time: string;
    uploadUrl: string;
    thoihanHD: any;

    rootUrl: any;
    link: any;
    hoso: any;
    name: any;
    idDV: any;
    selectedRows = [];
    userId: number;
    currentDate = new Date();
    nameArr: any[] = [];
    dataDisplay = [];
    value: any[] = [];
    hopDongId: any;
    dataRowDetail: any;
    currentTime: any;
    nameNhanVien: any;
    tepDinhKemSave = '';
    cmndExists = false;
    hopDong: CreateOrEditHopDongDto = new CreateOrEditHopDongDto();
    createOrEditHopDongInput: CreateOrEditHopDongInput = new CreateOrEditHopDongInput();
    ngayKy: Date;
    ngayCoHieuLuc: Date;
    ngayHetHan: Date;
    vitriCV: any;
    approvE_DT: Date;
    donViCongtac: any;
    isReadonly = true;
    congTY: any;
    num: any;
    donViCOngTacID: any;
    popupVisibleViTriCV = false;
    popupVisibleLHD = false;
    selected: any;
    maHD: any;
    tenHD: any;
    tenLoaiHD: any;
    listDataHoSo: any;
    treeData: any;
    danhSachCV: any;
    danhSachDV: any;




    advancedFiltersAreShown = false;
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
    tenCTY: any;
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
    loaiHD: any;
    totalUnitCount = 0;
    chude: any;
    congty: any;
    gioiTinh: any;
    lichSu_Upload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();
    trangThai: any;
    selectedRowsData: any[] = [];
    congTy = [
        { key: 1, type: 'GSOFT' },
        { key: 2, type: 'GOBRANDING' },

        // {key: 4, type: 'Năm'}
    ];
    lichSuUploadList: any[] = [];
    constructor(
        injector: Injector,
        private router: Router,

        private _hoSosServiceProxy: HoSosServiceProxy,
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _dangKyKCBsServiceProxy: DangKyKCBsServiceProxy,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _templatesServiceProxy: TemplatesServiceProxy,
        private _hopDongsServiceProxy: HopDongsServiceProxy
    ) {
        super(injector);
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;
    }



    ngOnInit(): void {

        this.getTreeDataFromServer();
        this.getHoSos();
        this.show();

    }

    showPublisherPopup() {
        this.popupVisibleHoSo = true;
        this.isReadonly = !this.isReadonly;

    }


    changeCTY(e) {
        console.log(e.value)
        this._templatesServiceProxy.getListTemplate(e.value).subscribe(res => {

            this.loaiHopDong = res;

        })
        this.danhSachDV = this.treeData.filter(x => x.label == e.value)[0].children;
        console.log(this.danhSachDV)
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

    selectItem(e) {
        this._hoSosServiceProxy.getAllCongViec(e.value).subscribe(res => {

            this.danhSachCV = res;
            console.log(this.danhSachCV)

        })

    }


    chonDonViCongTac() {
        this.popupVisible = true;

    }

    startEdit(e: any) {

        console.log(e)
        this.dataRowDetail = e;
        // this.isReadonly = !this.isReadonly;
        console.log(this.dataRowDetail)
        this.hopDong.hoTenNhanVien = this.dataRowDetail.hoVaTen;
        this.hopDong.nhanVienId = this.dataRowDetail.maNhanVien;

        this.hopDong.viTriCongViecCode = this.dataRowDetail.viTriCongViecCode;
        this.hopDong.luongDongBaoHiem = this.dataRowDetail.luongDongBH;
        this.hopDong.loaiHopDongID = this.dataRowDetail.loaiHopDongID;
        if (this.dataRowDetail.loaiHopDongID) {
            this._templatesServiceProxy.getTemplateForView(this.dataRowDetail.loaiHopDongID).subscribe(res => {

                this.linkHD = res.template.linkTemplate;

            })
        }
        else {
            this.linkHD = " ";
        }

        this.hopDong.hinhThucLamViecCode = this.dataRowDetail.trangThaiLamViecCode;
        this.hopDong.tyLeHuongLuong = this.dataRowDetail.tyLeDongBH;
        this.hopDong.donViCongTacID = this.dataRowDetail.donViCongTacID
        this.hopDong.tenCTY = this.dataRowDetail.tenCty;
        this.hopDong.luongCoBan = this.dataRowDetail.luongCoBan;
        this.popupVisibleHoSo = false;
    }


    chonLoaiHD() {
        this.popupVisibleLHD = true;
    }

    chonLoaiHopDong(e: any) {
        console.log(e)
        if (e.value != null) {
            this._templatesServiceProxy.getTemplateForView(e.value).subscribe(res => {

                console.log(res)
                this.hopDong.loaiHopDongID = res.template.id;
                this.linkHD = res.template.linkTemplate;
            })
        }
        else {

            this.linkHD = "";
        }

    }


    show(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.getAllTruongGiaoDich().subscribe((res) => {
            this.truongGiaoDich = res;

            this.thoiHanHopDong = this.truongGiaoDich.filter(x => x.code == 'THHD');


        });
        if (Id != null) {
            this.hopDongId = Number.parseInt(Id);
            this._hopDongsServiceProxy.getHopDongForEdit(this.hopDongId).subscribe(result => {
                console.log(result);
                this.hopDong = result.hopDong;

                this.danhSachCV = result.congViecList;
                this.chude = result.templateList;
                this.trangThai = result.trangThai;
                this.congty = result.congty;
                this.gioiTinh = result.gioiTinh;
                this.hinhThucLamViec = result.hinhThucLamViec;
                // this.thoiHanHopDong = result.thoiHanHopDong;
                this.selectedRows = result.lichSuUploadList;
                if (result.organizationUnitList && result.organizationUnitList.items) {
                    var treeData = this._arrayToTreeConverterService.createTree(result.organizationUnitList.items,
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
                    this.danhSachDV = treeData.filter(x => x.label == this.hopDong.tenCTY)[0].children;
                    this._hoSosServiceProxy.getAllCongViec(this.hopDong.donViCongTacID).subscribe(res => {
                        this.danhSachCV = res;
                    })
                }

            });
        }
    }

    onValueChanged(e) {
        console.log(e)
        this.selected = e.value;
        this.loaiHopDong = this.truongGiaoDich.filter(x => x.ghiChu == this.selected);
        console.log(this.loaiHopDong)
        this._templatesServiceProxy.getListTemplate(this.selected).subscribe(res => {

            this.loaiHopDong = res;
            let count = 0;

            for (var i = 0, len = this.loaiHopDong.length; i < len; i++) {
                this.loaiHopDong[i]["stt"] = ++count;
                console.log(this.loaiHopDong)
            }
        })
    }

    troVe() {
        this.router.navigate(['/app/main/qlns/hopDongs']);
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
    showDetail(e: any) {
        console.log(e)
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.link = this.rootUrl + "/" + e.value;
        console.log(this.link)
        window.open(this.link, '_blank');
    }
    xoaFile(e: any) {

        console.log(this.selectedRows)
        this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
        console.log(this.selectedRows)
        this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');

    }

    save(): void {
        let result = this.documentForm.instance.validate();
        if (result.isValid) {
            this.saving = true;
            this.lichSuUploadList.length = 0;

            const Id = this._activatedRoute.snapshot.paramMap.get('id');
            this.createOrEditHopDongInput.hopDong = this.hopDong;
            this.lichSu_Upload.type = "HD"
            this._lichSuUploadsServiceProxy.xoaLichSuUpLoadToID("HS", Id).subscribe(res => {

            })
            if (this.selectedRows)
                this.selectedRows.forEach(ele => {
                    this.lichSu_Upload.tenFile = ele.tenFile;
                    this.lichSu_Upload.tieuDe = ele.tieuDe;
                    this.lichSu_Upload.dungLuong = ele.dungLuong;
                    this.lichSu_Upload.type = "HD";
                    this.lichSu_Upload.typeID = Id;
                    this.lichSuUploadList.push(this.lichSu_Upload);
                    this._lichSuUploadsServiceProxy.createOrEdit(this.lichSu_Upload).pipe(finalize(() => { this.saving = false; }))
                        .subscribe(() => {

                        });
                });

            this.hopDong.linkTemplate = this.linkHD;

            if (this.tepDinhKemSave) {
                this.hopDong.tepDinhKem = this.tepDinhKemSave;
            }
            if (this.maHD) {
                this.hopDong.loaiHopDongCode = this.tenLoaiHD;
            }

            if (!this.hopDong.tenHopDong) {
                this.message.warn(" Tên hợp đồng không được bỏ trống!");
                return;
            }
            if (!this.hopDong.nhanVienId) {
                this.message.warn(" Mã nhân viên không được bỏ trống!");
                return;
            }
            if (!this.hopDong.luongCoBan) {
                this.message.warn(" Lương cơ bản không được bỏ trống!");
                return;
            }
            if (!this.hopDong.ngayKy) {
                this.message.warn(" Ngày ký HD không được bỏ trống!");
                return;
            }
            if (!this.hopDong.ngayCoHieuLuc) {
                this.message.warn(" Ngày có hiệu lực không được bỏ trống!");
                return;
            }
            if (!this.hopDong.ngayHetHan) {
                this.message.warn(" Ngày hết hạn không được bỏ trống!");
                return;
            }
            if (!this.hopDong.loaiHopDongID) {
                this.message.warn(" Loại hợp đồng không được bỏ trống!");
                return;
            }
            if (!this.hopDong.hoTenNhanVien) {
                this.message.warn(" Họ tên không được bỏ trống!");
                return;
            }
            if (!this.hopDong.donViCongTacID) {
                this.message.warn("Đơn vị công tác không được bỏ trống!");
                return;
            }
            if (!this.hopDong.viTriCongViecCode) {
                this.message.warn("Vị trí công việc không được bỏ trống!");
                return;
            }

            this.hopDong.ngayKy = moment(this.hopDong.ngayKy).utc(true);
            this.hopDong.ngayCoHieuLuc = moment(this.hopDong.ngayCoHieuLuc).utc(true);
            this.hopDong.ngayHetHan = moment(this.hopDong.ngayHetHan).utc(true);
            this.createOrEditHopDongInput.hopDong = this.hopDong;
            for (var j = 0; j < this.selectedRows.length; j++) {
                let file = new LichSuUploadNewDto();
                file.tenFile = this.selectedRows[j].tenFile;
                file.tieuDe = this.selectedRows[j].tieuDe;
                file.dungLuong = this.selectedRows[j].dungLuong;
                this.lichSuUploadList.push(file);
            }
            this.createOrEditHopDongInput.lichSuUpLoad = this.lichSuUploadList;
            this._hopDongsServiceProxy.createOrEdit(this.createOrEditHopDongInput)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    // this.hopDong = new CreateOrEditHopDongDto();
                    this.router.navigate(['/app/main/qlns/hopDongs']);
                    this.modalSave.emit(null);

                });

        }
    }


    close(): void {

        this.active = false;

    }


    getHoSos(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }
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
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)||5
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            let count = 0;
            this.listDataHoSo = result.items;
            for (var i = 0, len = this.listDataHoSo.length; i < len; i++) {
                this.listDataHoSo[i]["stt"] = ++count;
            }
            console.log(this.listDataHoSo);
            this.primengTableHelper.records = this.listDataHoSo;

        });
    }

}
