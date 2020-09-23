import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ViTriCongViecsServiceProxy, CreateOrEditViTriCongViecDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditViTriCongViecModal',
    templateUrl: './create-or-edit-viTriCongViec-modal.component.html'
})
export class CreateOrEditViTriCongViecModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    viTriCongViec: CreateOrEditViTriCongViecDto = new CreateOrEditViTriCongViecDto();



    constructor(
        injector: Injector,
        private _viTriCongViecsServiceProxy: ViTriCongViecsServiceProxy
    ) {
        super(injector);
    }

    show(viTriCongViecId?: number): void {

        if (!viTriCongViecId) {
            this.viTriCongViec = new CreateOrEditViTriCongViecDto();
            this.viTriCongViec.id = viTriCongViecId;
            this.viTriCongViec.ngayHetHanBHYT = moment().startOf('day');
            this.viTriCongViec.ngayThamGiaBH = moment().startOf('day');
            this.viTriCongViec.ngayChinhThuc = moment().startOf('day');
            this.viTriCongViec.ngayThuViec = moment().startOf('day');
            this.viTriCongViec.ngayTapSu = moment().startOf('day');
            this.viTriCongViec.ngayHetHan = moment().startOf('day');
            this.viTriCongViec.ngayCap = moment().startOf('day');
            this.viTriCongViec.ngaySinh = moment().startOf('day');
            this.viTriCongViec.ngayKyHDKTH = moment().startOf('day');
            this.viTriCongViec.ngayKyHD36TH = moment().startOf('day');
            this.viTriCongViec.ngayKyHD12TH = moment().startOf('day');
            this.viTriCongViec.ngayKyHDTV = moment().startOf('day');
            this.viTriCongViec.ngayKYHDCTV = moment().startOf('day');
            this.viTriCongViec.ngayKyHDKV = moment().startOf('day');
            this.viTriCongViec.ngayKYHDTT = moment().startOf('day');
            this.viTriCongViec.ngayKyHD = moment().startOf('day');

            this.active = true;
            this.modal.show();
        } else {
            this._viTriCongViecsServiceProxy.getViTriCongViecForEdit(viTriCongViecId).subscribe(result => {
                this.viTriCongViec = result.viTriCongViec;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._viTriCongViecsServiceProxy.createOrEdit(this.viTriCongViec)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
