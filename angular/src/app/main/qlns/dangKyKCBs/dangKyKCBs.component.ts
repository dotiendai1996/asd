import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DangKyKCBsServiceProxy, DangKyKCBDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDangKyKCBModalComponent } from './create-or-edit-dangKyKCB-modal.component';
import { ViewDangKyKCBModalComponent } from './view-dangKyKCB-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './dangKyKCBs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DangKyKCBsComponent extends AppComponentBase {

    @ViewChild('createOrEditDangKyKCBModal', { static: true }) createOrEditDangKyKCBModal: CreateOrEditDangKyKCBModalComponent;
    @ViewChild('viewDangKyKCBModalComponent', { static: true }) viewDangKyKCBModal: ViewDangKyKCBModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    tenNoiKCBFilter = '';
    maNoiKCBFilter = '';
    diaChiFilter = '';
    maxTinhThanhIDFilter : number;
		maxTinhThanhIDFilterEmpty : number;
		minTinhThanhIDFilter : number;
		minTinhThanhIDFilterEmpty : number;
    ghiChuFilter = '';

    selectedName : any;


    constructor(
        injector: Injector,
        private _dangKyKCBsServiceProxy: DangKyKCBsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getDangKyKCBs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._dangKyKCBsServiceProxy.getAll(
            this.filterText,
            this.tenNoiKCBFilter,
            this.maNoiKCBFilter,
            this.diaChiFilter,
            this.maxTinhThanhIDFilter == null ? this.maxTinhThanhIDFilterEmpty: this.maxTinhThanhIDFilter,
            this.minTinhThanhIDFilter == null ? this.minTinhThanhIDFilterEmpty: this.minTinhThanhIDFilter,
            this.ghiChuFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }
    public highlightRow(emp) {
        this.selectedName = emp.dangKyKCB.id;
      } 
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createDangKyKCB(): void {
        this.createOrEditDangKyKCBModal.show();
    }

    deleteDangKyKCB(dangKyKCB: DangKyKCBDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._dangKyKCBsServiceProxy.delete(dangKyKCB.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._dangKyKCBsServiceProxy.getDangKyKCBsToExcel(
        this.filterText,
            this.tenNoiKCBFilter,
            this.maNoiKCBFilter,
            this.diaChiFilter,
            this.maxTinhThanhIDFilter == null ? this.maxTinhThanhIDFilterEmpty: this.maxTinhThanhIDFilter,
            this.minTinhThanhIDFilter == null ? this.minTinhThanhIDFilterEmpty: this.minTinhThanhIDFilter,
            this.ghiChuFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
