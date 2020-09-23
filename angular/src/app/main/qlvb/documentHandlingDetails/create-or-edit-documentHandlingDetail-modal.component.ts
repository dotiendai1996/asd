import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DocumentHandlingDetailsServiceProxy, CreateOrEditDocumentHandlingDetailDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditDocumentHandlingDetailModal',
    templateUrl: './create-or-edit-documentHandlingDetail-modal.component.html'
})
export class CreateOrEditDocumentHandlingDetailModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    documentHandlingDetail: CreateOrEditDocumentHandlingDetailDto = new CreateOrEditDocumentHandlingDetailDto();



    constructor(
        injector: Injector,
        private _documentHandlingDetailsServiceProxy: DocumentHandlingDetailsServiceProxy
    ) {
        super(injector);
    }

    show(documentHandlingDetailId?: number): void {

        if (!documentHandlingDetailId) {
            this.documentHandlingDetail = new CreateOrEditDocumentHandlingDetailDto();
            this.documentHandlingDetail.id = documentHandlingDetailId;
            this.documentHandlingDetail.startDate = moment().startOf('day');
            this.documentHandlingDetail.endDate = moment().startOf('day');

            this.active = true;
            this.modal.show();
        } else {
            this._documentHandlingDetailsServiceProxy.getDocumentHandlingDetailForEdit(documentHandlingDetailId).subscribe(result => {
                this.documentHandlingDetail = result.documentHandlingDetail;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._documentHandlingDetailsServiceProxy.createOrEdit(this.documentHandlingDetail)
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
