import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { MenusServiceProxy, CreateOrEditMenuDto, MenuDto, PermissionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicModuleComponent } from '@app/shared/common/dynamicModule/dynamicModule.component';


@Component({
    selector: 'createOrEditMenuModal',
    templateUrl: './create-or-edit-menu-modal.component.html'
})
export class CreateOrEditMenuModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('dynamicModule', { static: false }) private dynamicModal : DynamicModuleComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    parentOptions: MenuDto[];
    link = '';
    parameter = '';
    menu: CreateOrEditMenuDto = new CreateOrEditMenuDto();
    parameters = 1;
    lastModificationTime: Date;
    deletionTime: Date;

    permissionOptions: PermissionDto[];


    constructor(
        injector: Injector,
        private router : Router,
        private _menusServiceProxy: MenusServiceProxy
    ) {
        super(injector);
        this.link = this.router.url;
    }

    ngOnInit(){
        // this._menusServiceProxy.getList("","").subscribe(result => {
        //     this.parentOptions = result;
        // });

        // this.parentOptions.unshift(new MenuDto(){id = 0, name = "", });

        this._menusServiceProxy.getRootPermissions().subscribe(result =>{
            this.permissionOptions = result;
        });
    }

    show(menuId?: number): void {
        this.lastModificationTime = null;
        this.deletionTime = null;

        if (!menuId) {
            this.menu = new CreateOrEditMenuDto();
            this.menu.id = menuId;
            this.menu.creationTime = moment().startOf('day');

            this.active = true;
            this.modal.show();
        } else {
            this._menusServiceProxy.getMenuForEdit(menuId).subscribe(result => {
                this.menu = result.menu;
                this.dynamicModal.loadDynamicField(this.menu.id, this.link); 
                if (this.menu.lastModificationTime) {
					this.lastModificationTime = this.menu.lastModificationTime.toDate();
                }
                if (this.menu.deletionTime) {
					this.deletionTime = this.menu.deletionTime.toDate();
                }

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;


        if (this.lastModificationTime) {
            if (!this.menu.lastModificationTime) {
                this.menu.lastModificationTime = moment(this.lastModificationTime).startOf('day');
            }
            else {
                this.menu.lastModificationTime = moment(this.lastModificationTime);
            }
        }
        else {
            this.menu.lastModificationTime = null;
        }
        if (this.deletionTime) {
            if (!this.menu.deletionTime) {
                this.menu.deletionTime = moment(this.deletionTime).startOf('day');
            }
            else {
                this.menu.deletionTime = moment(this.deletionTime);
            }
        }
        else {
            this.menu.deletionTime = null;
        }
            this._menusServiceProxy.createOrEdit(this.menu)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.dynamicModal.saveDynamicValue();
                this.close();
                this.modalSave.emit(null);
             });
    }

    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
