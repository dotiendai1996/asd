import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LichSuLamViecsServiceProxy, LichSuLamViecDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditLichSuLamViecModalComponent } from './create-or-edit-lichSuLamViec-modal.component';
import { ViewLichSuLamViecModalComponent } from './view-lichSuLamViec-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './lichSuLamViecs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class LichSuLamViecsComponent extends AppComponentBase {

    @ViewChild('createOrEditLichSuLamViecModal', { static: true }) createOrEditLichSuLamViecModal: CreateOrEditLichSuLamViecModalComponent;
    @ViewChild('viewLichSuLamViecmodal.component', { static: true }) viewLichSuLamViecModal: ViewLichSuLamViecModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxUngVienIdFilter : number;
		maxUngVienIdFilterEmpty : number;
		minUngVienIdFilter : number;
		minUngVienIdFilterEmpty : number;
    noiDungFilter = '';
    tepDinhKemFilter = '';




    constructor(
        injector: Injector,
        private _lichSuLamViecsServiceProxy: LichSuLamViecsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getLichSuLamViecs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._lichSuLamViecsServiceProxy.getAll(
            this.filterText,
            this.maxUngVienIdFilter == null ? this.maxUngVienIdFilterEmpty: this.maxUngVienIdFilter,
            this.minUngVienIdFilter == null ? this.minUngVienIdFilterEmpty: this.minUngVienIdFilter,
            this.noiDungFilter,
            this.tepDinhKemFilter,
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

    createLichSuLamViec(): void {
        this.createOrEditLichSuLamViecModal.show();
    }

    deleteLichSuLamViec(lichSuLamViec: LichSuLamViecDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._lichSuLamViecsServiceProxy.delete(lichSuLamViec.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._lichSuLamViecsServiceProxy.getLichSuLamViecsToExcel(
        this.filterText,
            this.maxUngVienIdFilter == null ? this.maxUngVienIdFilterEmpty: this.maxUngVienIdFilter,
            this.minUngVienIdFilter == null ? this.minUngVienIdFilterEmpty: this.minUngVienIdFilter,
            this.noiDungFilter,
            this.tepDinhKemFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
