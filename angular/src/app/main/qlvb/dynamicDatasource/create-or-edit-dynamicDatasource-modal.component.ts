import { Component, ViewChild, Injector, Output, EventEmitter, Input} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DynamicDatasourceServiceProxy, CreateOrEditDynamicDatasourceDto, MenuDto, MenusServiceProxy, DynamicFieldsServiceProxy, DynamicFieldDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditDynamicDatasourceModal',
    templateUrl: './create-or-edit-dynamicDatasource-modal.component.html'
})
export class CreateOrEditDynamicDatasourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    menus: MenuDto[] = [];
    dynamicDatasource: CreateOrEditDynamicDatasourceDto = new CreateOrEditDynamicDatasourceDto();
    dynamicField: DynamicFieldDto[] = [];
    selectedRow: DynamicFieldDto = new DynamicFieldDto();
    dataSource = [{id: 1, name: 'DataSource cứng'}, {id: 2, name: 'DataSource theo Store'}, {id: 3, name: 'DataSource theo lệnh'}];

    constructor(
        injector: Injector,
        private _menusServiceProxy: MenusServiceProxy,
        private _dynamicField: DynamicFieldsServiceProxy,
        private _dynamicDatasourceServiceProxy: DynamicDatasourceServiceProxy
    ) {
        super(injector);
        this._dynamicField.getCbbField(undefined).subscribe((res) => {
            this.dynamicField = res;
        });
    }

    getSelectedRow(){
        this.dynamicDatasource.objectId = this.selectedRow.moduleId;
        this.dynamicDatasource.dynamicFieldId = this.selectedRow.id;
    }

    show(dynamicDatasourceId?: number): void {

        if (!dynamicDatasourceId) {
            this.dynamicDatasource = new CreateOrEditDynamicDatasourceDto();
            this.dynamicDatasource.id = dynamicDatasourceId;
            this.dynamicDatasource.isActive = true;
            this.active = true;
            this.modal.show();
        } else {
            this._dynamicDatasourceServiceProxy.getDynamicDatasourceForEdit(dynamicDatasourceId).subscribe(result => {
                this.dynamicDatasource = result.dynamicDatasource;
                //binding vào thẻ select option
                this.selectedRow = this.dynamicField.find(x => x.id == this.dynamicDatasource.dynamicFieldId);
                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._dynamicDatasourceServiceProxy.createOrEdit(this.dynamicDatasource)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
