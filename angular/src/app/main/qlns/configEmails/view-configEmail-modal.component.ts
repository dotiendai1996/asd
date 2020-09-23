import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetConfigEmailForViewDto, ConfigEmailDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewConfigEmailModal',
    templateUrl: './view-configEmail-modal.component.html'
})
export class ViewConfigEmailModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetConfigEmailForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetConfigEmailForViewDto();
        this.item.configEmail = new ConfigEmailDto();
    }

    show(item: GetConfigEmailForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
