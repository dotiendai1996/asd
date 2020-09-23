import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SYS_CODEMASTERSsServiceProxy, CreateOrEditSYS_CODEMASTERSDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditSYS_CODEMASTERSModal',
    templateUrl: './create-or-edit-syS_CODEMASTERS-modal.component.html'
})
export class CreateOrEditSYS_CODEMASTERSModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    syS_CODEMASTERS: CreateOrEditSYS_CODEMASTERSDto = new CreateOrEditSYS_CODEMASTERSDto();



    constructor(
        injector: Injector,
        private _syS_CODEMASTERSsServiceProxy: SYS_CODEMASTERSsServiceProxy
    ) {
        super(injector);
    }

    show(syS_CODEMASTERSId?: number): void {

        if (!syS_CODEMASTERSId) {
            this.syS_CODEMASTERS = new CreateOrEditSYS_CODEMASTERSDto();
            this.syS_CODEMASTERS.id = syS_CODEMASTERSId;

            this.active = true;
            this.modal.show();
        } else {
            this._syS_CODEMASTERSsServiceProxy.getSYS_CODEMASTERSForEdit(syS_CODEMASTERSId).subscribe(result => {
                this.syS_CODEMASTERS = result.syS_CODEMASTERS;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._syS_CODEMASTERSsServiceProxy.createOrEdit(this.syS_CODEMASTERS)
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
