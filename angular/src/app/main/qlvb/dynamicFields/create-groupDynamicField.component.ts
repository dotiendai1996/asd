import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { DynamicFieldsServiceProxy, CreateOrEditDynamicFieldDto, LabelsServiceProxy, LabelDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditDynamicFieldModalComponent } from './create-or-edit-dynamicField-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import * as _ from 'lodash';
import * as moment from 'moment';
import { DxDataGridComponent, DxTreeViewComponent } from 'devextreme-angular';

@Component({
    templateUrl: './create-groupDynamicField.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class CreateGroupDynamicFieldComponent extends AppComponentBase {

    @ViewChild('createOrEditDynamicFieldModal', { static: true }) createOrEditDynamicFieldModal: CreateOrEditDynamicFieldModalComponent;
    @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
    advancedFiltersAreShown = false;
    selectedItemKeys: any[] = [];
    states = [{id: 1, name: 'Check Box'}, {id: 2, name: 'Text Box'}, {id: 3, name: 'Combobox'}, {id: 4, name: 'Date Box'}];
    dataSource: CreateOrEditDynamicFieldDto[] = [];
    labels: LabelDto[] = [];
    selectedMenu: LabelDto;
    treeBoxValue: string;
    //value.typeField == 3 || value.typeField == 1 || value.typeField == 4){ // combobox,checkbox,datebox
    constructor(
        injector: Injector,
        private _labelsServiceProxy: LabelsServiceProxy,
        private _dynamicFieldsServiceProxy: DynamicFieldsServiceProxy
    ) {
        super(injector);
        this._labelsServiceProxy.getAllLabelForDynamicField().subscribe(res => {
            this.labels = res;
        });
    }

    selectionChanged(data: any) {
        this.selectedItemKeys = data.selectedRowKeys;
    }

    //thêm một dòng rỗng
    plusClick(){
        this.dataSource.push(new CreateOrEditDynamicFieldDto());
        console.log(this.dataSource);
    }

    checkIfObjectNull(ele: CreateOrEditDynamicFieldDto){
        //hàm thêm của dxDataGrid tự động thêm trường __KEY__ vào
        if(_.isEmpty(ele)){
            return ele;
        }
        if(ele['__KEY__'] && (ele.name || ele.nameDescription || ele.width || ele.typeField))
            return ele;
    }

    onValueChanged(data: any){
        this.selectedMenu = data.selectedItem;
    }

    save(){
        console.log(this.selectedMenu);
        if(_.isEmpty(this.selectedMenu)){
            this.message.warn('Bạn chưa chọn Module');
            return;
        }
        //loại bỏ những dòng trống       
        this.dataSource = this.dataSource.filter(x => !_.isEmpty(x));
        this.dataSource.map(x => {x.moduleId = this.selectedMenu.id, x.tableName = this.selectedMenu.title});
        console.log(this.dataSource);
        this._dynamicFieldsServiceProxy.createDynamicFieldForModule(this.dataSource).subscribe((next)=>{
            this.message.success('Tạo thành công');
        }, (err)=>{
            this.message.error('Đã có lỗi xảy ra');
        });
    }
}
