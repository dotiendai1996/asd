import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetSqlStoreParamForViewDto, SqlStoreParamDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewSqlStoreParamModal',
    templateUrl: './view-sqlStoreParam-modal.component.html'
})
export class ViewSqlStoreParamModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetSqlStoreParamForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetSqlStoreParamForViewDto();
        this.item.sqlStoreParam = new SqlStoreParamDto();
    }

    show(item: GetSqlStoreParamForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
