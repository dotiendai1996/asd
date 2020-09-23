import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DynamicValuesServiceProxy, DynamicValueDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDynamicValueModalComponent } from './create-or-edit-dynamicValue-modal.component';
import { ViewDynamicValueModalComponent } from './view-dynamicValue-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './dynamicValues.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DynamicValuesComponent extends AppComponentBase {

    @ViewChild('createOrEditDynamicValueModal', { static: true }) createOrEditDynamicValueModal: CreateOrEditDynamicValueModalComponent;
    @ViewChild('viewDynamicValueModalComponent', { static: true }) viewDynamicValueModal: ViewDynamicValueModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxObjectIdFilter : number;
	maxObjectIdFilterEmpty : number;
	minObjectIdFilter : number;
    minObjectIdFilterEmpty : number;
    maxDynamicFieldIdFilter : number;
	maxDynamicFieldIdFilterEmpty : number;
	minDynamicFieldIdFilter : number;
    minDynamicFieldIdFilterEmpty : number;
    isActiveFilter = -1;
    maxOrderFilter : number;
	maxOrderFilterEmpty : number;
    minOrderFilter : number;
    minOrderFilterEmpty : number;
    keyFilter = '';
    valueFilter = '';




    constructor(
        injector: Injector,
        private _dynamicValuesServiceProxy: DynamicValuesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getDynamicValues(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._dynamicValuesServiceProxy.getAll(
            this.filterText,
            this.maxObjectIdFilter == null ? this.maxObjectIdFilterEmpty: this.maxObjectIdFilter,
            this.minObjectIdFilter == null ? this.minObjectIdFilterEmpty: this.minObjectIdFilter,
            this.keyFilter,
            this.maxDynamicFieldIdFilter == null ? this.maxDynamicFieldIdFilterEmpty: this.maxDynamicFieldIdFilter,
            this.minDynamicFieldIdFilter == null ? this.minDynamicFieldIdFilterEmpty: this.minDynamicFieldIdFilter,
            this.isActiveFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.valueFilter,
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

    createDynamicValue(): void {
        this.createOrEditDynamicValueModal.show();
    }

    deleteDynamicValue(dynamicValue: DynamicValueDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._dynamicValuesServiceProxy.delete(dynamicValue.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
