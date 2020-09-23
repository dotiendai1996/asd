import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RoleMappersServiceProxy, RoleMapperDto, CreateOrEditRoleMapperDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditRoleMapperModalComponent } from './create-or-edit-roleMapper-modal.component';
import { ViewRoleMapperModalComponent } from './view-roleMapper-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './roleMappers.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class RoleMappersComponent extends AppComponentBase {

    @ViewChild('createOrEditRoleMapperModal', { static: true }) createOrEditRoleMapperModal: CreateOrEditRoleMapperModalComponent;
    @ViewChild('viewRoleMapperModalComponent', { static: true }) viewRoleMapperModal: ViewRoleMapperModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxRoleIdFilter : number;
		maxRoleIdFilterEmpty : number;
		minRoleIdFilter : number;
		minRoleIdFilterEmpty : number;
    maxLabelIdFilter : number;
		maxLabelIdFilterEmpty : number;
		minLabelIdFilter : number;
		minLabelIdFilterEmpty : number;
    maxMenuIdFilter : number;
		maxMenuIdFilterEmpty : number;
		minMenuIdFilter : number;
		minMenuIdFilterEmpty : number;
    isActiveFilter = -1;
    roleMapper : CreateOrEditRoleMapperDto = new CreateOrEditRoleMapperDto();
    listDataRoleMapper:any;

    selectedName: any;


    constructor(
        injector: Injector,
        private _roleMappersServiceProxy: RoleMappersServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

     //getRoleMappers() {
        getRoleMappers(event?: LazyLoadEvent) {
            if (this.primengTableHelper.shouldResetPaging(event)) {
                this.paginator.changePage(0);
                return;
            }
            this.primengTableHelper.showLoadingIndicator();
        this._roleMappersServiceProxy.getAll(
            this.filterText,
            this.maxRoleIdFilter == null ? this.maxRoleIdFilterEmpty: this.maxRoleIdFilter,
            this.minRoleIdFilter == null ? this.minRoleIdFilterEmpty: this.minRoleIdFilter,
            this.maxLabelIdFilter == null ? this.maxLabelIdFilterEmpty: this.maxLabelIdFilter,
            this.minLabelIdFilter == null ? this.minLabelIdFilterEmpty: this.minLabelIdFilter,
            this.maxMenuIdFilter == null ? this.maxMenuIdFilterEmpty: this.maxMenuIdFilter,
            this.minMenuIdFilter == null ? this.minMenuIdFilterEmpty: this.minMenuIdFilter,
            this.isActiveFilter,//"1",0,100000
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),10
            //this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.listDataRoleMapper = result.items;
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            console.log(this.listDataRoleMapper);
            console.log(this.roleMapper);
        });
    };

    restart()
    {
     this.getRoleMappers();  
    }
    ngOnInit(){
        this.getRoleMappers();
    }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createRoleMapper(): void {
        this.createOrEditRoleMapperModal.show();
    }

    deleteRoleMapper(roleMapper: RoleMapperDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._roleMappersServiceProxy.delete(roleMapper.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    public highlightRow(emp) {
        this.selectedName = emp.roleMapper.id;
      } 
}
