import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetStoreDatasourceForViewDto, StoreDatasourceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewStoreDatasourceModal',
    templateUrl: './view-storeDatasource-modal.component.html'
})
export class ViewStoreDatasourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetStoreDatasourceForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetStoreDatasourceForViewDto();
        this.item.storeDatasource = new StoreDatasourceDto();
    }

    show(item: GetStoreDatasourceForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
