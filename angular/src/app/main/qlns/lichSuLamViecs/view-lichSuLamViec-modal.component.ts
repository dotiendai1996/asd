import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetLichSuLamViecForViewDto, LichSuLamViecDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewLichSuLamViecModal',
    templateUrl: './view-lichSuLamViec-modal.component.html'
})
export class ViewLichSuLamViecModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetLichSuLamViecForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetLichSuLamViecForViewDto();
        this.item.lichSuLamViec = new LichSuLamViecDto();
    }

    show(item: GetLichSuLamViecForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
