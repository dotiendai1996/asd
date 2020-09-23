import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { LabelsServiceProxy, CreateOrEditLabelDto, LabelDto, MenusServiceProxy, MenuDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditLabelModal',
    templateUrl: './create-or-edit-label-modal.component.html'
})
export class CreateOrEditLabelModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    codeOptions = [];

    label: CreateOrEditLabelDto = new CreateOrEditLabelDto();
    parentOptions: LabelDto[] = [];

    constructor(
        injector: Injector,
        private _labelsServiceProxy: LabelsServiceProxy,
        private _menuServiceProxy: MenusServiceProxy,
        private _labelServiceProxy: LabelsServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(){
        this._labelsServiceProxy.getAllLabels().subscribe(result => {
            this.parentOptions = result;
        });
        // this._menuServiceProxy.getAllMenuDto().subscribe(result => {
        //     this.parentOptions = result;
        // });
        this._labelServiceProxy.getAllSqlConfig().subscribe((res) => {
            console.log()
            this.codeOptions = res;
        });
    }

    show(labelId?: number): void {

        if (!labelId) {
            this.label = new CreateOrEditLabelDto();
            this.label.id = labelId;

            this.active = true;
            this.modal.show();
        } else {
            this._labelsServiceProxy.getLabelForEdit(labelId).subscribe(result => {
                this.label = result.label;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._labelsServiceProxy.createOrEdit(this.label)
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
