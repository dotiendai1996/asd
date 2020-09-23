import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SqlStoreParamsServiceProxy, SqlStoreParamDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSqlStoreParamModalComponent } from './create-or-edit-sqlStoreParam-modal.component';
import { ViewSqlStoreParamModalComponent } from './view-sqlStoreParam-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './sqlStoreParams.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SqlStoreParamsComponent extends AppComponentBase {

    @ViewChild('createOrEditSqlStoreParamModal', { static: true }) createOrEditSqlStoreParamModal: CreateOrEditSqlStoreParamModalComponent;
    @ViewChild('viewSqlStoreParamModalComponent', { static: true }) viewSqlStoreParamModal: ViewSqlStoreParamModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxSqlConfigIdFilter : number;
		maxSqlConfigIdFilterEmpty : number;
		minSqlConfigIdFilter : number;
		minSqlConfigIdFilterEmpty : number;
    codeFilter = '';
    formatFilter = '';
    nameFilter = '';
    isActiveFilter = -1;
    valueStringFilter = '';
    maxValueIntFilter : number;
		maxValueIntFilterEmpty : number;
		minValueIntFilter : number;
		minValueIntFilterEmpty : number;




    constructor(
        injector: Injector,
        private _sqlStoreParamsServiceProxy: SqlStoreParamsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getSqlStoreParams(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._sqlStoreParamsServiceProxy.getAll(
            this.filterText,
            this.maxSqlConfigIdFilter == null ? this.maxSqlConfigIdFilterEmpty: this.maxSqlConfigIdFilter,
            this.minSqlConfigIdFilter == null ? this.minSqlConfigIdFilterEmpty: this.minSqlConfigIdFilter,
            this.codeFilter,
            this.formatFilter,
            this.nameFilter,
            this.isActiveFilter,
            this.valueStringFilter,
            this.maxValueIntFilter == null ? this.maxValueIntFilterEmpty: this.maxValueIntFilter,
            this.minValueIntFilter == null ? this.minValueIntFilterEmpty: this.minValueIntFilter,
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

    createSqlStoreParam(): void {
        this.createOrEditSqlStoreParamModal.show();
    }

    deleteSqlStoreParam(sqlStoreParam: SqlStoreParamDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._sqlStoreParamsServiceProxy.delete(sqlStoreParam.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._sqlStoreParamsServiceProxy.getSqlStoreParamsToExcel(
        this.filterText,
            this.maxSqlConfigIdFilter == null ? this.maxSqlConfigIdFilterEmpty: this.maxSqlConfigIdFilter,
            this.minSqlConfigIdFilter == null ? this.minSqlConfigIdFilterEmpty: this.minSqlConfigIdFilter,
            this.codeFilter,
            this.formatFilter,
            this.nameFilter,
            this.isActiveFilter,
            this.valueStringFilter,
            this.maxValueIntFilter == null ? this.maxValueIntFilterEmpty: this.maxValueIntFilter,
            this.minValueIntFilter == null ? this.minValueIntFilterEmpty: this.minValueIntFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
