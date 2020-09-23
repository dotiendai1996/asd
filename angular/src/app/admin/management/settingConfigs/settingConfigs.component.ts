import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SettingConfigsServiceProxy, SettingConfigDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSettingConfigModalComponent } from './create-or-edit-settingConfig-modal.component';
import { ViewSettingConfigModalComponent } from './view-settingConfig-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './settingConfigs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SettingConfigsComponent extends AppComponentBase {

    @ViewChild('createOrEditSettingConfigModal', { static: true }) createOrEditSettingConfigModal: CreateOrEditSettingConfigModalComponent;
    @ViewChild('viewSettingConfigModalComponent', { static: true }) viewSettingConfigModal: ViewSettingConfigModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    codeFilter = '';
    valueStringFilter = '';
    maxValueIntFilter : number;
		maxValueIntFilterEmpty : number;
		minValueIntFilter : number;
		minValueIntFilterEmpty : number;
    valueHtmlFilter = '';
    imageFilter = '';




    constructor(
        injector: Injector,
        private _settingConfigsServiceProxy: SettingConfigsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getSettingConfigs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._settingConfigsServiceProxy.getAll(
            this.filterText,
            this.codeFilter,
            this.valueStringFilter,
            this.maxValueIntFilter == null ? this.maxValueIntFilterEmpty: this.maxValueIntFilter,
            this.minValueIntFilter == null ? this.minValueIntFilterEmpty: this.minValueIntFilter,
            this.valueHtmlFilter,
            this.imageFilter,
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

    createSettingConfig(): void {
        this.createOrEditSettingConfigModal.show();
    }

    deleteSettingConfig(settingConfig: SettingConfigDto): void {
        this.message.confirm(
            '', this.l('Are you sure?'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._settingConfigsServiceProxy.delete(settingConfig.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._settingConfigsServiceProxy.getSettingConfigsToExcel(
        this.filterText,
            this.codeFilter,
            this.valueStringFilter,
            this.maxValueIntFilter == null ? this.maxValueIntFilterEmpty: this.maxValueIntFilter,
            this.minValueIntFilter == null ? this.minValueIntFilterEmpty: this.minValueIntFilter,
            this.valueHtmlFilter,
            this.imageFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
