import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { TemplatesServiceProxy, CreateOrEditTemplateDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditTemplateModal',
    templateUrl: './create-or-edit-template-modal.component.html'
})
export class CreateOrEditTemplateModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    template: CreateOrEditTemplateDto = new CreateOrEditTemplateDto();



    constructor(
        injector: Injector,
        private _templatesServiceProxy: TemplatesServiceProxy
    ) {
        super(injector);
    }

    show(templateId?: number): void {

        if (!templateId) {
            this.template = new CreateOrEditTemplateDto();
            this.template.id = templateId;

            this.active = true;
            this.modal.show();
        } else {
            this._templatesServiceProxy.getTemplateForEdit(templateId).subscribe(result => {
                this.template = result.template;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._templatesServiceProxy.createOrEdit(this.template)
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
