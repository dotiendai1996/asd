import { AuthenticateModel } from '.././../../shared/service-proxies/service-proxies';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../service/auth.service';
import { LoadingController } from '@ionic/angular';
import { UtilService } from '../../../service/util.service';
import { AlertController } from '@ionic/angular';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { CreateOrEditQuanLyNghiPhepDto, QuanLyNghiPhepsServiceProxy, CreateOrEditNghiPhepInput } from '../../../shared/service-proxies/service-proxies';
import { AppSessionService } from '../../../shared/session/app-session.service';

@Component({
  selector: 'app-create-nghiphep',
  templateUrl: './CreateNghiPhep.page.html',
  styleUrls: ['./CreateNghiPhep.page.scss']
})
export class CreateNghiPhep implements OnInit {
  thongTinNghiPhep: CreateOrEditQuanLyNghiPhepDto= new CreateOrEditQuanLyNghiPhepDto();
  createOrEditNghiPhepInput: CreateOrEditNghiPhepInput = new CreateOrEditNghiPhepInput();
  startDate: String = new Date().toISOString();
  endDate : String = new Date().toISOString();
  appSession: AppSessionService;
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

  async onCreateOrEdit(form: NgForm) {
    const loading = await this._loadingCtrl.create({
      message: 'Đang xử lý........',
      duration: 2000
    });
    loading.present();

    var startDate=form.value.startDate;
    var endDate=form.value.endDate;
    this.thongTinNghiPhep.nghiPhep = true;
    this.thongTinNghiPhep.congTac = false;
    this.thongTinNghiPhep.tangCa = false;
    this.thongTinNghiPhep.trangThaiID = 4743;
    if (typeof (startDate) !== 'string'){
      this.thongTinNghiPhep.ngayBatDau = moment().year(startDate.year.text).month(startDate.month.text-1).date(startDate.day.text).utc(true);
    }
    else{
      this.thongTinNghiPhep.ngayBatDau=moment(startDate).utc(true);
    }
    if (typeof (endDate) !== 'string'){
      this.thongTinNghiPhep.ngayKetThuc = moment().year(endDate.year.text).month(endDate.month.text-1).date(endDate.day.text).utc(true);
    }
    else{
      this.thongTinNghiPhep.ngayKetThuc=moment(endDate).utc(true);
    }
    this.thongTinNghiPhep.lyDo=form.value.lydo;
    this.thongTinNghiPhep.truongBoPhanID=form.value.truongBoPhanID;
    this.createOrEditNghiPhepInput.nghiPhep = this.thongTinNghiPhep;
    this.CreateOrEdit(this.createOrEditNghiPhepInput);
  }
  CreateOrEdit(CreateOrEditModel: CreateOrEditNghiPhepInput) {
    const router = this._router;
    this._quanLyNghiPhepsServiceProxy
        .createOrEdit(CreateOrEditModel)
        .subscribe(() => {
        });
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
