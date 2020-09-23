﻿import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LichSuUploadsServiceProxy, LichSuUploadDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditLichSuUploadModalComponent } from './create-or-edit-lichSuUpload-modal.component';
import { ViewLichSuUploadModalComponent } from './view-lichSuUpload-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './lichSuUploads.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class LichSuUploadsComponent extends AppComponentBase {

    @ViewChild('createOrEditLichSuUploadModal', { static: true }) createOrEditLichSuUploadModal: CreateOrEditLichSuUploadModalComponent;
    @ViewChild('viewLichSuUploadModalComponent', { static: true }) viewLichSuUploadModal: ViewLichSuUploadModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    tenFileFilter = '';
    dungLuongFilter = '';
    tieuDeFilter = '';
    typeFilter = '';
    typeIDFilter = '';




    constructor(
        injector: Injector,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getLichSuUploads(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._lichSuUploadsServiceProxy.getAll(
            this.filterText,
            this.tenFileFilter,
            this.dungLuongFilter,
            this.tieuDeFilter,
            this.typeFilter,
            this.typeIDFilter,
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

    createLichSuUpload(): void {
        this.createOrEditLichSuUploadModal.show();
    }

    deleteLichSuUpload(lichSuUpload: LichSuUploadDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._lichSuUploadsServiceProxy.delete(lichSuUpload.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
