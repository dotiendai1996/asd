import { Component, ViewChild, Injector, Output, EventEmitter, Input, forwardRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetUngVienForViewDto, UngVienDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { OrganizationUnitsTreeComponent } from '@app/admin/shared/organization-unit-tree.component';
import { ArrayToTreeConverterService } from '@shared/utils/array-to-tree-converter.service';
import { TreeNode } from 'primeng/primeng';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
@Component({
    selector: 'inputDropdown',
    templateUrl: './custom-inputDropdown.component.html',
    providers: [
        {
          provide: NG_VALUE_ACCESSOR,
          useExisting: forwardRef(() => CustomInputDropdownComponent),
          multi: true
        }
      ]
})
export class CustomInputDropdownComponent implements ControlValueAccessor {
    //@ViewChild('pTree', {static: false}) pTree: Tre
    @ViewChild('organizationUnitTree', {static: false}) organizationUnitTree: OrganizationUnitsTreeComponent;
    @Output() ngModel: EventEmitter<any> = new EventEmitter<any>();
    @Input() name: string;
    @Input() id: string;
    @Input() class: string;
    active = false;
    saving = false;
    treeData: any;
    nameSelected: any ; 
    idSelected: any;
    // <input type="text" id="HoSo_AnhDaiDien" class="form-control" [(ngModel)]="hoSo.anhDaiDien" name="AnhDaiDien"
    item: GetUngVienForViewDto;
    totalUnitCount = 0;

    constructor(
        private _arrayToTreeConverterService: ArrayToTreeConverterService,
        private _organizationUnitService: OrganizationUnitServiceProxy,
        injector: Injector
    ) {
        this.getTreeDataFromServer();
        
    }
    writeValue(obj: any): void {
        throw new Error("Method not implemented.");
    }
    registerOnChange(fn: any): void {
        throw new Error("Method not implemented.");
    }
    registerOnTouched(fn: any): void {
        throw new Error("Method not implemented.");
    }
    setDisabledState?(isDisabled: boolean): void {
        throw new Error("Method not implemented.");
    }

    openModal(){
        document.getElementById('tree-container').style.display = 'block';
    }

    closeModal(event: any){
        document.getElementById('tree-container').style.display = 'none';
    }

    private getTreeDataFromServer(): void {
        let self = this;
        this._organizationUnitService.getOrganizationUnits().subscribe((result: ListResultDtoOfOrganizationUnitDto) => {
            this.totalUnitCount = result.items.length;
            this.treeData = this._arrayToTreeConverterService.createTree(result.items,
                'parentId',
                'id',
                null,
                'children',
                [
                    {
                        target: 'label',
                        targetFunction(item) {
                            return item.displayName;
                        }
                    }, {
                        target: 'expandedIcon',
                        value: 'fa fa-folder-open m--font-warning'
                    },
                    {
                        target: 'collapsedIcon',
                        value: 'fa fa-building-o m--font-warning'
                    },
                    {
                        target: 'selectable',
                        value: true
                    },
                    {
                        target: 'memberCount',
                        targetFunction(item) {
                            return item.memberCount;
                        }
                    },
                    {
                        target: 'roleCount',
                        targetFunction(item) {
                            return item.roleCount;
                        }
                    }
                ]);
            console.log(this.treeData);
        });
        console.log(this.name, this.ngModel, this.id, this.class);
    }

    nodeSelect(event: any){
        console.log(event);
        let arrDV= [];
        this.idSelected=  event.node.data.id;
        this.nameSelected=  event.node.label;
        arrDV.push( { id: this.idSelected , name: this.nameSelected} )
        console.log(arrDV)
    //   this.idSelected=  event.node.data.id;
      
        this.ngModel.emit(arrDV);
    }

    show(item: GetUngVienForViewDto): void {
        this.item = item;
        this.active = true;

    }

    close(): void {
        this.active = false;
      
    }
}
