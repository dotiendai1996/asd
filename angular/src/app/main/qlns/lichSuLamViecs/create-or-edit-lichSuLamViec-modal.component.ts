import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { LichSuLamViecsServiceProxy, CreateOrEditLichSuLamViecDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditLichSuLamViecModal',
    templateUrl: './create-or-edit-lichSuLamViec-modal.component.html'
})
export class CreateOrEditLichSuLamViecModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    lichSuLamViec: CreateOrEditLichSuLamViecDto = new CreateOrEditLichSuLamViecDto();



    constructor(
        injector: Injector,
        private _lichSuLamViecsServiceProxy: LichSuLamViecsServiceProxy
    ) {
        super(injector);
    }

    show(lichSuLamViecId?: number): void {

        if (!lichSuLamViecId) {
            this.lichSuLamViec = new CreateOrEditLichSuLamViecDto();
            this.lichSuLamViec.id = lichSuLamViecId;

            this.active = true;
            this.modal.show();
        } else {
            this._lichSuLamViecsServiceProxy.getLichSuLamViecForEdit(lichSuLamViecId).subscribe(result => {
                this.lichSuLamViec = result.lichSuLamViec;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._lichSuLamViecsServiceProxy.createOrEdit(this.lichSuLamViec)
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
