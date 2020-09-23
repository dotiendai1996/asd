import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { RoleMappersServiceProxy, CreateOrEditRoleMapperDto, RoleServiceProxy, MenusServiceProxy, LabelsServiceProxy, MenuDto, GetMenuForViewDto, CreateOrEditListRoleMapper } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { AppNavigationService } from '@app/shared/layout/nav/app-navigation.service';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { AppMenu } from '@app/shared/layout/nav/app-menu';
import { DxTreeViewComponent } from 'devextreme-angular';



@Component({
    selector: 'createOrEditRoleMapperModal',
    templateUrl: './create-or-edit-roleMapper-modal.component.html'
})
export class CreateOrEditRoleMapperModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild(DxTreeViewComponent, { static: false }) treeView;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    listlabel : any ; 
    saving = false;
    showlabel :any;

    id:number;
    roleMapper: CreateOrEditRoleMapperDto = new CreateOrEditRoleMapperDto();
    roleOptions: any[] = [];
    menuOptions:  any[] = [];
    labelOptions: any[] = [];
    checkedVal: any;
     menus: MenuDto = new MenuDto;
    showMenu : GetMenuForViewDto = new GetMenuForViewDto();
    list_label: any;
    menu: AppMenu = null;
    treeBoxValue: string[];
    listmenu:any ; 
    list_menu :any ; 
    selectedItems: any[] = [];
    menuID:any;
    constructor(
        injector: Injector,
        private _roleMappersServiceProxy: RoleMappersServiceProxy,
        private _roleServiceProxy: RoleServiceProxy,
        private _menuServiceProxy: MenusServiceProxy,
        private _labelServiceProxy: LabelsServiceProxy,
        private _appNavigationService: AppNavigationService,

    ) {
        super(injector);
        // this.treeBoxValue = ["1_1"];
    }

    ngOnInit(){
        this._roleServiceProxy.getAllRoles().subscribe((res) => {
            this.roleOptions  = res;
        });
       this.listMenu();

      
       
        // this._menuServiceProxy.getAllActive().subscribe((res) => {
        //     this.menuOptions  = res.items;

        // });

        // this._appNavigationService.getNumberOfDocument()
        // this.list_label = this._appNavigationService.getFullLabels(this.listmenu.id);
        // this.listlabel = this.list_label.items;
        // console.log( this.listlabel);

        // this._labelServiceProxy.getAllForRoleMapper().subscribe((res) => {
        //     this.labelOptions = res;
        // });
    }
    // treeView_itemSelectionChanged(e){
    //     console.log(e);
    //     const nodes = e.component.getNodes();
    //     console.log(nodes);
    //     debugger;
    //     this.treeBoxValue = this.getSelectedItemsKeys(nodes);
    // }
    // getSelectedItemsKeys(items) {
    //     var result = [],
    //         that = this;

    //     items.forEach(function(item) {
    //         if(item.selected) {
    //             result.push(item.key);
    //         }
    //         if(item.items.length) {
    //             result = result.concat(that.getSelectedItemsKeys(item.items));
    //         }
    //     });
    //     return result;
    // }

    // syncTreeViewSelection(e) {
    //     var component = (e && e.component) || (this.treeView && this.treeView.instance);

    //     if (!component) return;

    //     if (!this.treeBoxValue) {
    //         component.unselectAll();
    //     }

    //     if (this.treeBoxValue) {
    //         this.treeBoxValue.forEach((function (value) {
    //             component.selectItem(value);
    //         }).bind(this));
    //     }
    // }


    listMenu()
    {
         this._labelServiceProxy.getMenu().subscribe(res =>{
             this.list_menu = res; 
             console.log(this.list_menu)
         });
     
      
       
    }
      

    mouseClick( id: number){
     console.log(id)

     this.menuID = id;
         this._labelServiceProxy.getListLabelByMenuId(id).subscribe(res  =>{
            this.listlabel =res;
            console.log(this.listlabel)
         });
     
     
    }   
    show(roleMapperId?: number): void {

        if (!roleMapperId) {
            this.roleMapper = new CreateOrEditRoleMapperDto();
            this.roleMapper.id = roleMapperId;

            this.active = true;
            this.modal.show();
        } else {
            this._roleMappersServiceProxy.getRoleMapperForEdit(roleMapperId).subscribe(result => {
                this.roleMapper = result.roleMapper;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        // if(    this.roleOptions == null ||  this.listmenu == null){
        //     this.message.warn('Bạn chưa chọn Module');
        //     return;
        // }
            this.saving = true;
            // console.log(this.checkedVal);
            // if(this.checkedVal){
            //     this.roleMapper.labelId = 0;
            // }
            // else{
            //     this.roleMapper.menuId = 0;
            // }
            let data : CreateOrEditListRoleMapper = new CreateOrEditListRoleMapper();
            data.labelId = [];
            data.roleId = this.roleMapper.roleId;
            //  data.menuId = this.roleMapper.menuId; 
            this.selectedItems.forEach(x => {
                data.labelId.push(parseInt(x.id));
            });
            data.labelId.push(this.menuID);
            console.log(data)
            this._roleMappersServiceProxy.createOrEditListRoleMapper(data)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.message.success('Tạo thành công');
            }, (err)=>{
                this.message.error('Đã có lỗi xảy ra');
                this.close();
                this.modalSave.emit(null);
             });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
