import { Component, Injector, ViewEncapsulation, ViewChild, OnInit, Input } from '@angular/core';
import { DynamicFieldsServiceProxy, RoleServiceProxy ,TenantServiceProxy , CreateOrEditDynamicFieldDto, LabelsServiceProxy, LabelDto, DynamicActionsServiceProxy, CreateOrEditDynamicActionDto, TenantListDto, RoleListDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';

import { appModuleAnimation } from '@shared/animations/routerTransition';
import * as _ from 'lodash';
import * as moment from 'moment';
import { DxDataGridComponent, DxTreeViewComponent } from 'devextreme-angular';

@Component({
    templateUrl: './dynamicActions.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DynamicActionsComponent extends AppComponentBase {

    // @ViewChild('createOrEditDynamicFieldModal', { static: true }) createOrEditDynamicFieldModal: CreateOrEditDynamicFieldModalComponent;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
    @Input() labelId: number;
    advancedFiltersAreShown = false;
    selectedItemKeys: any[] = [];
    states = [{id: 1, name: 'Check Box'}, {id: 2, name: 'Text Box'}, {id: 3, name: 'Combobox'}, {id: 4, name: 'Date Box'}];
    dataSource: CreateOrEditDynamicActionDto[] = [];
    labels: LabelDto[] = [];
    selectedLabel: LabelDto;
    selectedRole: RoleListDto;
    selectedTenant: TenantListDto;
    treeBoxValue: string;
    roleid: any ; 
    tenantid: any ; 
    tenants: TenantListDto [] = [];
    roles: RoleListDto [] = [];
    lookupData = {
        store: {
            type: 'array',
            data: [
                { id: 1, name: 'Màn hình chuyển 1', hasTransfer :'Màn hình chuyển 1',  position: 'Top Left'  },
                { id: 2, name: 'Màn hình chuyển 2' ,hasTransfer :'Màn hình chuyển 2' ,  position: 'Top Right' },
                { id: 3, name: 'Màn hình chuyển 3' ,hasTransfer :'Màn hình chuyển 3' , position: 'Bottom Left'  }, 
                { id: 4, name: 'Màn hình chuyển 4' ,hasTransfer :'Màn hình chuyển 4'  , position: 'Bottom Right'  }
                // ...
            ],
            key: "id"
        },
        pageSize: 10,
        paginate: true
    };

    dynamicActionData: CreateOrEditDynamicFieldDto[] = [];

    oldData: any[] = [];
    //value.typeField == 3 || value.typeField == 1 || value.typeField == 4){ // combobox,checkbox,datebox
    constructor(
        injector: Injector,
        private _labelsServiceProxy: LabelsServiceProxy,
        private _roleServiceProxy: RoleServiceProxy,
        private _tenantServiceProxy: TenantServiceProxy,
        private _dynamicActionServiceProxy: DynamicActionsServiceProxy
    ) {
        super(injector);
        this._labelsServiceProxy.getAllForRoleMapper().subscribe(res => {
            this.labels = res;
        });
        this._roleServiceProxy.getAllRoles().subscribe(res => {
            this.roles = res;
        });
        this._tenantServiceProxy.getAllTenant().subscribe(res => {
            this.tenants = res;
        });

    }

    selectionChanged(data: any) {
        console.log(data);
        this.selectedItemKeys = data.selectedRowKeys;
    }

    //thêm một dòng rỗng
    plusClick(){
        this.dataSource.push(new CreateOrEditDynamicActionDto());
    }

    checkIfObjectNull(ele: CreateOrEditDynamicFieldDto){
        //hàm thêm của dxDataGrid tự động thêm trường __KEY__ vào
        if(_.isEmpty(ele)){
            return ele;
        }
        if(ele['__KEY__'] && (ele.name || ele.nameDescription || ele.width || ele.typeField))
            return ele;
    }

    // onValueChanged(data: any){
    //     this.selectedMenu = data.selectedItem;
    //     this.dataSource = [];
    //     // this._dynamicActionServiceProxy.getDynamicActionByLabelId(this.selectedMenu.id , this.tenantid,this.roleid).subscribe(res => {
    //         this._dynamicActionServiceProxy.getDynamicActionByLabelId(this.selectedMenu.id ).subscribe(res => {
    //     if(res == null){
    //             this.plusClick();
    //         }
    //         else{
    //             this.dataSource.push(res);
    //             // this.dataGrid.dataSource = data;
    //         }
    //     });
    // }
   

    onValueChangedLabel(data:any)
    {
        this.selectedLabel = data.selectedItem ; 
    }
    onValueChangedRole(data:any)
    {
        this.selectedRole = data.selectedItem ; 
    }
    onValueChangedTenant(data:any)
    {
        this.selectedTenant = data.selectedItem ; 
    }
    

    
    okCLick(e: any){
        let data = [];

        if(this.selectedTenant != undefined || this.selectedTenant != null){
            this._dynamicActionServiceProxy.getAllDynamicActionByLabelId(this.selectedLabel.id,this.selectedRole.id,this.selectedTenant.id).subscribe(result =>{
                console.log(result);
                if(result == null){
                    this.dataSource.push(new CreateOrEditDynamicActionDto());
                }
                else{
                    this.dataSource.push(result);
                    this.dataGrid.dataSource = data;
                }
            });

        }
        else{
            this._dynamicActionServiceProxy.getAllDynamicActionByLabelId(this.selectedLabel.id,this.selectedRole.id,0).subscribe(result =>{
                console.log(result);
                if(result == null){
                    console.log("aaaa")
                    this.dataSource.push(new CreateOrEditDynamicActionDto());
                }
                else{
                    this.dataSource.push(result);
                    this.dataGrid.dataSource = data;
                }
            });
        }
        
    }

    save(){
        if(_.isEmpty(this.selectedLabel)){
            this.message.warn('Bạn chưa chọn Module');
            return;
        }
        //loại bỏ những dòng trống       
        // this.dataSource = this.dataSource.filter(x => !_.isEmpty(x));
        console.log(this.dataSource[0])
        this.dataSource[0].labelId = this.selectedLabel.id;
        // this.dataSource.map(x => {x.labelId = this.selectedMenu.id});
        // let newData = new CreateOrEditDynamicActionDto();
        // newData.labelId = this.dataGrid.dataSource[0].labelId;
        // newData.isActive = true;
        // newData.hasSave = this.dataGrid.dataSource[0].hasSave;
        // newData.hasAssignWork = this.dataGrid.dataSource[0].hasAssignWork;
        // newData.hasFinish = this.dataGrid.dataSource[0].hasFinish;
        // newData.hasSaveAndTransfer = this.dataGrid.dataSource[0].hasSaveAndTransfer;
        // newData.hasTransfer = this.dataGrid.dataSource[0].hasTransfer;
        // newData.isBack = this.dataGrid.dataSource[0].isBack;
        // newData.isTopPosition = this.dataGrid.dataSource[0].isTopPosition;
        // newData.order = this.dataGrid.dataSource[0].order;
        this._dynamicActionServiceProxy.createOrEdit(this.dataGrid.dataSource[0]).subscribe((next)=>{
            this.message.success('Tạo thành công');
        }, (err)=>{
            this.message.error('Đã có lỗi xảy ra');
        });
    }
}
