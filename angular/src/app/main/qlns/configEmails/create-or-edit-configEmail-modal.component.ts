import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ConfigEmailsServiceProxy, CreateOrEditConfigEmailDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditConfigEmailModal',
    templateUrl: './create-or-edit-configEmail-modal.component.html'
})
export class CreateOrEditConfigEmailModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    configEmail: CreateOrEditConfigEmailDto = new CreateOrEditConfigEmailDto();



    constructor(
        injector: Injector,
        private _configEmailsServiceProxy: ConfigEmailsServiceProxy
    ) {
        super(injector);
    }

    show(configEmailId?: number): void {

        if (!configEmailId) {
            this.configEmail = new CreateOrEditConfigEmailDto();
            this.configEmail.id = configEmailId;

            this.active = true;
            this.modal.show();
        } else {
            this._configEmailsServiceProxy.getConfigEmailForEdit(configEmailId).subscribe(result => {
                this.configEmail = result.configEmail;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._configEmailsServiceProxy.createOrEdit(this.configEmail)
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
