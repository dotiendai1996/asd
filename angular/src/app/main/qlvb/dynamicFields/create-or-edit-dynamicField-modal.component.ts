import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DynamicFieldsServiceProxy, CreateOrEditDynamicFieldDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditDynamicFieldModal',
    templateUrl: './create-or-edit-dynamicField-modal.component.html'
})
export class CreateOrEditDynamicFieldModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    dynamicField: CreateOrEditDynamicFieldDto = new CreateOrEditDynamicFieldDto();



    constructor(
        injector: Injector,
        private _dynamicFieldsServiceProxy: DynamicFieldsServiceProxy
    ) {
        super(injector);
    }

    show(dynamicFieldId?: number): void {

        if (!dynamicFieldId) {
            this.dynamicField = new CreateOrEditDynamicFieldDto();
            this.dynamicField.id = dynamicFieldId;

            this.active = true;
            this.modal.show();
        } else {
            this._dynamicFieldsServiceProxy.getDynamicFieldForEdit(dynamicFieldId).subscribe(result => {
                this.dynamicField = result.dynamicField;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._dynamicFieldsServiceProxy.createOrEdit(this.dynamicField)
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
