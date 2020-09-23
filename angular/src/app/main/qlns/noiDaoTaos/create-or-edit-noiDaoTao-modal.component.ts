import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { NoiDaoTaosServiceProxy, CreateOrEditNoiDaoTaoDto, NoiDaoTaoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditNoiDaoTaoModal',
    templateUrl: './create-or-edit-noiDaoTao-modal.component.html'
})
export class CreateOrEditNoiDaoTaoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    mndtExists = false;
    noiDaoTao: CreateOrEditNoiDaoTaoDto = new CreateOrEditNoiDaoTaoDto();

    noiDtForCheckCMND: NoiDaoTaoDto[] = [];

    constructor(
        injector: Injector,
        private _noiDaoTaosServiceProxy: NoiDaoTaosServiceProxy
    ) {
        super(injector);
    }

    show(noiDaoTaoId?: number): void {

        if (!noiDaoTaoId) {
            this.noiDaoTao = new CreateOrEditNoiDaoTaoDto();
            this.noiDaoTao.id = noiDaoTaoId;

            this.active = true;
            this.modal.show();
        } else {
            this._noiDaoTaosServiceProxy.getNoiDaoTaoForEdit(noiDaoTaoId).subscribe(result => {
                this.noiDaoTao = result.noiDaoTao;


                this.active = true;
                this.modal.show();
            });
        }
    }

    // checkTenNoiDaoTaoExists(): void {
    //     this._noiDaoTaosServiceProxy.checkNoiDaoTaoName(this.noiDaoTao.tenNoiDaoTao).subscribe(res => {
    //         this.mndtExists = res;
    //         if(this.mndtExists){
    //             this.message.warn("Tên Tỉnh thành đã bị trùng!");
    //             return;
    //         }
    //     });
    // }

    // checkMaNoiDaoTaoExists(): void {
    //     this._noiDaoTaosServiceProxy.checkNoiDaoTaoCode(this.noiDaoTao.maNoiDaoTao).subscribe(res => {
    //         this.mndtExists = res;
    //         if(this.mndtExists){
    //             this.message.warn("Tên Tỉnh thành đã bị trùng!");
    //             return;
    //         }
    //     });
    // }

    save(): void {
        this._noiDaoTaosServiceProxy.getAllNoiDaoTao().subscribe((res)=>{
            this.noiDtForCheckCMND = res;

            if(this.noiDtForCheckCMND.findIndex(x => x.maNoiDaoTao == this.noiDaoTao.maNoiDaoTao && x.id !== this.noiDaoTao.id) > -1){
                this.message.warn("Mã nơi đào tạo đã bị trùng!");
                return;
            }
            if(this.noiDtForCheckCMND.findIndex(x => x.tenNoiDaoTao == this.noiDaoTao.tenNoiDaoTao && x.id !== this.noiDaoTao.id) > -1){
                this.message.warn("Tên nơi đào tạo đã bị trùng!");
                return;
            }
            this.saving = true;
			
            this._noiDaoTaosServiceProxy.createOrEdit(this.noiDaoTao)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
        })
            
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
