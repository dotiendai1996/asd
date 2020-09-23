import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetHardDatasourceForViewDto, HardDatasourceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewHardDatasourceModal',
    templateUrl: './view-hardDatasource-modal.component.html'
})
export class ViewHardDatasourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetHardDatasourceForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetHardDatasourceForViewDto();
        this.item.hardDatasource = new HardDatasourceDto();
    }

    show(item: GetHardDatasourceForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
