import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { TinhThanhsServiceProxy, CreateOrEditTinhThanhDto, TinhThanhDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditTinhThanhModal',
    templateUrl: './create-or-edit-tinhThanh-modal.component.html'
})
export class CreateOrEditTinhThanhModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    ttExists = false;
    tinhThanh: CreateOrEditTinhThanhDto = new CreateOrEditTinhThanhDto();
    tenTinhThanhInitial: TinhThanhDto[] = [];
    tenTinhThanh = [];


    constructor(
        injector: Injector,
        private _tinhThanhsServiceProxy: TinhThanhsServiceProxy
    ) {
        super(injector);
        // this._tinhThanhsServiceProxy.getAllTinhThanh().subscribe((res)=>{
        //     this.tenTinhThanhInitial = res;
        // });
    }

    show(tinhThanhId?: number): void {

        if (!tinhThanhId) {
            this.tinhThanh = new CreateOrEditTinhThanhDto();
            this.tinhThanh.id = tinhThanhId;

            this.active = true;
            this.modal.show();
        } else {
            this.ttExists = false;
            
            this._tinhThanhsServiceProxy.getTinhThanhForEdit(tinhThanhId).subscribe(result => {
                this.tinhThanh = result.tinhThanh;

                // this.tenTinhThanh = this.tenTinhThanhInitial.map(x => x.tenTinhThanh);
                // let index = this.tenTinhThanh.findIndex(x => x == this.tinhThanh.tenTinhThanh);
                // this.tenTinhThanh.splice(index, 1);

                this.active = true;
                this.modal.show();
            });
        }
    }

    checkTenTinhThanhExists(): void {
        // console.log(this.tinhThanh.tenTinhThanh);
        // this._tinhThanhsServiceProxy.checkTenTinhThanh(this.tinhThanh.tenTinhThanh).subscribe(res => {
        //     console.log(res);
        //     this.ttExists = res;
        //     if(this.ttExists){
        //         this.message.warn("Tên Tỉnh thành đã bị trùng!");
        //         return;
        //     }
        // });
    }

    save(): void {
        // this._tinhThanhsServiceProxy.checkTenTinhThanh(this.tinhThanh.tenTinhThanh).subscribe(res => {
        //     console.log(res);
        //     this.ttExists = res;
        //     if(this.ttExists){
        //         this.message.warn("Tên Tỉnh thành đã bị trùng!");
        //         return;
        //     }
        //     else{
        //         this.saving = true;

			
        //         this._tinhThanhsServiceProxy.createOrEdit(this.tinhThanh)
        //          .pipe(finalize(() => { this.saving = false;}))
        //          .subscribe(() => {
        //             this.notify.info(this.l('SavedSuccessfully'));
        //             this.close();
        //             this.modalSave.emit(null);
        //          });
        //     }
        // }); 
        this._tinhThanhsServiceProxy.getAllTinhThanh().subscribe((res)=>{
            this.tenTinhThanhInitial = res;

            if(this.tenTinhThanhInitial.findIndex(x => x.tenTinhThanh == this.tinhThanh.tenTinhThanh && x.id !== this.tinhThanh.id) > -1){
                this.message.warn("Tên Tỉnh thành đã bị trùng!");
                return;
            }

            if(this.tenTinhThanhInitial.findIndex(x => x.maTinhThanh == this.tinhThanh.maTinhThanh && x.id !== this.tinhThanh.id) > -1){
                this.message.warn("Mã Tỉnh thành đã bị trùng!");
                return;
            }
            this.saving = true;

			
            this._tinhThanhsServiceProxy.createOrEdit(this.tinhThanh)
                .pipe(finalize(() => { this.saving = false;}))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close();
                    this.modalSave.emit(null);
                }); 
         
        });

        // if(this.tenTinhThanh.findIndex(x => x == this.tinhThanh.tenTinhThanh) > -1){
        //     this.message.warn("Tên Tỉnh thành đã bị trùng!");
        //     return;
        // }
            
        // this.saving = true;

			
        // this._tinhThanhsServiceProxy.createOrEdit(this.tinhThanh)
        //     .pipe(finalize(() => { this.saving = false;}))
        //     .subscribe(() => {
        //         this.notify.info(this.l('SavedSuccessfully'));
        //         this.close();
        //         this.modalSave.emit(null);
        //     });           
    }

    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
