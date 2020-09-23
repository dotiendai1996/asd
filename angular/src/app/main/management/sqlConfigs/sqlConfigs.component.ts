import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SqlConfigsServiceProxy, SqlConfigDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSqlConfigModalComponent } from './create-or-edit-sqlConfig-modal.component';
import { ViewSqlConfigModalComponent } from './view-sqlConfig-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './sqlConfigs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SqlConfigsComponent extends AppComponentBase {

    @ViewChild('createOrEditSqlConfigModal', { static: true }) createOrEditSqlConfigModal: CreateOrEditSqlConfigModalComponent;
    @ViewChild('viewSqlConfigModalComponent', { static: true }) viewSqlConfigModal: ViewSqlConfigModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    codeFilter = '';
    nameFilter = '';
    isRawQueryFilter = -1;
    sqlContentFilter = '';
    maxGroupLevelFilter : number;
		maxGroupLevelFilterEmpty : number;
		minGroupLevelFilter : number;
		minGroupLevelFilterEmpty : number;
    maxDisplayTypeFilter : number;
		maxDisplayTypeFilterEmpty : number;
		minDisplayTypeFilter : number;
		minDisplayTypeFilterEmpty : number;
    maxVersionFilter : number;
		maxVersionFilterEmpty : number;
		minVersionFilter : number;
		minVersionFilterEmpty : number;
    isDynamicColumnFilter = -1;
    maxTypeGetColumnFilter : number;
		maxTypeGetColumnFilterEmpty : number;
		minTypeGetColumnFilter : number;
		minTypeGetColumnFilterEmpty : number;




    constructor(
        injector: Injector,
        private _sqlConfigsServiceProxy: SqlConfigsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private router: Router
    ) {
        super(injector);
    }

    navigateToConfig(id: number){
        this.router.navigate(['./app/main/management/sqlConfigDetails/config/' + id]);
    }

    navigateToAddParam(){
        this.router.navigate(['./app/main/management/sqlStoreParams']);
    }

    getSqlConfigs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._sqlConfigsServiceProxy.getAll(
            this.filterText,
            this.codeFilter,
            this.nameFilter,
            this.isRawQueryFilter,
            this.sqlContentFilter,
            this.maxGroupLevelFilter == null ? this.maxGroupLevelFilterEmpty: this.maxGroupLevelFilter,
            this.minGroupLevelFilter == null ? this.minGroupLevelFilterEmpty: this.minGroupLevelFilter,
            this.maxDisplayTypeFilter == null ? this.maxDisplayTypeFilterEmpty: this.maxDisplayTypeFilter,
            this.minDisplayTypeFilter == null ? this.minDisplayTypeFilterEmpty: this.minDisplayTypeFilter,
            this.maxVersionFilter == null ? this.maxVersionFilterEmpty: this.maxVersionFilter,
            this.minVersionFilter == null ? this.minVersionFilterEmpty: this.minVersionFilter,
            this.isDynamicColumnFilter,
            this.maxTypeGetColumnFilter == null ? this.maxTypeGetColumnFilterEmpty: this.maxTypeGetColumnFilter,
            this.minTypeGetColumnFilter == null ? this.minTypeGetColumnFilterEmpty: this.minTypeGetColumnFilter,
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

    createSqlConfig(): void {
        this.createOrEditSqlConfigModal.show();
    }

    deleteSqlConfig(sqlConfig: SqlConfigDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._sqlConfigsServiceProxy.delete(sqlConfig.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
