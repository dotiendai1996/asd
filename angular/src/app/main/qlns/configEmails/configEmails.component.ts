import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfigEmailsServiceProxy, ConfigEmailDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditConfigEmailModalComponent } from './create-or-edit-configEmail-modal.component';
import { ViewConfigEmailModalComponent } from './view-configEmail-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './configEmails.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ConfigEmailsComponent extends AppComponentBase {

    @ViewChild('createOrEditConfigEmailModal', { static: true }) createOrEditConfigEmailModal: CreateOrEditConfigEmailModalComponent;
    @ViewChild('viewConfigEmailModalComponent', { static: true }) viewConfigEmailModal: ViewConfigEmailModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    diaChiEmailFilter = '';
    tenHienThiFilter = '';
    diaChiIPFilter = '';
    maxCongSMTPFilter : number;
		maxCongSMTPFilterEmpty : number;
		minCongSMTPFilter : number;
		minCongSMTPFilterEmpty : number;
    checkSSLFilter = -1;
    checkThongTinFilter = -1;
    tenMienFilter = '';
    tenTruyCapFilter = '';
    matKhauFilter = '';
    tenCTYFilter = '';

    selectedName : any;


    constructor(
        injector: Injector,
        private _configEmailsServiceProxy: ConfigEmailsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getConfigEmails(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._configEmailsServiceProxy.getAll(
            this.filterText,
            this.diaChiEmailFilter,
            this.tenHienThiFilter,
            this.diaChiIPFilter,
            this.maxCongSMTPFilter == null ? this.maxCongSMTPFilterEmpty: this.maxCongSMTPFilter,
            this.minCongSMTPFilter == null ? this.minCongSMTPFilterEmpty: this.minCongSMTPFilter,
            this.checkSSLFilter,
            this.checkThongTinFilter,
            this.tenMienFilter,
            this.tenTruyCapFilter,
            this.matKhauFilter,
            this.tenCTYFilter,
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
        this.selectedName = emp.configEmail.id;
      } 
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createConfigEmail(): void {
        this.createOrEditConfigEmailModal.show();
    }

    deleteConfigEmail(configEmail: ConfigEmailDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._configEmailsServiceProxy.delete(configEmail.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._configEmailsServiceProxy.getConfigEmailsToExcel(
        this.filterText,
            this.diaChiEmailFilter,
            this.tenHienThiFilter,
            this.diaChiIPFilter,
            this.maxCongSMTPFilter == null ? this.maxCongSMTPFilterEmpty: this.maxCongSMTPFilter,
            this.minCongSMTPFilter == null ? this.minCongSMTPFilterEmpty: this.minCongSMTPFilter,
            this.checkSSLFilter,
            this.checkThongTinFilter,
            this.tenMienFilter,
            this.tenTruyCapFilter,
            this.matKhauFilter,
            this.tenCTYFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
