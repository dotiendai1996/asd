import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetTruongGiaoDichForViewDto, TruongGiaoDichDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewTruongGiaoDichModal',
    templateUrl: './view-truongGiaoDich-modal.component.html'
})
export class ViewTruongGiaoDichModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetTruongGiaoDichForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetTruongGiaoDichForViewDto();
        this.item.truongGiaoDich = new TruongGiaoDichDto();
    }

    show(item: GetTruongGiaoDichForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
