import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetMenuForViewDto, MenuDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewMenuModal',
    templateUrl: './view-menu-modal.component.html'
})
export class ViewMenuModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetMenuForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetMenuForViewDto();
        this.item.menu = new MenuDto();
    }

    show(item: GetMenuForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
