import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetViTriCongViecForViewDto, ViTriCongViecDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewViTriCongViecModal',
    templateUrl: './view-viTriCongViec-modal.component.html'
})
export class ViewViTriCongViecModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetViTriCongViecForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetViTriCongViecForViewDto();
        this.item.viTriCongViec = new ViTriCongViecDto();
    }

    show(item: GetViTriCongViecForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
