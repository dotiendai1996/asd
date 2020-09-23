import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DynamicFieldsServiceProxy, DynamicFieldDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDynamicFieldModalComponent } from './create-or-edit-dynamicField-modal.component';
import { ViewDynamicFieldModalComponent } from './view-dynamicField-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './dynamicFields.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DynamicFieldsComponent extends AppComponentBase {

    @ViewChild('createOrEditDynamicFieldModal', { static: true }) createOrEditDynamicFieldModal: CreateOrEditDynamicFieldModalComponent;
    @ViewChild('viewDynamicFieldModalComponent', { static: true }) viewDynamicFieldModal: ViewDynamicFieldModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxModuleIdFilter : number;
		maxModuleIdFilterEmpty : number;
	minModuleIdFilter : number;
	minModuleIdFilterEmpty : number;
    tableNameFilter = '';
    nameFilter = '';
    maxTypeFieldFilter : number;
	maxTypeFieldFilterEmpty : number;
	minTypeFieldFilter : number;
	minTypeFieldFilterEmpty : number;
    maxWidthFilter : number;
	maxWidthFilterEmpty : number;
	minWidthFilter : number;
	minWidthFilterEmpty : number;
    nameDescriptionFilter = '';
    maxWidthDescriptionFilter : number;
	maxWidthDescriptionFilterEmpty : number;
	minWidthDescriptionFilter : number;
    minWidthDescriptionFilterEmpty : number;
    maxDepartmentIdFilter : number;
	maxDepartmentIdFilterEmpty : number;
	minDepartmentIdFilter : number;
    minDepartmentIdFilterEmpty : number;
    isActiveFilter = -1;
    maxOrderFilter : number;
	maxOrderFilterEmpty : number;
    minOrderFilter : number;
    minOrderFilterEmpty : number;
    classAttachFilter = '';




    constructor(
        injector: Injector,
        private _dynamicFieldsServiceProxy: DynamicFieldsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getDynamicFields(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._dynamicFieldsServiceProxy.getAll(
            this.filterText,
            this.maxModuleIdFilter == null ? this.maxModuleIdFilterEmpty: this.maxModuleIdFilter,
            this.minModuleIdFilter == null ? this.minModuleIdFilterEmpty: this.minModuleIdFilter,
            this.tableNameFilter,
            this.nameFilter,           
            this.maxTypeFieldFilter == null ? this.maxTypeFieldFilterEmpty: this.maxTypeFieldFilter,
            this.minTypeFieldFilter == null ? this.minTypeFieldFilterEmpty: this.minTypeFieldFilter,
            this.maxWidthFilter == null ? this.maxWidthFilterEmpty: this.maxWidthFilter,
            this.minWidthFilter == null ? this.minWidthFilterEmpty: this.minWidthFilter,
            this.nameDescriptionFilter,
            this.maxDepartmentIdFilter == null ? this.maxDepartmentIdFilterEmpty: this.maxDepartmentIdFilter,
            this.minDepartmentIdFilter == null ? this.minDepartmentIdFilterEmpty: this.minDepartmentIdFilter,
            this.isActiveFilter,
            this.maxOrderFilter == null ? this.maxOrderFilterEmpty: this.maxOrderFilter,
            this.minOrderFilter == null ? this.minOrderFilterEmpty: this.minOrderFilter,
            this.maxWidthDescriptionFilter == null ? this.maxWidthDescriptionFilterEmpty: this.maxWidthDescriptionFilter,
            this.minWidthDescriptionFilter == null ? this.minWidthDescriptionFilterEmpty: this.minWidthDescriptionFilter,
            this.classAttachFilter,         
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

    createDynamicField(): void {
        this.createOrEditDynamicFieldModal.show();
    }

    deleteDynamicField(dynamicField: DynamicFieldDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._dynamicFieldsServiceProxy.delete(dynamicField.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        // this._dynamicFieldsServiceProxy.getDynamicFieldsToExcel(
        // this.filterText,
        //     this.maxModuleIdFilter == null ? this.maxModuleIdFilterEmpty: this.maxModuleIdFilter,
        //     this.minModuleIdFilter == null ? this.minModuleIdFilterEmpty: this.minModuleIdFilter,
        //     this.tableNameFilter,
        //     this.nameFilter,
        //     this.maxTypeFieldFilter == null ? this.maxTypeFieldFilterEmpty: this.maxTypeFieldFilter,
        //     this.minTypeFieldFilter == null ? this.minTypeFieldFilterEmpty: this.minTypeFieldFilter,
        //     this.maxWidthFilter == null ? this.maxWidthFilterEmpty: this.maxWidthFilter,
        //     this.minWidthFilter == null ? this.minWidthFilterEmpty: this.minWidthFilter,
        //     this.nameDescriptionFilter,
        //     this.maxWidthDescriptionFilter == null ? this.maxWidthDescriptionFilterEmpty: this.maxWidthDescriptionFilter,
        //     this.minWidthDescriptionFilter == null ? this.minWidthDescriptionFilterEmpty: this.minWidthDescriptionFilter,
        //     this.classAttachFilter,
        // )
        // .subscribe(result => {
        //     this._fileDownloadService.downloadTempFile(result);
        //  });
    }
}
