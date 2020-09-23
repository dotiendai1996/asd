import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetTinhThanhForViewDto, TinhThanhDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewTinhThanhModal',
    templateUrl: './view-tinhThanh-modal.component.html'
})
export class ViewTinhThanhModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetTinhThanhForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetTinhThanhForViewDto();
        this.item.tinhThanh = new TinhThanhDto();
    }

    show(item: GetTinhThanhForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
