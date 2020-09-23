import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TruongGiaoDichsServiceProxy, TruongGiaoDichDto, CreateOrEditTruongGiaoDichDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditTruongGiaoDichModalComponent } from './create-or-edit-truongGiaoDich-modal.component';
import { ViewTruongGiaoDichModalComponent } from './view-truongGiaoDich-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './truongGiaoDichs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class TruongGiaoDichsComponent extends AppComponentBase {

    @ViewChild('createOrEditTruongGiaoDichModal', { static: true }) createOrEditTruongGiaoDichModal: CreateOrEditTruongGiaoDichModalComponent;
    @ViewChild('viewTruongGiaoDichmodal.component', { static: true }) viewTruongGiaoDichModal: ViewTruongGiaoDichModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    codeFilter = '';
    cdNameFilter = '';
    valueFilter = '';
    ghiChuFilter = '';
    selectedName :any;



    constructor(
        injector: Injector,
        private _truongGiaoDichsServiceProxy: TruongGiaoDichsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getTruongGiaoDichs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._truongGiaoDichsServiceProxy.getAll(
            this.filterText,
            this.codeFilter,
            this.cdNameFilter,
            this.valueFilter,
            this.ghiChuFilter,
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
        this.selectedName = emp.truongGiaoDich.id;
      } 
    setDefault(input: CreateOrEditTruongGiaoDichDto){
        input.setDefault = true;
        this._truongGiaoDichsServiceProxy.setDefaultValue(input).subscribe(()=>{
            this.notify.success("Đặt làm mặc định thành công");
            this.getTruongGiaoDichs();
        })
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createTruongGiaoDich(): void {
        this.createOrEditTruongGiaoDichModal.show();
    }

    deleteTruongGiaoDich(truongGiaoDich: TruongGiaoDichDto): void {
        this.message.confirm(
            '',this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._truongGiaoDichsServiceProxy.delete(truongGiaoDich.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._truongGiaoDichsServiceProxy.getTruongGiaoDichsToExcel(
        this.filterText,
            this.codeFilter,
            this.cdNameFilter,
            this.valueFilter,
            this.ghiChuFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
