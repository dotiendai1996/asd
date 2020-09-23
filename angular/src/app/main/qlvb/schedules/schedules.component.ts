import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SchedulesServiceProxy, ScheduleDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditScheduleModalComponent } from './create-or-edit-schedule-modal.component';
import { ViewScheduleModalComponent } from './view-schedule-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './schedules.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SchedulesComponent extends AppComponentBase {

    @ViewChild('createOrEditScheduleModal', { static: true }) createOrEditScheduleModal: CreateOrEditScheduleModalComponent;
    @ViewChild('viewScheduleModalComponent', { static: true }) viewScheduleModal: ViewScheduleModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxScheduleTypeIDFilter : number;
		maxScheduleTypeIDFilterEmpty : number;
		minScheduleTypeIDFilter : number;
		minScheduleTypeIDFilterEmpty : number;
    maxDateCreatedFilter : moment.Moment;
		minDateCreatedFilter : moment.Moment;
    maxDateOccurFilter : moment.Moment;
		minDateOccurFilter : moment.Moment;
    fromTimeFilter = '';
    toTimeFilter = '';
    contentFilter = '';
    notesFilter = '';




    constructor(
        injector: Injector,
        private _schedulesServiceProxy: SchedulesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getSchedules(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._schedulesServiceProxy.getAll(
            this.filterText,
            this.maxScheduleTypeIDFilter == null ? this.maxScheduleTypeIDFilterEmpty: this.maxScheduleTypeIDFilter,
            this.minScheduleTypeIDFilter == null ? this.minScheduleTypeIDFilterEmpty: this.minScheduleTypeIDFilter,
            this.maxDateCreatedFilter,
            this.minDateCreatedFilter,
            this.maxDateOccurFilter,
            this.minDateOccurFilter,
            this.fromTimeFilter,
            this.toTimeFilter,
            this.contentFilter,
            this.notesFilter,
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

    createSchedule(): void {
        this.createOrEditScheduleModal.show();
    }
 

    exportToExcel(): void {
        this._schedulesServiceProxy.getSchedulesToExcel(
        this.filterText,
            this.maxScheduleTypeIDFilter == null ? this.maxScheduleTypeIDFilterEmpty: this.maxScheduleTypeIDFilter,
            this.minScheduleTypeIDFilter == null ? this.minScheduleTypeIDFilterEmpty: this.minScheduleTypeIDFilter,
            this.maxDateCreatedFilter,
            this.minDateCreatedFilter,
            this.maxDateOccurFilter,
            this.minDateOccurFilter,
            this.fromTimeFilter,
            this.toTimeFilter,
            this.contentFilter,
            this.notesFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
