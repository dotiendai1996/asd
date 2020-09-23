import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { StoreDatasourcesServiceProxy, CreateOrEditStoreDatasourceDto, DynamicDatasourceDto, DynamicDatasourceServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditStoreDatasourceModal',
    templateUrl: './create-or-edit-storeDatasource-modal.component.html'
})
export class CreateOrEditStoreDatasourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    storeDatasource: CreateOrEditStoreDatasourceDto = new CreateOrEditStoreDatasourceDto();
    dynamicDatasource: DynamicDatasourceDto[] = [];
    


    constructor(
        injector: Injector,
        private _dynamicDatasourceServiceProxy: DynamicDatasourceServiceProxy,
        private _storeDatasourcesServiceProxy: StoreDatasourcesServiceProxy
    ) {
        super(injector);
        this._dynamicDatasourceServiceProxy.getDynamicDatasourceByType(2).subscribe((res) => {
            this.dynamicDatasource = res;
        });
    }

    show(storeDatasourceId?: number): void {

        if (!storeDatasourceId) {
            this.storeDatasource = new CreateOrEditStoreDatasourceDto();
            this.storeDatasource.id = storeDatasourceId;

            this.active = true;
            this.modal.show();
        } else {
            this._storeDatasourcesServiceProxy.getStoreDatasourceForEdit(storeDatasourceId).subscribe(result => {
                this.storeDatasource = result.storeDatasource;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._storeDatasourcesServiceProxy.createOrEdit(this.storeDatasource)
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
