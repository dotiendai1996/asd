import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DynamicValuesServiceProxy, CreateOrEditDynamicValueDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditDynamicValueModal',
    templateUrl: './create-or-edit-dynamicValue-modal.component.html'
})
export class CreateOrEditDynamicValueModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    dynamicValue: CreateOrEditDynamicValueDto = new CreateOrEditDynamicValueDto();



    constructor(
        injector: Injector,
        private _dynamicValuesServiceProxy: DynamicValuesServiceProxy
    ) {
        super(injector);
    }

    show(dynamicValueId?: number): void {

        if (!dynamicValueId) {
            this.dynamicValue = new CreateOrEditDynamicValueDto();
            this.dynamicValue.id = dynamicValueId;

            this.active = true;
            this.modal.show();
        } else {
            this._dynamicValuesServiceProxy.getDynamicValueForEdit(dynamicValueId).subscribe(result => {
                this.dynamicValue = result.dynamicValue;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._dynamicValuesServiceProxy.createOrEdit(this.dynamicValue)
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
