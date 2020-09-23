import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SqlStoreParamsServiceProxy, CreateOrEditSqlStoreParamDto, SqlConfigDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditSqlStoreParamModal',
    templateUrl: './create-or-edit-sqlStoreParam-modal.component.html'
})
export class CreateOrEditSqlStoreParamModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    listStore: SqlConfigDto[] = [];
    sqlStoreParam: CreateOrEditSqlStoreParamDto = new CreateOrEditSqlStoreParamDto();



    constructor(
        injector: Injector,
        private _sqlStoreParamsServiceProxy: SqlStoreParamsServiceProxy
    ) {
        super(injector);
    }

    show(sqlStoreParamId?: number): void {
        this._sqlStoreParamsServiceProxy.getAllStore().subscribe(result => {
            this.listStore = result;
        });

        if (!sqlStoreParamId) {
            this.sqlStoreParam = new CreateOrEditSqlStoreParamDto();
            this.sqlStoreParam.id = sqlStoreParamId;
            this.sqlStoreParam.isActive = true;
            this.active = true;
            this.modal.show();
        } else {
            this._sqlStoreParamsServiceProxy.getSqlStoreParamForEdit(sqlStoreParamId).subscribe(result => {
                this.sqlStoreParam = result.sqlStoreParam;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._sqlStoreParamsServiceProxy.createOrEdit(this.sqlStoreParam)
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
