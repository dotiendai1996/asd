import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetSYS_PREFIXForViewDto, SYS_PREFIXDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewSYS_PREFIXModal',
    templateUrl: './view-syS_PREFIX-modal.component.html'
})
export class ViewSYS_PREFIXModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetSYS_PREFIXForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetSYS_PREFIXForViewDto();
        this.item.syS_PREFIX = new SYS_PREFIXDto();
    }

    show(item: GetSYS_PREFIXForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
