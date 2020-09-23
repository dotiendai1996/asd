import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SqlConfigDetailsServiceProxy, CreateOrEditSqlConfigDetailDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditSqlConfigDetailModal',
    templateUrl: './create-or-edit-sqlConfigDetail-modal.component.html'
})
export class CreateOrEditSqlConfigDetailModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    sqlConfigDetail: CreateOrEditSqlConfigDetailDto = new CreateOrEditSqlConfigDetailDto();



    constructor(
        injector: Injector,
        private _sqlConfigDetailsServiceProxy: SqlConfigDetailsServiceProxy
    ) {
        super(injector);
    }

    show(sqlConfigDetailId?: number): void {

        if (!sqlConfigDetailId) {
            this.sqlConfigDetail = new CreateOrEditSqlConfigDetailDto();
            this.sqlConfigDetail.id = sqlConfigDetailId;

            this.active = true;
            this.modal.show();
        } else {
            this._sqlConfigDetailsServiceProxy.getSqlConfigDetailForEdit(sqlConfigDetailId).subscribe(result => {
                this.sqlConfigDetail = result.sqlConfigDetail;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._sqlConfigDetailsServiceProxy.createOrEdit(this.sqlConfigDetail)
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
