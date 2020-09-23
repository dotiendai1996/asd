import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetSYS_CODEMASTERSForViewDto, SYS_CODEMASTERSDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewSYS_CODEMASTERSModal',
    templateUrl: './view-syS_CODEMASTERS-modal.component.html'
})
export class ViewSYS_CODEMASTERSModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetSYS_CODEMASTERSForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetSYS_CODEMASTERSForViewDto();
        this.item.syS_CODEMASTERS = new SYS_CODEMASTERSDto();
    }

    show(item: GetSYS_CODEMASTERSForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
