import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SettingConfigsServiceProxy, CreateOrEditSettingConfigDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditSettingConfigModal',
    templateUrl: './create-or-edit-settingConfig-modal.component.html'
})
export class CreateOrEditSettingConfigModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    settingConfig: CreateOrEditSettingConfigDto = new CreateOrEditSettingConfigDto();



    constructor(
        injector: Injector,
        private _settingConfigsServiceProxy: SettingConfigsServiceProxy
    ) {
        super(injector);
    }

    show(settingConfigId?: number): void {

        if (!settingConfigId) {
            this.settingConfig = new CreateOrEditSettingConfigDto();
            this.settingConfig.id = settingConfigId;

            this.active = true;
            this.modal.show();
        } else {
            this._settingConfigsServiceProxy.getSettingConfigForEdit(settingConfigId).subscribe(result => {
                this.settingConfig = result.settingConfig;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._settingConfigsServiceProxy.createOrEdit(this.settingConfig)
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
