import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SqlConfigDetailsServiceProxy, SqlConfigDetailDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSqlConfigDetailModalComponent } from './create-or-edit-sqlConfigDetail-modal.component';
import { ViewSqlConfigDetailModalComponent } from './view-sqlConfigDetail-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './sqlConfigDetails.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SqlConfigDetailsComponent extends AppComponentBase {

    @ViewChild('createOrEditSqlConfigDetailModal', { static: true }) createOrEditSqlConfigDetailModal: CreateOrEditSqlConfigDetailModalComponent;
    @ViewChild('viewSqlConfigDetailModalComponent', { static: true }) viewSqlConfigDetailModal: ViewSqlConfigDetailModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxSqlConfigIdFilter : number;
		maxSqlConfigIdFilterEmpty : number;
		minSqlConfigIdFilter : number;
		minSqlConfigIdFilterEmpty : number;
    codeFilter = '';
    nameFilter = '';
    formatFilter = '';
    typeFilter = '';
    widthFilter = '';
    maxColNumFilter : number;
		maxColNumFilterEmpty : number;
		minColNumFilter : number;
		minColNumFilterEmpty : number;
    maxGroupLevelFilter : number;
		maxGroupLevelFilterEmpty : number;
		minGroupLevelFilter : number;
		minGroupLevelFilterEmpty : number;
    isDisplayFilter = -1;
    maxOrderFilter : number;
		maxOrderFilterEmpty : number;
		minOrderFilter : number;
		minOrderFilterEmpty : number;
    textAlignFilter = '';
    maxVersionFilter : number;
		maxVersionFilterEmpty : number;
		minVersionFilter : number;
		minVersionFilterEmpty : number;
    isSumFilter = -1;
    isFreePaneFilter = -1;
    isParentFilter = -1;
    parentCodeFilter = '';
    groupSortFilter = '';
    cellTemplateFilter = '';


    constructor(
        injector: Injector,
        private _sqlConfigDetailsServiceProxy: SqlConfigDetailsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getSqlConfigDetails(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._sqlConfigDetailsServiceProxy.getAll(
            this.filterText,
            this.maxSqlConfigIdFilter == null ? this.maxSqlConfigIdFilterEmpty: this.maxSqlConfigIdFilter,
            this.minSqlConfigIdFilter == null ? this.minSqlConfigIdFilterEmpty: this.minSqlConfigIdFilter,
            this.codeFilter,
            this.nameFilter,
            this.formatFilter,
            this.typeFilter,
            this.widthFilter,
            this.maxColNumFilter == null ? this.maxColNumFilterEmpty: this.maxColNumFilter,
            this.minColNumFilter == null ? this.minColNumFilterEmpty: this.minColNumFilter,
            this.maxGroupLevelFilter == null ? this.maxGroupLevelFilterEmpty: this.maxGroupLevelFilter,
            this.minGroupLevelFilter == null ? this.minGroupLevelFilterEmpty: this.minGroupLevelFilter,
            this.isDisplayFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.textAlignFilter,
            this.maxVersionFilter == null ? this.maxVersionFilterEmpty: this.maxVersionFilter,
            this.minVersionFilter == null ? this.minVersionFilterEmpty: this.minVersionFilter,
            this.isSumFilter,
            this.isFreePaneFilter,
            this.isParentFilter,
            this.parentCodeFilter,
            this.groupSortFilter,
            this.cellTemplateFilter,
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

    createSqlConfigDetail(): void {
        this.createOrEditSqlConfigDetailModal.show();
    }

    deleteSqlConfigDetail(sqlConfigDetail: SqlConfigDetailDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._sqlConfigDetailsServiceProxy.delete(sqlConfigDetail.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._sqlConfigDetailsServiceProxy.getSqlConfigDetailsToExcel(
        this.filterText,
            this.maxSqlConfigIdFilter == null ? this.maxSqlConfigIdFilterEmpty: this.maxSqlConfigIdFilter,
            this.minSqlConfigIdFilter == null ? this.minSqlConfigIdFilterEmpty: this.minSqlConfigIdFilter,
            this.codeFilter,
            this.nameFilter,
            this.formatFilter,
            this.typeFilter,
            this.widthFilter,
            this.maxColNumFilter == null ? this.maxColNumFilterEmpty: this.maxColNumFilter,
            this.minColNumFilter == null ? this.minColNumFilterEmpty: this.minColNumFilter,
            this.maxGroupLevelFilter == null ? this.maxGroupLevelFilterEmpty: this.maxGroupLevelFilter,
            this.minGroupLevelFilter == null ? this.minGroupLevelFilterEmpty: this.minGroupLevelFilter,
            this.isDisplayFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.textAlignFilter,
            this.maxVersionFilter == null ? this.maxVersionFilterEmpty: this.maxVersionFilter,
            this.minVersionFilter == null ? this.minVersionFilterEmpty: this.minVersionFilter,
            this.isSumFilter,
            this.isFreePaneFilter,
            this.isParentFilter,
            this.parentCodeFilter,
            this.groupSortFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
