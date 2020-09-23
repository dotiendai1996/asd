import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { SchedulesServiceProxy, CreateOrEditScheduleDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditScheduleModal',
    templateUrl: './create-or-edit-schedule-modal.component.html'
})
export class CreateOrEditScheduleModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    schedule: CreateOrEditScheduleDto = new CreateOrEditScheduleDto();



    constructor(
        injector: Injector,
        private _schedulesServiceProxy: SchedulesServiceProxy
    ) {
        super(injector);
    }

    show(scheduleId?: number): void {

        if (!scheduleId) {
            this.schedule = new CreateOrEditScheduleDto();
            this.schedule.id = scheduleId;
            this.schedule.dateCreated = moment().startOf('day');
            this.schedule.dateOccur = moment().startOf('day');

            this.active = true;
            this.modal.show();
        } else {
            this._schedulesServiceProxy.getScheduleForEdit(scheduleId).subscribe(result => {
                this.schedule = result.schedule;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._schedulesServiceProxy.createOrEdit(this.schedule)
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
