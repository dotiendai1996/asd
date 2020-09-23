import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetNoiDaoTaoForViewDto, NoiDaoTaoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewNoiDaoTaoModal',
    templateUrl: './view-noiDaoTao-modal.component.html'
})
export class ViewNoiDaoTaoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetNoiDaoTaoForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetNoiDaoTaoForViewDto();
        this.item.noiDaoTao = new NoiDaoTaoDto();
    }

    show(item: GetNoiDaoTaoForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
