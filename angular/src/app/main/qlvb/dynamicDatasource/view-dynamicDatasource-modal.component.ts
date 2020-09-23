import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetDynamicDatasourceForViewDto, DynamicDatasourceDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewDynamicDatasourceModal',
    templateUrl: './view-dynamicDatasource-modal.component.html'
})
export class ViewDynamicDatasourceModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetDynamicDatasourceForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetDynamicDatasourceForViewDto();
        this.item.dynamicDatasource = new DynamicDatasourceDto();
    }

    show(item: GetDynamicDatasourceForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
