import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommandDatasourcesServiceProxy, CommandDatasourceDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCommandDatasourceModalComponent } from './create-or-edit-commandDatasource-modal.component';
import { ViewCommandDatasourceModalComponent } from './view-commandDatasource-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './commandDatasources.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class CommandDatasourcesComponent extends AppComponentBase {

    @ViewChild('createOrEditCommandDatasourceModal', { static: true }) createOrEditCommandDatasourceModal: CreateOrEditCommandDatasourceModalComponent;
    @ViewChild('viewCommandDatasourceModalComponent', { static: true }) viewCommandDatasourceModal: ViewCommandDatasourceModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    commandFilter = '';
    keyFilter = '';
    valueFilter = '';
    maxDynamicDatasourceIdFilter : number;
		maxDynamicDatasourceIdFilterEmpty : number;
		minDynamicDatasourceIdFilter : number;
		minDynamicDatasourceIdFilterEmpty : number;
    maxOrderFilter : number;
		maxOrderFilterEmpty : number;
		minOrderFilter : number;
		minOrderFilterEmpty : number;
    isActiveFilter = -1;




    constructor(
        injector: Injector,
        private _commandDatasourcesServiceProxy: CommandDatasourcesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getCommandDatasources(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._commandDatasourcesServiceProxy.getAll(
            this.filterText,
            this.commandFilter,
            this.keyFilter,
            this.valueFilter,
            this.maxDynamicDatasourceIdFilter == null ? this.maxDynamicDatasourceIdFilterEmpty: this.maxDynamicDatasourceIdFilter,
            this.minDynamicDatasourceIdFilter == null ? this.minDynamicDatasourceIdFilterEmpty: this.minDynamicDatasourceIdFilter,
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

    createCommandDatasource(): void {
        this.createOrEditCommandDatasourceModal.show();
    }

    deleteCommandDatasource(commandDatasource: CommandDatasourceDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._commandDatasourcesServiceProxy.delete(commandDatasource.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._commandDatasourcesServiceProxy.getCommandDatasourcesToExcel(
        this.filterText,
            this.commandFilter,
            this.keyFilter,
            this.valueFilter,
            this.maxDynamicDatasourceIdFilter == null ? this.maxDynamicDatasourceIdFilterEmpty: this.maxDynamicDatasourceIdFilter,
            this.minDynamicDatasourceIdFilter == null ? this.minDynamicDatasourceIdFilterEmpty: this.minDynamicDatasourceIdFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.isActiveFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
