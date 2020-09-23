import { AuthenticateModel } from '.././../../shared/service-proxies/service-proxies';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
// import { AuthService } from '../../service/auth.service';
import { LoadingController } from '@ionic/angular';
import { UtilService } from '../../../service/util.service';
import { AlertController } from '@ionic/angular';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { CreateOrEditQuanLyNghiPhepDto, QuanLyNghiPhepsServiceProxy, CreateOrEditNghiPhepInput } from '../../../shared/service-proxies/service-proxies';
// import { AppEditionExpireAction } from '../../../shared/AppEnums';
@Component({
  selector: 'app-data-chamcong',
  templateUrl: './dataChamCong.page.html',
  styleUrls: ['./dataChamCong.page.scss']
})
export class dataChamCong implements OnInit {
  thongTinNghiPhep: CreateOrEditQuanLyNghiPhepDto= new CreateOrEditQuanLyNghiPhepDto();
  createOrEditNghiPhepInput: CreateOrEditNghiPhepInput = new CreateOrEditNghiPhepInput();
  startDate =  moment().format();
  endDate =  moment().format();
  constructor(
    private _loadingCtrl: LoadingController,
    private _utilService: UtilService,
    public _alertController: AlertController,
    private _router: Router,
    private _quanLyNghiPhepsServiceProxy: QuanLyNghiPhepsServiceProxy
  ) {

  }

  ngOnInit() {
  }


  private async ShowError(data: {}) {
    const obj: any = data;
    const alert = await this._alertController.create({
      header: 'Error',
      subHeader: obj.error.message,
      message: obj.error.details,
      buttons: ['OK']
    });

    await alert.present();
  }
}
