import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import {  HoSoDto, CreateOrEditHoSoDto, HoSosServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'viewHoSoModal',
    templateUrl: './view-hoSo-modal.component.html'
})
export class ViewHoSoModalComponent extends AppComponentBase implements OnInit {

    // @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    // @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    profilePicture = AppConsts.appBaseUrl + '/assets/common/images/default-profile-picture.png';
    active = false;
    saving = false;
    hoSo: any;
    hoSoID: any;
    item: CreateOrEditHoSoDto;


    constructor(
        private _hoSosServiceProxy: HoSosServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private router: Router,
        injector: Injector

    ) {
        super(injector);
        // this.item = new CreateOrEditHoSoDto();

        // this.profilePicture = AppConsts.remoteServiceBaseUrl + "\\" + cValue + "\\" + x.name;
        // this.item.hoSo = new HoSoDto();
    }
    ngOnInit(): void {
        const Id = this._activatedRoute.snapshot.paramMap.get('id');
        this.hoSoID = Number.parseInt(Id);
        this._hoSosServiceProxy.getHoSoForView(this.hoSoID).subscribe(result => {
            this.hoSo = result;
            console.log(this.hoSo)
            this.profilePicture = this.hoSo.anhDaiDien;
            console.log(this.profilePicture)
        });

    }
    troVe() {
        this.router.navigate(['/app/main/qlns/hoSos']);
    }
    // show(item: CreateOrEditHoSoDto): void {

    //     this.active = true;
    //     this.modal.show();
    // }

    close(): void {
        this.active = false;
        // this.modal.hide();
    }
}
