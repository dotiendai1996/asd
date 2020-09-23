import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { CommandDatasourcesServiceProxy, CreateOrEditCommandDatasourceDto, DynamicDatasourceDto, DynamicDatasourceServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditCommandDatasourceModal',
    templateUrl: './create-or-edit-commandDatasource-modal.component.html'
})
export class CreateOrEditCommandDatasourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    commandDatasource: CreateOrEditCommandDatasourceDto = new CreateOrEditCommandDatasourceDto();
    dynamicDatasource: DynamicDatasourceDto[] = [];
    


    constructor(
        injector: Injector,
        private _dynamicDatasourceServiceProxy: DynamicDatasourceServiceProxy,
        private _commandDatasourcesServiceProxy: CommandDatasourcesServiceProxy
    ) {
        super(injector);
        this._dynamicDatasourceServiceProxy.getDynamicDatasourceByType(3).subscribe((res) => {
            this.dynamicDatasource = res;
        });
    }

    show(commandDatasourceId?: number): void {

        if (!commandDatasourceId) {
            this.commandDatasource = new CreateOrEditCommandDatasourceDto();
            this.commandDatasource.id = commandDatasourceId;

            this.active = true;
            this.modal.show();
        } else {
            this._commandDatasourcesServiceProxy.getCommandDatasourceForEdit(commandDatasourceId).subscribe(result => {
                this.commandDatasource = result.commandDatasource;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._commandDatasourcesServiceProxy.createOrEdit(this.commandDatasource)
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
