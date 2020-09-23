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
// import { AppEditionExpireAction } from '../../../shared/AppEnums';
@Component({
  selector: 'app-create-ditre',
  templateUrl: './CreateDiTre.page.html',
  styleUrls: ['./CreateDiTre.page.scss']
})
export class CreateDiTre implements OnInit {
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

  async onCreateOrEdit(form: NgForm) {
    const loading = await this._loadingCtrl.create({
      message: 'Đang xử lý........',
      duration: 2000
    });
    loading.present();

    // this.thongTinNghiPhep.nghiPhep = true;
    // this.thongTinNghiPhep.congTac = false;
    // this.thongTinNghiPhep.tangCa = false;
    // this.thongTinNghiPhep.trangThaiID = 4743;
    // this.thongTinNghiPhep.ngayBatDau = moment(this.startDate).utc(true);
    // this.thongTinNghiPhep.ngayKetThuc = moment(this.endDate).utc(true);
    // this.thongTinNghiPhep.lyDo=form.value.lydo;
    // this.thongTinNghiPhep.truongBoPhanID=form.value.truongBoPhanID;
    // this.createOrEditNghiPhepInput.nghiPhep = this.thongTinNghiPhep;
    // this.CreateOrEdit(this.createOrEditNghiPhepInput);
  }
  CreateOrEdit(CreateOrEditModel: CreateOrEditNghiPhepInput) {
    // const router = this._router;
    // console.log(this._quanLyNghiPhepsServiceProxy
    //   .createOrEdit(CreateOrEditModel));
    // this._quanLyNghiPhepsServiceProxy
    //     .createOrEdit(CreateOrEditModel)
    //     .subscribe(() => {
    //     });
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
