import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetLabelForViewDto, LabelDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewLabelModal',
    templateUrl: './view-label-modal.component.html'
})
export class ViewLabelModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetLabelForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetLabelForViewDto();
        this.item.label = new LabelDto();
    }

    show(item: GetLabelForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
