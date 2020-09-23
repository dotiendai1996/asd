import { Component, Injector, ViewEncapsulation, ViewChild, OnInit, ElementRef, Output, EventEmitter, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UngViensServiceProxy, UngVienDto, TruongGiaoDichDto, GetUngVienForViewDto, OrganizationUnitServiceProxy, CreateOrEditUngVienInput, NoiDaoTaoDto, TinhThanhDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { ViewUngVienModalComponent } from './view-ungVien-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import { HttpClient } from '@angular/common/http';
import { FileUpload } from 'primeng/fileupload';
import { PermissionTreeModalComponent } from '@app/admin/shared/permission-tree-modal.component';
import { finalize } from 'rxjs/operators';
import { formatDate } from '@angular/common';
import { DxFileUploaderComponent } from 'devextreme-angular';
import { DxFileManagerModule, DxPopupModule, DxDataGridComponent } from 'devextreme-angular';
import { UtilityService } from '@shared/utils/UltilityService.service';
import { NgForm, Form } from '@angular/forms';
import { LocalStorageService } from '@shared/utils/local-storage.service';

@Component({
    selector: 'ungVienUI',
    templateUrl: './ungViens.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class UngViensComponent extends AppComponentBase {


    @ViewChild('form', { static: true }) form: Form;
    @ViewChild(DxDataGridComponent, { static: false }) gridContainer: DxDataGridComponent;
    @ViewChild('viewUngVienmodal.component', { static: true }) viewUngVienModal: ViewUngVienModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @ViewChild('ExcelFileUpload', { static: true }) excelFileUpload: FileUpload;
    @ViewChild('fileUploader1', { static: false }) uploader1: DxFileUploaderComponent;

    @Output() onBack: EventEmitter<GetUngVienForViewDto> = new EventEmitter<GetUngVienForViewDto>();
    @ViewChild('permissionFilterTreeModal', { static: true }) permissionFilterTreeModal: PermissionTreeModalComponent;
    advancedFiltersAreShown = false;
    filterText = '';
    maUngVienFilter = '';
    hoVaTenFilter = '';
    viTriUngTuyenCodeFilter = '';
    kenhTuyenDungCodeFilter = '';
    gioiTinhCodeFilter = '';
    trangThaiCodeFilter = '';
    maxNgaySinhFilter: moment.Moment;
    minNgaySinhFilter: moment.Moment;
    startDateFilter: moment.Moment;
    day1Filter: moment.Moment;
    day2Filter: moment.Moment;
    day3Filter: moment.Moment;
    endDateFilter: moment.Moment;
    lastModificationTimeFilter;
    createTimeFilter: moment.Moment;
    isCheckTimeFilter = false;
    soCMNDFilter = '';
    trinhDoVanHoaFilter = '';
    maxNamTotNghiepFilter: number;
    maxNamTotNghiepFilterEmpty: number;
    minNamTotNghiepFilter: number;
    minNamTotNghiepFilterEmpty: number;
    tienDoTuyenDungCodeFilter = '';
    recorD_STATUSFilter = '';
    maxMARKER_IDFilter: number;
    currentDate = new Date();

    fromDate1: any;
    fromDate2: any;
    fromDate3: any;
    toDate: any;
    maxMARKER_IDFilterEmpty: number;
    minMARKER_IDFilter: number;
    minMARKER_IDFilterEmpty: number;
    autH_STATUSFilter = '';
    dienThoaiFilter = '';
    emailFilter = '';
    diaChiFilter = '';
    listDataUngVien: any;
    time1Filter = '';
    time2Filter = '';
    time3Filter = '';
    link: string;
    createOrEditUngVienInput: CreateOrEditUngVienInput = new CreateOrEditUngVienInput();
    noteFilter = '';
    getUngVienForViewDto: GetUngVienForViewDto = new GetUngVienForViewDto();


    tenCTYFilter = '';
    maxDay1Filter: moment.Moment;
    minDay1Filter: moment.Moment;
    maxDay2Filter: moment.Moment;
    minDay2Filter: moment.Moment;
    maxDay3Filter: moment.Moment;
    minDay3Filter: moment.Moment;
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
    noiDaoTaoList: NoiDaoTaoDto[] = [];
    tinhThanhList: TinhThanhDto[] = [];
    xemChiTietUngVien = new GetUngVienForViewDto();
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
    gioitinh: any;
    fromDate: any;
    ungVienConts = 'ungVien.Filter';
    public selectedName: any;
    public hoverName: any;
    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private _notifyService: NotifyService,
        private ultility: UtilityService,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _localStorageService: LocalStorageService,
        private router: Router,
        private _httpClient: HttpClient,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;

    }



    lamtuoi() {

        this.viTriUngTuyenCodeFilter = "";
        this.kenhTuyenDungCodeFilter = "";
        this.gioiTinhCodeFilter = "";
        this.tienDoTuyenDungCodeFilter = "";
        this.trangThaiCodeFilter = "";
        this.startDateFilter = null;
        this.endDateFilter = null;
        this.isCheckTimeFilter = false;
        // this.day1Filter = null;
        // this.day3Filter = null;
        // this.day2Filter = null;
        this.filterText = "";
        this.maxDay1Filter = this.fromDate;
        this.minDay1Filter = this.fromDate;
        this.maxDay2Filter = this.fromDate;
        this.minDay2Filter = this.fromDate;
        this.maxDay3Filter = this.fromDate;
        this.minDay3Filter = this.fromDate;
    }

    checkBoxToggled(e: any) {
        this.getUngViens();
    }

    getSelectedRowKeys() {
        this.selectedRowsData = this.gridContainer.instance.getSelectedRowsData();
        if (this.selectedRowsData.length == 0) {
            return;
        }
        this.selectedRowsData.forEach(element => {
            this.selectedRows.push(element["Id"]);
        });
        this.ultility.selectedRows = this.selectedRows;
        //console.log(this.selectedRows)
    }

    ngOnInit(): void {

        this.getListItemSearch();
        this._localStorageService.getItem('ungVien.Filter', (err, filters) => {
            this.bindingToFilter(filters);
            this.getUngViens();
        });
    }
    bindingToFilter(filters: any) {
        if (filters) {
            this.toDate = filters.toDate;
            this.fromDate = filters.fromDate;
            this.viTriUngTuyenCodeFilter = filters.viTriUngTuyenCodeFilter;
            this.kenhTuyenDungCodeFilter = filters.kenhTuyenDungCodeFilter;
            this.gioiTinhCodeFilter = filters.gioiTinhCodeFilter;
            this.tienDoTuyenDungCodeFilter = filters.tienDoTuyenDungCodeFilter;
            this.trangThaiCodeFilter = filters.trangThaiCodeFilter;
            this.startDateFilter = filters.startDateFilter==null ? null : moment(filters.startDateFilter);
            this.endDateFilter = filters.endDateFilter==null ? null : moment(filters.endDateFilter);
            this.isCheckTimeFilter = filters.isCheckTimeFilter;
            this.filterText = filters.filterText;
            this.maxDay1Filter = filters.maxDay1Filter==null ? null : moment(filters.maxDay1Filter);
            this.minDay1Filter = filters.minDay1Filter==null ? null : moment(filters.minDay1Filter);
            this.maxDay2Filter = filters.maxDay2Filter==null ? null : moment(filters.maxDay2Filter);
            this.minDay2Filter = filters.minDay2Filter==null ? null : moment(filters.minDay2Filter);
            this.maxDay3Filter = filters.maxDay3Filter==null ? null : moment(filters.maxDay3Filter);
            this.minDay3Filter = filters.minDay3Filter==null ? null : moment(filters.minDay3Filter);
        }
    }
    parseToFiltersDto() {
        return {
            toDate : this.toDate,
            fromDate : this.fromDate,
            viTriUngTuyenCodeFilter: this.viTriUngTuyenCodeFilter,
            kenhTuyenDungCodeFilter: this.kenhTuyenDungCodeFilter,
            gioiTinhCodeFilter: this.gioiTinhCodeFilter,
            tienDoTuyenDungCodeFilter: this.tienDoTuyenDungCodeFilter,
            trangThaiCodeFilter: this.trangThaiCodeFilter,
            startDateFilter: this.startDateFilter,
            endDateFilter: this.endDateFilter,
            isCheckTimeFilter: this.isCheckTimeFilter,
            filterText: this.filterText,
            maxDay1Filter: this.maxDay1Filter,
            minDay1Filter: this.minDay1Filter,
            maxDay2Filter: this.maxDay2Filter,
            minDay2Filter: this.minDay2Filter,
            maxDay3Filter: this.maxDay3Filter,
            minDay3Filter: this.minDay3Filter
        }
    }



    // getUngViens(): void {
    //     this.a = true;
    //     this._ungViensServiceProxy.getAll(
    //         this.filterText,
    //         this.maUngVienFilter,
    //         this.hoVaTenFilter,
    //         this.viTriUngTuyenCodeFilter,
    //         this.kenhTuyenDungCodeFilter,
    //         this.gioiTinhCodeFilter,
    //         this.trangThaiCodeFilter,
    //         this.startDateFilter == null ? this.fromDate : this.startDateFilter,
    //         this.endDateFilter == null ? this.toDate : this.endDateFilter,
    //         this.day1Filter == null ? this.fromDate1 : this.day1Filter,
    //         this.day2Filter == null ? this.fromDate2 : this.day2Filter,
    //         this.day3Filter == null ? this.fromDate3 : this.day3Filter,
    //         this.isCheckTimeFilter,
    //         this.createTimeFilter,
    //         this.lastModificationTimeFilter,
    //         this.tienDoTuyenDungCodeFilter,
    //         "1",
    //         0,
    //         100000

    //     ).subscribe(result => {
    //         console.log(result);
    //         this.listUngVien = result.items;

    //     });

    // }


    getUngViens(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }
        this._localStorageService.setItem(this.ungVienConts, this.parseToFiltersDto());
        this._ungViensServiceProxy.getAll(
            this.filterText,
            this.maUngVienFilter,
            this.hoVaTenFilter,
            this.viTriUngTuyenCodeFilter,
            this.kenhTuyenDungCodeFilter,
            this.gioiTinhCodeFilter,
            this.trangThaiCodeFilter,
            this.startDateFilter == null ? this.fromDate : this.startDateFilter,
            this.endDateFilter == null ? this.toDate : this.endDateFilter,
            // this.day1Filter == null ? this.fromDate1 : this.day1Filter,
            // this.day2Filter == null ? this.fromDate2 : this.day2Filter,
            // this.day3Filter == null ? this.fromDate3 : this.day3Filter,
            this.isCheckTimeFilter,
            this.createTimeFilter,
            this.lastModificationTimeFilter,
            this.tienDoTuyenDungCodeFilter,
            this.minDay1Filter == null ? this.fromDate : this.minDay1Filter,
            this.maxDay1Filter == null ? this.fromDate : this.maxDay1Filter,
            this.minDay2Filter == null ? this.fromDate : this.minDay2Filter,
            this.maxDay2Filter == null ? this.fromDate : this.maxDay2Filter,
            this.minDay3Filter == null ? this.fromDate : this.minDay3Filter,
            this.maxDay3Filter == null ? this.fromDate : this.maxDay3Filter,


            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.listDataUngVien = result.items;
            this.primengTableHelper.records = this.listDataUngVien;
        });

    }
    
    public highlightRow(emp) {
        this.hoverName = emp.ungVien.id;
    }


    getListItemSearch() {
        this._ungViensServiceProxy.getListItemSearch().subscribe(result => {
            this.noiDaoTaoList = result.noiDaoTaoList;
            this.tinhThanhList = result.tinhThanhList;
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

    createUngVien(): void {
        this.router.navigate(['/app/main/qlns/ungViens/create']);
    }

    viewDetailUngVien(): void {
        this.router.navigate(['/app/main/qlns/ungVien/xemChiTiet/' + this.id]);
    }

    editUngVien(): void {

        this.router.navigate(['/app/main/qlns/ungViens/edit/' + this.id]);
    }

    searchUngVien() {

        this.getUngViens();
    }

    deleteUngVien(): void {
        this.message.confirm(
            '', this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._ungViensServiceProxy.delete(this.id)
                        .subscribe(() => {
                            // this.reloadPage();
                            this.getUngViens();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    startEdit(e: any) {

        this.id = e;
        this.selectedName = e;
        if (this.id != null) {// && this.data!=null && this.xemChiTietUngVien!=null
            this.a = false;
        } else {
            this.a = true;
        }

    }

    xoaFile(e: any) {

        //console.log(this.selectedRows)
        this.selectedRows.splice(this.selectedRows.indexOf(e.row.data.tepDinhKem), 1);
        //console.log(this.selectedRows)
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
    importToExcel() {

        const cValue = formatDate(this.currentDate, 'dd-MM-yyyy', 'en-US');
        this.value.forEach((ele) => {
            this.dataDisplay = ele.name
            this._ungViensServiceProxy.importToExcel(this.currentTime, this.dataDisplay).subscribe(res => {
                if (res == null) {
                    this.message.success("Import thành công");
                    this.getUngViens();
                }
                else {
                    this.message.info("Nhập liệu chưa đúng");
                }

            })
            this.value.length = 0
        });

    }

    dowloadTemplate() {

        this.rootUrl = AppConsts.appBaseUrl;
        let link = this.rootUrl + '/assets/sampleFiles/FIleMauImportUngVien.xlsx';
        //console.log(link)
        window.open(link, '_blank');
    }
    // exportToExcel(): void {
    //     this._ungViensServiceProxy.getUngViensToExcel(
    //         this.filterText,
    //         this.maUngVienFilter,
    //         this.hoVaTenFilter,
    //         this.viTriUngTuyenCodeFilter,
    //         this.kenhTuyenDungCodeFilter,
    //         this.gioiTinhCodeFilter,
    //         this.trangThaiCodeFilter,
    //         this.maxNgaySinhFilter,
    //         this.minNgaySinhFilter,
    //         this.soCMNDFilter,
    //         this.trinhDoVanHoaFilter,
    //         this.maxNamTotNghiepFilter == null ? this.maxNamTotNghiepFilterEmpty : this.maxNamTotNghiepFilter,
    //         this.minNamTotNghiepFilter == null ? this.minNamTotNghiepFilterEmpty : this.minNamTotNghiepFilter,
    //         this.tienDoTuyenDungCodeFilter,
    //         this.recorD_STATUSFilter,
    //         this.maxMARKER_IDFilter == null ? this.maxMARKER_IDFilterEmpty : this.maxMARKER_IDFilter,
    //         this.minMARKER_IDFilter == null ? this.minMARKER_IDFilterEmpty : this.minMARKER_IDFilter,
    //         this.autH_STATUSFilter,
    //         this.dienThoaiFilter,
    //         this.emailFilter,
    //         this.diaChiFilter,
    //         this.maxDay1Filter,
    //         this.minDay1Filter,
    //         this.maxDay2Filter,
    //         this.minDay2Filter,
    //         this.maxDay3Filter,
    //         this.minDay3Filter,
    //         this.time1Filter,
    //         this.time2Filter,
    //         this.time3Filter,
    //         this.noteFilter,
    //     )
    //         .subscribe(result => {
    //             this._fileDownloadService.downloadTempFile(result);
    //         });
    // }
}
