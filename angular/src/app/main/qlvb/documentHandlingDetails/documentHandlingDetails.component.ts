import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DocumentHandlingDetailsServiceProxy, DocumentHandlingDetailDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDocumentHandlingDetailModalComponent } from './create-or-edit-documentHandlingDetail-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './documentHandlingDetails.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DocumentHandlingDetailsComponent extends AppComponentBase {

    @ViewChild('createOrEditDocumentHandlingDetailModal', { static: true }) createOrEditDocumentHandlingDetailModal: CreateOrEditDocumentHandlingDetailModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    groupFilter = '';
    personFilter = '';
    typeFilter = '';
    superiosFilter = '';
    personalCommentFilter = '';
    maxStartDateFilter : moment.Moment;
		minStartDateFilter : moment.Moment;
    maxEndDateFilter : moment.Moment;
		minEndDateFilter : moment.Moment;
    maxDocumentHandlingIdFilter : number;
		maxDocumentHandlingIdFilterEmpty : number;
		minDocumentHandlingIdFilter : number;
		minDocumentHandlingIdFilterEmpty : number;
    mainHandlingFilter = -1;
    coHandlingFilter = -1;
    toKnowFilter = -1;
    userIdFilter : number;
    isHandledFilter: boolean;


    constructor(
        injector: Injector,
        private _documentHandlingDetailsServiceProxy: DocumentHandlingDetailsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getDocumentHandlingDetails(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._documentHandlingDetailsServiceProxy.getAll(
            this.filterText,
            this.groupFilter,
            this.personFilter,
            this.typeFilter,
            this.superiosFilter,
            this.personalCommentFilter,
            this.maxStartDateFilter,
            this.minStartDateFilter,
            this.maxEndDateFilter,
            this.minEndDateFilter,
            this.maxDocumentHandlingIdFilter == null ? this.maxDocumentHandlingIdFilterEmpty: this.maxDocumentHandlingIdFilter,
            this.minDocumentHandlingIdFilter == null ? this.minDocumentHandlingIdFilterEmpty: this.minDocumentHandlingIdFilter,
            this.mainHandlingFilter,
            this.coHandlingFilter,
            this.toKnowFilter,
            this.userIdFilter,
            this.isHandledFilter,
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

    createDocumentHandlingDetail(): void {
        this.createOrEditDocumentHandlingDetailModal.show();
    }

    deleteDocumentHandlingDetail(documentHandlingDetail: DocumentHandlingDetailDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._documentHandlingDetailsServiceProxy.delete(documentHandlingDetail.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
