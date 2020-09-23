import { Component, Injector, ViewEncapsulation, ViewChild, OnInit, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuanLyNghiPhepsServiceProxy, QuanLyNghiPhepDto, TruongGiaoDichDto, UngViensServiceProxy, OrganizationUnitServiceProxy, GetQuanLyNghiPhepForViewDto, GetUngVienForViewDto, UserLoginInfoDto, UserLoginServiceProxy, UserServiceProxy, HoSosServiceProxy, CreateOrEditQuanLyNghiPhepDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
//import { CreateOrEditQuanLyNghiPhepModalComponent } from './create-or-edit-quanLyNghiPhep-modal.component';
import { ViewQuanLyNghiPhepModalComponent } from './view-quanLyNghiPhep-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
// import { Table } from 'primeng/components/table/table';
// import { Paginator } from 'primeng/components/paginator/paginator';
// import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { DxDataGridComponent, DxFileUploaderComponent } from 'devextreme-angular';
// import { FileUpload } from 'primeng/primeng';
import { PermissionTreeModalComponent } from '@app/admin/shared/permission-tree-modal.component';
import { UtilityService } from '@shared/utils/UltilityService.service';
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '@shared/AppConsts';
import { LazyLoadEvent } from 'primeng/api';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/primeng';


export class inputFiter {
    maNV: string;
    nghiPhep: number;
    congTac: number;


}

@Component({
    templateUrl: './quanLyNghiPheps.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})


export class QuanLyNghiPhepsComponent extends AppComponentBase implements OnInit {
    [x: string]: any;

    // @ViewChild('createOrEditQuanLyNghiPhepModal', { static: true }) createOrEditQuanLyNghiPhepModal: CreateOrEditQuanLyNghiPhepModalComponent;
    @ViewChild('viewQuanLyNghiPhepModalComponent', { static: true }) viewQuanLyNghiPhepModal: ViewQuanLyNghiPhepModalComponent;
    @ViewChild(DxDataGridComponent, { static: false }) gridContainer: DxDataGridComponent;
    @ViewChild('fileUploader1', { static: false }) uploader1: DxFileUploaderComponent;
    @ViewChild('permissionFilterTreeModal', { static: true }) permissionFilterTreeModal: PermissionTreeModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;



    @Output() inputFiter: EventEmitter<inputFiter> = new EventEmitter<inputFiter>();
    input_Fiter = new inputFiter();
    advancedFiltersAreShown = false;
    filterText = '';
    tenNhanVienFilter = '';
    maxPhongBanFilter: number;
    maxPhongBanFilterEmpty: number;
    minPhongBanFilter: number;
    minPhongBanFilterEmpty: number;
    maNVFilter = '';
    nghiPhepFilter = -1;
    congTacFilter = -1;
    tangCaFilter = -1;
    maxDonViCongTacIDFilter: number;
    minDonViCongTacIDFilter: number;
    maxNgayBatDauFilter: moment.Moment;
    minNgayBatDauFilter: moment.Moment;
    maxNgayKetThucFilter: moment.Moment;
    minNgayKetThucFilter: moment.Moment;
    thongTinNghiPhep: CreateOrEditQuanLyNghiPhepDto = new CreateOrEditQuanLyNghiPhepDto();
    lyDoFilter = '';
    minQuanLyTrucTiepIDFilter: number;
    maxQuanLyTrucTiepIDFilter: number;
    maxTruongBoPhanIDFilter: number;
    minTruongBoPhanIDFilter: number;
    tepDinhKemFilter = '';
    tenCTYFilter = '';
    roleIDFilter: any; ;
    maxNguoiDuyetIDFilter: number;
    minNguoiDuyetIDFilter: number;

    minNgayDuyetFilter: moment.Moment;
    maxNgayDuyetFilter: moment.Moment;
    trangThaiFilter = '';
    currentDate = new Date();
    // fromDate = new Date();
    // toDate = new Date();
    fromDate: any;
    toDate: any;

    maxDay1Filter: moment.Moment;
    minDay1Filter: moment.Moment;
    maxDay2Filter: moment.Moment;
    minDay2Filter: moment.Moment;
    maxDay3Filter: moment.Moment;
    minDay3Filter: moment.Moment;
    parentID: number;
    dataDisplay: any;
    selectedRows = [];
    currentTime: any;
    truongGiaoDich: TruongGiaoDichDto[] = [];
    viTriCongViec: any;
    // viTriCongViec: TruongGiaoDichDto[] = [];
    loaiHopDong: TruongGiaoDichDto[] = [];
    hinhThucLamViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    updateTime: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    gioiTinh: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
    xepLoaiHocLuc: TruongGiaoDichDto[] = [];
    frozenCols: any[];
    selectedRowsData: any[] = [];
    value: any[] = [];
    rootUrl = '';
    tepDinhKemSave = '';
    uploadUrl: any;
    id: any;
    data: any;
    a = true;
    popupVisible = false;
    listUngVien: any;
    day1: any;
    day2: any;
    day3: any;
    xemChiTietNghiPhep = new GetQuanLyNghiPhepForViewDto();
    link: any;
    listDSNghiPhep: any;


    startDateFilter: moment.Moment;
    endDateFilter: moment.Moment;
    isCheckTimeFilter = false;
    lastModificationTimeFilter;
    createTimeFilter: moment.Moment;

    quanLyNghiPhepDto = new QuanLyNghiPhepDto();
    listDataQuanLyNghiPhep: any;

    nghiPhep: number;
    congTac: number;
    maNV: any;
    public selectedName: any;
    hoverName:any;
    trangThaiIDFilter: any;
    trangThaiDuyet = [
        { id: 1, tenTrangThai: 'Đã duyệt' },
        { id: 2, tenTrangThai: 'Chưa duyệt' }

    ];
    donViCongTacIDFilter: number;
    quanLyTrucTiepIDFilter: number;
    truongBoPhanIDFilter: number;
    currentUserId = this.getCurrentUser().id;
    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private _notifyService: NotifyService,
        private ultility: UtilityService,
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _quanLyNghiPhepsServiceProxy: QuanLyNghiPhepsServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private router: Router,
        private _httpClient: HttpClient,
        private _userLoginServiceProxy: UserLoginServiceProxy,
        private _userServiceProxy: UserServiceProxy,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();

        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;

    }
    highlightRow(emp) {
        this.hoverName = emp.quanLyNghiPhep.id;
    }

    lamtuoi() {
        this.maNVFilter = "";
        this.nghiPhepFilter = -1;
        this.tangCaFilter = -1;
        this.congTacFilter = -1;
        this.filterText = "";
        this.trangThaiIDFilter = this.fromDate;
        this.startDateFilter = this.fromDate;
        this.endDateFilter = this.fromDate;
        this.isCheckTimeFilter = false;
        this.maxDay1Filter = this.fromDate;
        this.minDay1Filter = this.fromDate;;
    }

    startEdit(e: any) {
        this.id = e;
        this.selectedName = e;
        if (this.id != null) {
            this.a = false;
        } else {
            this.a = true;
        }
    }

    ngOnInit(): void {
        //this.getCurrentUser();
        this.getListItemSearch();

    }

    getCurrentUser(): UserLoginInfoDto {
       // console.log(this.appSession.userId)
        // this._userLoginServiceProxy.getUserForView(this.appSession.userId).subscribe(res => {
        //     console.log(this.thongTinUser.employeeCode);
        // })

        return this.appSession.user;
    }

    createQuanLyNghiPheps(): void {

        this.router.navigate(['/app/main/qlns/nghiPhep/create']);
    }

    viewDetailUngVien(): void {
        this.router.navigate(['/app/main/qlns/nghiPhep/xemChiTiet/' + this.id]);
    }

    editQuanLyNghiPheps(): void {

        this.router.navigate(['/app/main/qlns/nghiPhep/edit/' + this.id]);
    }

    searchQuanLyNghiPheps() {

        this.getQuanLyNghiPheps();
    }

    xoaFile(e: any) {


        this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
        this.tepDinhKemSave = this.selectedRows.map(x => { return x.tepDinhKem.toString() }).join(';');

    }
    // chức năng xem file chi tiết
    showDetail(e: any) {
        //console.log(e)
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.link = this.rootUrl + "/" + e.data.tepDinhKem;
        //console.log(this.link)
        window.open(this.link, '_blank');
    }


    dowloadTemplate() {

        this.rootUrl = AppConsts.appBaseUrl;
        let link = this.rootUrl + '/assets/sampleFiles/FIleMauImportUngVien.xlsx';
        //console.log(link)
        window.open(link, '_blank');
    }

    getListItemSearch() {
        this._ungViensServiceProxy.getListItemSearch().subscribe(result => {

            this.viTriCongViec = result.viTriCongViec;
            this.tienDoTuyenDung = result.tienDoTuyenDung;
            this.trangThai = result.trangThai;
            this.kenhTuyenDung = result.kenhTuyenDung;
            this.gioiTinh = result.gioiTinh;
            this.tinhTrangHonNhan = result.tinhTrangHonNhan;
            this.trinhDoDaoTao = result.trinhDoDaoTao;
            this.xepLoaiHocLuc = result.xepLoaiHocLuc;
        })
    }

    getQuanLyNghiPheps(event?: LazyLoadEvent) {
        // if (this.primengTableHelper.shouldResetPaging(event)) {
        //     this.paginator.changePage(0);
        //     return;
        // }
        this.a = true;
        this.getCurrentUser();


        this._userLoginServiceProxy.getUserForView(this.appSession.userId).subscribe(res => {

            this.thongTinUser = res.user;
            this.maNVFilter = this.thongTinUser.employeeCode;
            console.log(res)

            this._userServiceProxy.getRoleIdOfUser(this.appSession.userId).subscribe(res => {

                this.roleIDFilter = res;
                console.log(res);
                this._quanLyNghiPhepsServiceProxy.getAll(
                    this.filterText,
                    this.tenNhanVienFilter,
                    this.maNVFilter,
                    this.nghiPhepFilter,
                    this.congTacFilter,
                    this.tangCaFilter,
                    this.maxNgayBatDauFilter,
                    this.minNgayBatDauFilter,
                    this.maxNgayKetThucFilter,
                    this.minNgayKetThucFilter,
                    this.lyDoFilter,
                    this.tepDinhKemFilter,
                    this.tenCTYFilter,
                    this.maxNguoiDuyetIDFilter,
                    this.minNguoiDuyetIDFilter,
                    this.maxNgayDuyetFilter,
                    this.minNgayDuyetFilter,
                    this.trangThaiIDFilter,
                    this.donViCongTacIDFilter,
                    this.quanLyTrucTiepIDFilter,
                    this.isCheckTimeFilter,
                    this.createTimeFilter,
                    this.lastModificationTimeFilter,
                    this.startDateFilter == null ? this.fromDate : this.startDateFilter,
                    this.endDateFilter == null ? this.toDate : this.endDateFilter,
                    this.roleIDFilter,
                    this.primengTableHelper.getSorting(this.dataTable),
                    this.primengTableHelper.getSkipCount(this.paginator, event),
                    // this.primengTableHelper.getMaxResultCount(this.paginator, event)
                    10
                ).subscribe(result => {
                    this.primengTableHelper.totalRecordsCount = result.totalCount;
                    let count = 0;
                    this.listDataQuanLyNghiPhep = result.items;
                    for (var i = 0, len = this.listDataQuanLyNghiPhep.length; i < len; i++) {
                        this.listDataQuanLyNghiPhep[i]["stt"] = ++count;
                    }
                    console.log(this.listDataQuanLyNghiPhep);
                    this.primengTableHelper.records = this.listDataQuanLyNghiPhep;
                });


            console.log(this.appSession.userId)

           

        })
    })
    }

    deleteQuanLyNghiPhep(): void {
        this.message.confirm(
            '', this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._quanLyNghiPhepsServiceProxy.delete(this.id)
                        .subscribe(() => {
                            // this.reloadPage();
                            this.getQuanLyNghiPheps();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }



    // exportToExcel(): void {
    //     this._quanLyNghiPhepsServiceProxy.getQuanLyNghiPhepsToExcel(
    //         this._quanLyNghiPhepsServiceProxy.getAll(
    //             this.filterText,
    //             this.tenNhanVienFilter,
    //             this.maxPhongBanFilter == null ? this.maxPhongBanFilterEmpty: this.maxPhongBanFilter,
    //             this.minPhongBanFilter == null ? this.minPhongBanFilterEmpty: this.minPhongBanFilter,
    //             this.maNVFilter,
    //             this.nghiPhepFilter,
    //             this.congTacFilter,
    //             this.maxNgayBatDauFilter,
    //             this.minNgayBatDauFilter,
    //             this.maxNgayKetThucFilter,
    //             this.minNgayKetThucFilter,
    //             this.lyDoFilter,
    //             this.maxQuanLyTrucTiepIDFilter,
    //             this.minQuanLyTrucTiepIDFilter,
    //             this.maxTruongBoPhanIDFilter,
    //             this.minTruongBoPhanIDFilter,
    //             this.tepDinhKemFilter,
    //             this.tenCTYFilter,
    //             this.maxNguoiDuyetIDFilter,
    //             this.minNguoiDuyetIDFilter,
    //             this.maxNgayDuyetFilter,
    //             this.minNgayDuyetFilter,
    //             this.trangThaiFilter,
    //             "1",
    //             0,
    //             100000
    //     .subscribe(result => {
    //         this._fileDownloadService.downloadTempFile(result);
    //      });
    // }
}
export class Employee {
    public fname: string;
    public lname: string;
    constructor() {
    }
}
