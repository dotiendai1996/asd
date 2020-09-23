import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SYS_PREFIXsServiceProxy, CreateOrEditSYS_PREFIXDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditSYS_PREFIXModal',
    templateUrl: './create-or-edit-syS_PREFIX-modal.component.html'
})
export class CreateOrEditSYS_PREFIXModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    syS_PREFIX: CreateOrEditSYS_PREFIXDto = new CreateOrEditSYS_PREFIXDto();



    constructor(
        injector: Injector,
        private _syS_PREFIXsServiceProxy: SYS_PREFIXsServiceProxy
    ) {
        super(injector);
    }

    show(syS_PREFIXId?: number): void {

        if (!syS_PREFIXId) {
            this.syS_PREFIX = new CreateOrEditSYS_PREFIXDto();
            this.syS_PREFIX.id = syS_PREFIXId;

            this.active = true;
            this.modal.show();
        } else {
            this._syS_PREFIXsServiceProxy.getSYS_PREFIXForEdit(syS_PREFIXId).subscribe(result => {
                this.syS_PREFIX = result.syS_PREFIX;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._syS_PREFIXsServiceProxy.createOrEdit(this.syS_PREFIX)
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
