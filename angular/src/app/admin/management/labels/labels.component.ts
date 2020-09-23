import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LabelsServiceProxy, LabelDto, CreateOrEditLabelDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditLabelModalComponent } from './create-or-edit-label-modal.component';
import { ViewLabelModalComponent } from './view-label-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';

import notify from 'devextreme/ui/notify';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './labels.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class LabelsComponent extends AppComponentBase {

    @ViewChild('createOrEditLabelModal', { static: true }) createOrEditLabelModal: CreateOrEditLabelModalComponent;
    @ViewChild('viewLabelModalComponent', { static: true }) viewLabelModal: ViewLabelModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameFilter = '';
    titleFilter = '';
    descriptionFilter = '';
    iconFilter = '';
    linkFilter = '';
    maxParentFilter : number;
		maxParentFilterEmpty : number;
		minParentFilter : number;
		minParentFilterEmpty : number;
    maxIndexFilter : number;
		maxIndexFilterEmpty : number;
		minIndexFilter : number;
		minIndexFilterEmpty : number;
    requiredPermissionNameFilter = '';
    sqlStringFilter = '';
    listLabels :any;
    lookupDataSource = ["drop1","drop2","drop3"];
    downloads : string [];
    labels : CreateOrEditLabelDto = new CreateOrEditLabelDto();

    selectedName: any;
    constructor(
        injector: Injector,
        private _labelsServiceProxy: LabelsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getLabels(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }
     
        this._labelsServiceProxy.getAll(
            this.filterText,
            this.nameFilter,
            this.titleFilter,
            this.descriptionFilter,
            this.iconFilter,
            this.linkFilter,
            this.maxParentFilter == null ? this.maxParentFilterEmpty: this.maxParentFilter,
            this.minParentFilter == null ? this.minParentFilterEmpty: this.minParentFilter,
            this.maxIndexFilter == null ? this.maxIndexFilterEmpty: this.maxIndexFilter,
            this.minIndexFilter == null ? this.minIndexFilterEmpty: this.minIndexFilter,
            this.sqlStringFilter,
            this.requiredPermissionNameFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            10
            // this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            let count = 0;
            this.listLabels = result.items;
            for (var i = 0, len = this.listLabels.length; i < len; i++) {
                this.listLabels[i]["stt"] = ++count;
            }
            console.log(this.listLabels);
            this.primengTableHelper.records = this.listLabels;
          
        });
    }

    restart(){
        this.getLabels();
    }
    ngOnInit(){
        this.getLabels();
    }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createLabel(): void {
        this.createOrEditLabelModal.show();
    }

    deleteLabel(label: LabelDto): void {
        this.message.confirm(
            '', this.l('Are You Sure?'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._labelsServiceProxy.delete(label.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    public highlightRow(emp) {
        this.selectedName = emp.label.id;
      } 
}
