import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UngViensServiceProxy, UngVienDto, TruongGiaoDichDto, GetUngVienForViewDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import { FileUpload } from 'primeng/fileupload';
import { formatDate } from '@angular/common';
import { LocalStorageService } from '@shared/utils/local-storage.service';

@Component({
    templateUrl: './tuongTacUngViens.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class TuongTacUngViensComponent extends AppComponentBase implements OnInit {
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @ViewChild('ExcelFileUpload', { static: true }) excelFileUpload: FileUpload;
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
    noteFilter = '';

    tenCTYFilter = '';
    startDateFilter: moment.Moment;
    endDateFilter: moment.Moment;
    lastModificationTimeFilter;
    createTimeFilter: moment.Moment;
    isCheckTimeFilter = false;
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
    viTriCongViec: TruongGiaoDichDto[] = [];
    loaiHopDong: TruongGiaoDichDto[] = [];
    hinhThucLamViec: TruongGiaoDichDto[] = [];
    trangThai: TruongGiaoDichDto[] = [];
    kenhTuyenDung: TruongGiaoDichDto[] = [];
    tienDoTuyenDung: TruongGiaoDichDto[] = [];
    gioiTinh: TruongGiaoDichDto[] = [];
    tinhTrangHonNhan: TruongGiaoDichDto[] = [];
    trinhDoDaoTao: TruongGiaoDichDto[] = [];
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
    fromDate: any;
    fromDate1: any;
    fromDate2: any;
    fromDate3: any;
    day1Filter: moment.Moment;
    day2Filter: moment.Moment;
    day3Filter: moment.Moment;

    toDate: any;
    a = true;
    popupVisible = false;
    listUngVien: any;
    //viewIsLoading = true;

    tuongTacUngVienConts = 'tuongTacUngVien.Filter';
    selectedName : any;
    constructor(
        injector: Injector,
        private _ungViensServiceProxy: UngViensServiceProxy,
        private _localStorageService: LocalStorageService,
        private router: Router,
    ) {
        super(injector);
        this.rootUrl = AppConsts.remoteServiceBaseUrl;
        this.currentTime = new Date().getHours() + "-" + new Date().getMinutes();

        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/Upload_file?currentTime=' + this.currentTime;

    }

    ngOnInit(): void {
        this.getListItemSearch();

        this._localStorageService.getItem('tuongTacUngVien.Filter', (err, filters) => {
            this.bindingToFilter(filters);
            this.getUngViens();
        });
    }

    clearFilter() {
        this.filterText = '';
        this.maUngVienFilter = '';
        this.hoVaTenFilter = '';
        this.viTriUngTuyenCodeFilter = '';
        this.kenhTuyenDungCodeFilter = '';
        this.gioiTinhCodeFilter = '';
        this.trangThaiCodeFilter = '';
        this.startDateFilter = this.fromDate;
        this.endDateFilter = this.fromDate;
        this.isCheckTimeFilter = false;
        this.createTimeFilter = this.fromDate;
        this.lastModificationTimeFilter  = this.fromDate;
        this.tienDoTuyenDungCodeFilter = '';
        this.minDay1Filter = this.fromDate;
        this.maxDay1Filter = this.fromDate;
        this.minDay2Filter = this.fromDate;
        this.maxDay2Filter = this.fromDate;
        this.minDay3Filter = this.fromDate;
        this.maxDay3Filter = this.fromDate;
        this.getUngViens();
    }

    getUngViens(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }
        this._localStorageService.setItem(this.tuongTacUngVienConts, this.parseToFiltersDto());
        // this.primengTableHelper.showLoadingIndicator();
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
            this.isCheckTimeFilter,
            this.createTimeFilter,
            this.lastModificationTimeFilter,
            this.tienDoTuyenDungCodeFilter,
            this.minDay1Filter,
            this.maxDay1Filter,
            this.minDay2Filter,
            this.maxDay2Filter,
            this.minDay3Filter,
            this.maxDay3Filter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            // this.primengTableHelper.hideLoadingIndicator();
        });
    }

    getListItemSearch() {
        let that = this;
        this._ungViensServiceProxy.getListItemSearch().subscribe(result => {
            that.viTriCongViec = result.viTriCongViec;
            that.tienDoTuyenDung = result.tienDoTuyenDung;
            that.trangThai = result.trangThai;
            that.kenhTuyenDung = result.kenhTuyenDung;
            that.gioiTinh = result.gioiTinh;
            that.tinhTrangHonNhan = result.tinhTrangHonNhan;
            that.trinhDoDaoTao = result.trinhDoDaoTao;
            that.xepLoaiHocLuc = result.xepLoaiHocLuc;
        })
    }

    editUngVien(id: any): void {
        this.router.navigate(['/app/main/qlns/tuongtacungvien/' + id]);
    }

    parseToFiltersDto() {
        return {
            filterText: this.filterText,
            maUngVienFilter: this.maUngVienFilter,
            hoVaTenFilter: this.hoVaTenFilter,
            viTriUngTuyenCodeFilter: this.viTriUngTuyenCodeFilter,
            kenhTuyenDungCodeFilter: this.kenhTuyenDungCodeFilter,
            gioiTinhCodeFilter: this.gioiTinhCodeFilter,
            trangThaiCodeFilter: this.trangThaiCodeFilter,
            startDateFilter: this.startDateFilter,
            endDateFilter: this.endDateFilter,
            isCheckTimeFilter: this.isCheckTimeFilter,
            createTimeFilter: this.createTimeFilter,
            lastModificationTimeFilter: this.lastModificationTimeFilter,
            tienDoTuyenDungCodeFilter: this.tienDoTuyenDungCodeFilter,
            minDay1Filter: this.minDay1Filter,
            maxDay1Filter: this.maxDay1Filter,
            minDay2Filter: this.minDay2Filter,
            maxDay2Filter: this.maxDay2Filter,
            minDay3Filter: this.minDay3Filter,
            maxDay3Filter: this.maxDay3Filter
        }
    }

    bindingToFilter(filters: any) {
        if (filters) {
            this.filterText = filters.filterText;
            this.maUngVienFilter = filters.maUngVienFilter;
            this.hoVaTenFilter = filters.hoVaTenFilter;
            this.viTriUngTuyenCodeFilter = filters.viTriUngTuyenCodeFilter;
            this.kenhTuyenDungCodeFilter = filters.kenhTuyenDungCodeFilter;
            this.gioiTinhCodeFilter = filters.gioiTinhCodeFilter;
            this.trangThaiCodeFilter = filters.trangThaiCodeFilter;
            this.startDateFilter = filters.startDateFilter;
            this.endDateFilter = filters.endDateFilter;
            this.isCheckTimeFilter = filters.isCheckTimeFilter;
            this.createTimeFilter = filters.createTimeFilter;
            this.lastModificationTimeFilter = filters.lastModificationTimeFilter;
            this.tienDoTuyenDungCodeFilter = filters.tienDoTuyenDungCodeFilter;
            this.minDay1Filter = filters.minDay1Filter;
            this.maxDay1Filter = filters.maxDay1Filter;
            this.minDay2Filter = filters.minDay2Filter;
            this.maxDay2Filter = filters.maxDay2Filter;
            this.minDay3Filter = filters.minDay3Filter;
            this.maxDay3Filter = filters.maxDay3Filter;
        }
    }
    public highlightRow(emp) {
        this.selectedName = emp.ungVien.id;
      } 
}
