import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetDangKyKCBForViewDto, DangKyKCBDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewDangKyKCBModal',
    templateUrl: './view-dangKyKCB-modal.component.html'
})
export class ViewDangKyKCBModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetDangKyKCBForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetDangKyKCBForViewDto();
        this.item.dangKyKCB = new DangKyKCBDto();
    }

    show(item: GetDangKyKCBForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
