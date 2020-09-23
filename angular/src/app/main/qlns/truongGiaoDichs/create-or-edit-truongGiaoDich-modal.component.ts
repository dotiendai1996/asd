import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { TruongGiaoDichsServiceProxy, CreateOrEditTruongGiaoDichDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditTruongGiaoDichModal',
    templateUrl: './create-or-edit-truongGiaoDich-modal.component.html'
})
export class CreateOrEditTruongGiaoDichModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    truongGiaoDich: CreateOrEditTruongGiaoDichDto = new CreateOrEditTruongGiaoDichDto();



    constructor(
        injector: Injector,
        private _truongGiaoDichsServiceProxy: TruongGiaoDichsServiceProxy
    ) {
        super(injector);
    }

    show(truongGiaoDichId?: number): void {

        if (!truongGiaoDichId) {
            this.truongGiaoDich = new CreateOrEditTruongGiaoDichDto();
            this.truongGiaoDich.id = truongGiaoDichId;

            this.active = true;
            this.modal.show();
        } else {
            this._truongGiaoDichsServiceProxy.getTruongGiaoDichForEdit(truongGiaoDichId).subscribe(result => {
                this.truongGiaoDich = result.truongGiaoDich;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._truongGiaoDichsServiceProxy.createOrEdit(this.truongGiaoDich)
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
