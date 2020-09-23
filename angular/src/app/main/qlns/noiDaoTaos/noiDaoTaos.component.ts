import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NoiDaoTaosServiceProxy, NoiDaoTaoDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditNoiDaoTaoModalComponent } from './create-or-edit-noiDaoTao-modal.component';
import { ViewNoiDaoTaoModalComponent } from './view-noiDaoTao-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './noiDaoTaos.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class NoiDaoTaosComponent extends AppComponentBase {

    @ViewChild('createOrEditNoiDaoTaoModal', { static: true }) createOrEditNoiDaoTaoModal: CreateOrEditNoiDaoTaoModalComponent;
    @ViewChild('viewNoiDaoTaomodal.component', { static: true }) viewNoiDaoTaoModal: ViewNoiDaoTaoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    tenNoiDaoTaoFilter = '';
    maNoiDaoTaoFilter = '';
    diaChiFilter = '';
    khuVucFilter = '';


    selectedName: any;

    constructor(
        injector: Injector,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getNoiDaoTaos(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._noiDaoTaosServiceProxy.getAll(
            this.filterText,
            this.tenNoiDaoTaoFilter,
            this.maNoiDaoTaoFilter,
            this.diaChiFilter,
            this.khuVucFilter,
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
        this.selectedName = emp.noiDaoTao.id;
      }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createNoiDaoTao(): void {
        this.createOrEditNoiDaoTaoModal.show();
    }

    deleteNoiDaoTao(noiDaoTao: NoiDaoTaoDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._noiDaoTaosServiceProxy.delete(noiDaoTao.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._noiDaoTaosServiceProxy.getNoiDaoTaosToExcel(
        this.filterText,
            this.tenNoiDaoTaoFilter,
            this.maNoiDaoTaoFilter,
            this.diaChiFilter,
            this.khuVucFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
