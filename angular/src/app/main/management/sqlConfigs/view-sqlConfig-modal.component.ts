import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetSqlConfigForViewDto, SqlConfigDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewSqlConfigModal',
    templateUrl: './view-sqlConfig-modal.component.html'
})
export class ViewSqlConfigModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetSqlConfigForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetSqlConfigForViewDto();
        this.item.sqlConfig = new SqlConfigDto();
    }

    show(item: GetSqlConfigForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
