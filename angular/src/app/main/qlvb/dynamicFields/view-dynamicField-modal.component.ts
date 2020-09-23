import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetDynamicFieldForViewDto, DynamicFieldDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewDynamicFieldModal',
    templateUrl: './view-dynamicField-modal.component.html'
})
export class ViewDynamicFieldModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetDynamicFieldForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetDynamicFieldForViewDto();
        this.item.dynamicField = new DynamicFieldDto();
    }

    show(item: GetDynamicFieldForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
