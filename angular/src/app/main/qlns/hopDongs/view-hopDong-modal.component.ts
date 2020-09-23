import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetHopDongForViewDto, HopDongDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewHopDongModal',
    templateUrl: './view-hopDong-modal.component.html'
})
export class ViewHopDongModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetHopDongForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetHopDongForViewDto();
        this.item.hopDong = new HopDongDto();
    }

    show(item: GetHopDongForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
