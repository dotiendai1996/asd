import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { DataChamCongsServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';


@Component({
    selector: 'exportChamCongModal',
    templateUrl: './export-ChamCong-modal.component.html'
})
export class ExportChamCongModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    minProcessDateExportFilter = moment().startOf('day');

    constructor(
        injector: Injector,
        private _dataChamCongsServiceProxy: DataChamCongsServiceProxy,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    show(truongGiaoDichId?: number): void {
        this.active = true;
        this.modal.show();
    }

    save(): void {
        this.saving = true;

        this._dataChamCongsServiceProxy.getDataChamCongsToExcel(
            this.minProcessDateExportFilter
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
            this.modalSave.emit(null);
         });
    }

    close(): void {
        this.saving = false;
        this.active = false;
        this.modal.hide();
    }
    onOpenCalendar(container) {
        container.monthSelectHandler = (event: any): void => {
          container._store.dispatch(container._actions.select(event.date));
        };     
        container.setViewMode('month');
    }
}
