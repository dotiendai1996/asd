import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DynamicDatasourceServiceProxy, DynamicDatasourceDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDynamicDatasourceModalComponent } from './create-or-edit-dynamicDatasource-modal.component';
import { ViewDynamicDatasourceModalComponent } from './view-dynamicDatasource-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './dynamicDatasource.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DynamicDatasourceComponent extends AppComponentBase {

    @ViewChild('createOrEditDynamicDatasourceModal', { static: true }) createOrEditDynamicDatasourceModal: CreateOrEditDynamicDatasourceModalComponent;
    @ViewChild('viewDynamicDatasourceModalComponent', { static: true }) viewDynamicDatasourceModal: ViewDynamicDatasourceModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxTypeFilter : number;
		maxTypeFilterEmpty : number;
		minTypeFilter : number;
		minTypeFilterEmpty : number;
    maxObjectIdFilter : number;
		maxObjectIdFilterEmpty : number;
		minObjectIdFilter : number;
		minObjectIdFilterEmpty : number;
    maxDynamicFieldIdFilter : number;
		maxDynamicFieldIdFilterEmpty : number;
		minDynamicFieldIdFilter : number;
		minDynamicFieldIdFilterEmpty : number;
    maxOrderFilter : number;
		maxOrderFilterEmpty : number;
		minOrderFilter : number;
		minOrderFilterEmpty : number;
    isActiveFilter = -1;




    constructor(
        injector: Injector,
        private _dynamicDatasourceServiceProxy: DynamicDatasourceServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getDynamicDatasource(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._dynamicDatasourceServiceProxy.getAll(
            this.filterText,
            this.maxTypeFilter == null ? this.maxTypeFilterEmpty: this.maxTypeFilter,
            this.minTypeFilter == null ? this.minTypeFilterEmpty: this.minTypeFilter,
            this.maxObjectIdFilter == null ? this.maxObjectIdFilterEmpty: this.maxObjectIdFilter,
            this.minObjectIdFilter == null ? this.minObjectIdFilterEmpty: this.minObjectIdFilter,
            this.maxDynamicFieldIdFilter == null ? this.maxDynamicFieldIdFilterEmpty: this.maxDynamicFieldIdFilter,
            this.minDynamicFieldIdFilter == null ? this.minDynamicFieldIdFilterEmpty: this.minDynamicFieldIdFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.isActiveFilter,
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

    createDynamicDatasource(): void {
        this.createOrEditDynamicDatasourceModal.show();
    }

    deleteDynamicDatasource(dynamicDatasource: DynamicDatasourceDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._dynamicDatasourceServiceProxy.delete(dynamicDatasource.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._dynamicDatasourceServiceProxy.getDynamicDatasourceToExcel(
        this.filterText,
            this.maxTypeFilter == null ? this.maxTypeFilterEmpty: this.maxTypeFilter,
            this.minTypeFilter == null ? this.minTypeFilterEmpty: this.minTypeFilter,
            this.maxObjectIdFilter == null ? this.maxObjectIdFilterEmpty: this.maxObjectIdFilter,
            this.minObjectIdFilter == null ? this.minObjectIdFilterEmpty: this.minObjectIdFilter,
            this.maxDynamicFieldIdFilter == null ? this.maxDynamicFieldIdFilterEmpty: this.maxDynamicFieldIdFilter,
            this.minDynamicFieldIdFilter == null ? this.minDynamicFieldIdFilterEmpty: this.minDynamicFieldIdFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.isActiveFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
