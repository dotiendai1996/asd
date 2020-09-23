import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../service/auth.service';
import { UtilService } from '../../../service/util.service';
import { AppConsts } from '../../../shared/AppConsts';
import * as moment from 'moment';
import { AlertController } from '@ionic/angular';
@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  today: any;
  moment: any = moment;
  constructor(
    private _authService: AuthService,
    private _utilService: UtilService,
    private _router: Router,
    public _alertController: AlertController,
  ) { }

  ngOnInit() {
    this._utilService.getCookieValue(AppConsts.authorization.encrptedAuthTokenName);
    this.today = new Date();
    setInterval(() => {
      this.moment = moment;
    }, 1000);
  }
  async presentAlert() {
    const alert = await this._alertController.create({
      header: 'THÔNG BÁO',
      message: 'Bạn vui lòng truy cập wifi công ty để check in',
      buttons: ['Đóng']
    });

    await alert.present();
  }
}
