import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SYS_CODEMASTERSsServiceProxy, SYS_CODEMASTERSDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSYS_CODEMASTERSModalComponent } from './create-or-edit-syS_CODEMASTERS-modal.component';
import { ViewSYS_CODEMASTERSModalComponent } from './view-syS_CODEMASTERS-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './syS_CODEMASTERSs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SYS_CODEMASTERSsComponent extends AppComponentBase {

    @ViewChild('createOrEditSYS_CODEMASTERSModal', { static: true }) createOrEditSYS_CODEMASTERSModal: CreateOrEditSYS_CODEMASTERSModalComponent;
    @ViewChild('viewSYS_CODEMASTERSmodal.component', { static: true }) viewSYS_CODEMASTERSModal: ViewSYS_CODEMASTERSModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    prefixFilter = '';
    maxCurValueFilter : number;
		maxCurValueFilterEmpty : number;
		minCurValueFilter : number;
		minCurValueFilterEmpty : number;
    descriptionFilter = '';
    activeFilter = '';




    constructor(
        injector: Injector,
        private _syS_CODEMASTERSsServiceProxy: SYS_CODEMASTERSsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getSYS_CODEMASTERSs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._syS_CODEMASTERSsServiceProxy.getAll(
            this.filterText,
            this.prefixFilter,
            this.maxCurValueFilter == null ? this.maxCurValueFilterEmpty: this.maxCurValueFilter,
            this.minCurValueFilter == null ? this.minCurValueFilterEmpty: this.minCurValueFilter,
            this.descriptionFilter,
            this.activeFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createSYS_CODEMASTERS(): void {
        this.createOrEditSYS_CODEMASTERSModal.show();
    }

    deleteSYS_CODEMASTERS(syS_CODEMASTERS: SYS_CODEMASTERSDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._syS_CODEMASTERSsServiceProxy.delete(syS_CODEMASTERS.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._syS_CODEMASTERSsServiceProxy.getSYS_CODEMASTERSsToExcel(
        this.filterText,
            this.prefixFilter,
            this.maxCurValueFilter == null ? this.maxCurValueFilterEmpty: this.maxCurValueFilter,
            this.minCurValueFilter == null ? this.minCurValueFilterEmpty: this.minCurValueFilter,
            this.descriptionFilter,
            this.activeFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
