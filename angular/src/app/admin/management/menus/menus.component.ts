import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenusServiceProxy, MenuDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditMenuModalComponent } from './create-or-edit-menu-modal.component';
import { ViewMenuModalComponent } from './view-menu-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './menus.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class MenusComponent extends AppComponentBase {
    @ViewChild('createOrEditMenuModal', { static: true }) createOrEditMenuModal: CreateOrEditMenuModalComponent;
    @ViewChild('viewMenuModalComponent', { static: true }) viewMenuModal: ViewMenuModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameFilter = '';
    titleFilter = '';
    iconFilter = '';
    descriptionFilter = '';
    maxParentFilter: number;
    maxParentFilterEmpty: number;
    minParentFilter: number;
    minParentFilterEmpty: number;
    isParentFilter = -1;
    linkFilter = '';
    typeFilter = '';
    maxCreationTimeFilter: moment.Moment;
    minCreationTimeFilter: moment.Moment;
    maxLastModificationTimeFilter: moment.Moment;
    minLastModificationTimeFilter: moment.Moment;
    isDeletedFilter = -1;
    maxDeletionTimeFilter: moment.Moment;
    minDeletionTimeFilter: moment.Moment;
    maxIndexFilter: number;
    maxIndexFilterEmpty: number;
    minIndexFilter: number;
    minIndexFilterEmpty: number;
    isDelimiterFilter = -1;
    requiredPermissionNameFilter = '';




    constructor(
        injector: Injector,
        private _menusServiceProxy: MenusServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getMenus(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._menusServiceProxy.getAll(
            this.filterText,
            this.nameFilter,
            this.titleFilter,
            this.iconFilter,
            this.descriptionFilter,
            this.maxParentFilter == null ? this.maxParentFilterEmpty : this.maxParentFilter,
            this.minParentFilter == null ? this.minParentFilterEmpty : this.minParentFilter,
            // this.isParentFilter,
            this.linkFilter,
            this.maxCreationTimeFilter,
            this.minCreationTimeFilter,
            this.maxLastModificationTimeFilter,
            this.minLastModificationTimeFilter,
            this.isDeletedFilter,
            this.maxDeletionTimeFilter,
            this.minDeletionTimeFilter,
            this.maxIndexFilter == null ? this.maxIndexFilterEmpty : this.maxIndexFilter,
            this.minIndexFilter == null ? this.minIndexFilterEmpty : this.minIndexFilter,
            // this.isDelimiterFilter,
            this.requiredPermissionNameFilter,
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

    createMenu(): void {
        this.createOrEditMenuModal.show();
    }

    deleteMenu(menu: MenuDto): void {
        this.message.confirm(
            '', this.l('Are you sure?'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._menusServiceProxy.delete(menu.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    // exportToExcel(): void {
    //     this._menusServiceProxy.getMenusToExcel(
    //     this.filterText,
    //         this.nameFilter,
    //         this.titleFilter,
    //         this.iconFilter,
    //         this.descriptionFilter,
    //         this.maxParentFilter == null ? this.maxParentFilterEmpty: this.maxParentFilter,
    //         this.minParentFilter == null ? this.minParentFilterEmpty: this.minParentFilter,
    //         this.isParentFilter,
    //         this.linkFilter,
    //         this.typeFilter,
    //         this.maxCreationTimeFilter,
    //         this.minCreationTimeFilter,
    //         this.maxLastModificationTimeFilter,
    //         this.minLastModificationTimeFilter,
    //         this.isDeletedFilter,
    //         this.maxDeletionTimeFilter,
    //         this.minDeletionTimeFilter,
    //         this.maxIndexFilter == null ? this.maxIndexFilterEmpty: this.maxIndexFilter,
    //         this.minIndexFilter == null ? this.minIndexFilterEmpty: this.minIndexFilter,
    //         this.isDelimiterFilter,
    //         this.requiredPermissionNameFilter,
    //     )
    //     .subscribe(result => {
    //         this._fileDownloadService.downloadTempFile(result);
    //      });
    // }
}
