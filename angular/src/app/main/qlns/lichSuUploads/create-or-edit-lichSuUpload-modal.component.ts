import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { LichSuUploadsServiceProxy, CreateOrEditLichSuUploadDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditLichSuUploadModal',
    templateUrl: './create-or-edit-lichSuUpload-modal.component.html'
})
export class CreateOrEditLichSuUploadModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    lichSuUpload: CreateOrEditLichSuUploadDto = new CreateOrEditLichSuUploadDto();



    constructor(
        injector: Injector,
        private _lichSuUploadsServiceProxy: LichSuUploadsServiceProxy
    ) {
        super(injector);
    }

    show(lichSuUploadId?: number): void {

        if (!lichSuUploadId) {
            this.lichSuUpload = new CreateOrEditLichSuUploadDto();
            this.lichSuUpload.id = lichSuUploadId;

            this.active = true;
            this.modal.show();
        } else {
            this._lichSuUploadsServiceProxy.getLichSuUploadForEdit(lichSuUploadId).subscribe(result => {
                this.lichSuUpload = result.lichSuUpload;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._lichSuUploadsServiceProxy.createOrEdit(this.lichSuUpload)
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
