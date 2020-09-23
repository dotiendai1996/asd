import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetRoleMapperForViewDto, RoleMapperDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewRoleMapperModal',
    templateUrl: './view-roleMapper-modal.component.html'
})
export class ViewRoleMapperModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetRoleMapperForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetRoleMapperForViewDto();
        this.item.roleMapper = new RoleMapperDto();
    }

    show(item: GetRoleMapperForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
