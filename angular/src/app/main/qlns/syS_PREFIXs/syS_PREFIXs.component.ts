import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SYS_PREFIXsServiceProxy, SYS_PREFIXDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSYS_PREFIXModalComponent } from './create-or-edit-syS_PREFIX-modal.component';
import { ViewSYS_PREFIXModalComponent } from './view-syS_PREFIX-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './syS_PREFIXs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class SYS_PREFIXsComponent extends AppComponentBase {

    @ViewChild('createOrEditSYS_PREFIXModal', { static: true }) createOrEditSYS_PREFIXModal: CreateOrEditSYS_PREFIXModalComponent;
    @ViewChild('viewSYS_PREFIXmodalcomponent', { static: true }) viewSYS_PREFIXModal: ViewSYS_PREFIXModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    codeFilter = '';
    prefixFilter = '';
    descriptionFilter = '';




    constructor(
        injector: Injector,
        private _syS_PREFIXsServiceProxy: SYS_PREFIXsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getSYS_PREFIXs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._syS_PREFIXsServiceProxy.getAll(
            this.filterText,
            this.codeFilter,
            this.prefixFilter,
            this.descriptionFilter,
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

    createSYS_PREFIX(): void {
        this.createOrEditSYS_PREFIXModal.show();
    }

    deleteSYS_PREFIX(syS_PREFIX: SYS_PREFIXDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._syS_PREFIXsServiceProxy.delete(syS_PREFIX.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._syS_PREFIXsServiceProxy.getSYS_PREFIXsToExcel(
        this.filterText,
            this.codeFilter,
            this.prefixFilter,
            this.descriptionFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
