import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SqlConfigsServiceProxy, CreateOrEditSqlConfigDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditSqlConfigModal',
    templateUrl: './create-or-edit-sqlConfig-modal.component.html'
})
export class CreateOrEditSqlConfigModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    sqlConfig: CreateOrEditSqlConfigDto = new CreateOrEditSqlConfigDto();



    constructor(
        injector: Injector,
        private _sqlConfigsServiceProxy: SqlConfigsServiceProxy
    ) {
        super(injector);
    }

    show(sqlConfigId?: number): void {

        if (!sqlConfigId) {
            this.sqlConfig = new CreateOrEditSqlConfigDto();
            this.sqlConfig.id = sqlConfigId;

            this.active = true;
            this.modal.show();
        } else {
            this._sqlConfigsServiceProxy.getSqlConfigForEdit(sqlConfigId).subscribe(result => {
                this.sqlConfig = result.sqlConfig;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._sqlConfigsServiceProxy.createOrEdit(this.sqlConfig)
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
