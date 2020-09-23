import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataChamCongsServiceProxy, DataChamCongDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { ExportChamCongModalComponent } from './export-ChamCong-modal.component';

@Component({
    templateUrl: './dataChamCongs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DataChamCongsComponent extends AppComponentBase {
    @ViewChild('exportChamCongModal', { static: true }) exportChamCongModal: ExportChamCongModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    congty: any;
    congViecList: any;
    viTriCongViec: any;

    filterText = '';
    minProcessDateExportFilter = moment().startOf('day');
    minProcessDateFilter = moment().startOf('day');
    maxProcessDateFilter = moment().endOf('day');
    phongBanFilter = '';
    chucDanhFilter = '';
    diTreFilter = false;
    veSomFilter = false;
    tangCaFilter = false;
    congTacFilter = false;
    nghiPhepFilter = false;
    quenChamCongFilter = false;
    congTyFilter = '';
    selectedName : any;
    constructor(
        injector: Injector,
        private _dataChamCongsServiceProxy: DataChamCongsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    ngOnInit() {
        this.loadDataChamCongFilter();
    }

    getDataChamCongs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._dataChamCongsServiceProxy.getAll(
            this.filterText,
            this.minProcessDateFilter,
            this.maxProcessDateFilter,
            this.congTyFilter,
            this.phongBanFilter,
            this.chucDanhFilter,
            this.diTreFilter,
            this.veSomFilter,
            this.tangCaFilter,
            this.nghiPhepFilter,
            this.congTacFilter,
            this.quenChamCongFilter,
            false,
            0,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    clearFilter() {
        this.filterText = '';
        this.minProcessDateFilter = moment().startOf('day');
        this.maxProcessDateFilter = moment().endOf('day');
        this.phongBanFilter = '';
        this.chucDanhFilter = '';
        this.congTyFilter = '';
        this.diTreFilter = false;
        this.veSomFilter = false;
        this.tangCaFilter = false;
        this.getDataChamCongs();
    }

    loadDataChamCongFilter() {
        this._dataChamCongsServiceProxy.getDataChamCongFilter().subscribe(result => {
            if (result.congty && result.congty.length) {
                var congTy = result.congty.filter(x => x.cdName == 'GSOFT');
                this.congTyFilter = congTy ? congTy[0].cdName : '';
            }
            this.congty = result.congty;
            this.congViecList = result.congViecList;
            this.viTriCongViec = result.viTriCongViec;
        });
    }
    getPhongBan($event) {
        this._dataChamCongsServiceProxy.getCongViecByTenCty(this.congTyFilter).subscribe(result => {
            this.congViecList = result;
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    exportToExcel(): void {
        this.exportChamCongModal.show();
    }
    public highlightRow(emp) {
        this.selectedName = emp.rowIndex;
      }
    exportChamCongByFilterToExcel(event?: LazyLoadEvent){
        this._dataChamCongsServiceProxy.getDataChamCongsFilterToExcel(
            this.filterText,
            this.minProcessDateFilter,
            this.maxProcessDateFilter,
            this.congTyFilter,
            this.phongBanFilter,
            this.chucDanhFilter,
            this.diTreFilter,
            this.veSomFilter,
            this.tangCaFilter,
            this.nghiPhepFilter,
            this.congTacFilter,
            this.quenChamCongFilter,
            true,
            0,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
            this.notify.info(this.l('SavedSuccessfully'));
         });
    }
}
