import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { DangKyKCBsServiceProxy, CreateOrEditDangKyKCBDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditDangKyKCBModal',
    templateUrl: './create-or-edit-dangKyKCB-modal.component.html'
})
export class CreateOrEditDangKyKCBModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    dangKyKCB: CreateOrEditDangKyKCBDto = new CreateOrEditDangKyKCBDto();



    constructor(
        injector: Injector,
        private _dangKyKCBsServiceProxy: DangKyKCBsServiceProxy
    ) {
        super(injector);
    }

    show(dangKyKCBId?: number): void {

        if (!dangKyKCBId) {
            this.dangKyKCB = new CreateOrEditDangKyKCBDto();
            this.dangKyKCB.id = dangKyKCBId;

            this.active = true;
            this.modal.show();
        } else {
            this._dangKyKCBsServiceProxy.getDangKyKCBForEdit(dangKyKCBId).subscribe(result => {
                this.dangKyKCB = result.dangKyKCB;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._dangKyKCBsServiceProxy.createOrEdit(this.dangKyKCB)
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
