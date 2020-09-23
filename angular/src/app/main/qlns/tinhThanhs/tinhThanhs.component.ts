import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TinhThanhsServiceProxy, TinhThanhDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditTinhThanhModalComponent } from './create-or-edit-tinhThanh-modal.component';
import { ViewTinhThanhModalComponent } from './view-tinhThanh-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './tinhThanhs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class TinhThanhsComponent extends AppComponentBase {

    @ViewChild('createOrEditTinhThanhModal', { static: true }) createOrEditTinhThanhModal: CreateOrEditTinhThanhModalComponent;
    @ViewChild('viewTinhThanhmodal.component', { static: true }) viewTinhThanhModal: ViewTinhThanhModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    tenTinhThanhFilter = '';
    maTinhThanhFilter = '';

    selectedName: any;



    constructor(
        injector: Injector,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getTinhThanhs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._tinhThanhsServiceProxy.getAll(
            this.filterText,
            this.tenTinhThanhFilter,
            this.maTinhThanhFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }
    public highlightRow(emp) {
        this.selectedName = emp.tinhThanh.id;
      } 
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createTinhThanh(): void {
        this.createOrEditTinhThanhModal.show();
    }

    deleteTinhThanh(tinhThanh: TinhThanhDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._tinhThanhsServiceProxy.delete(tinhThanh.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._tinhThanhsServiceProxy.getTinhThanhsToExcel(
        this.filterText,
            this.tenTinhThanhFilter,
            this.maTinhThanhFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
