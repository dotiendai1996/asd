import { AuthenticateModel, AuthenticateResultModel } from './../../shared/service-proxies/service-proxies';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from './../../service/auth.service';
import { LoadingController } from '@ionic/angular';
import { UtilService } from './../../service/util.service';
import { AlertController } from '@ionic/angular';
import { Router } from '@angular/router';
import { AccountService } from '../../service/account.service';
import { AppEditionExpireAction } from '../../shared/AppEnums';
import { AbpSessionService } from '@abp/session/abp-session.service';
import { AppConsts } from '../../shared/AppConsts';
import { AppSessionService } from '../../shared/session/app-session.service';
import { TokenService } from 'abp-ng2-module/src/auth/token.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss']
})
export class LoginPage implements OnInit {
  submitting = false;
  tenantName = '';
  cookieTenantIdValue = '';
  constructor(
    private _authService: AuthService,
    private _loadingCtrl: LoadingController,
    private _utilService: UtilService,
    public _alertController: AlertController,
    private _router: Router,
    private _accountService: AccountService,
    private _sessionService: AppSessionService,
    private _tokenService: TokenService
  ) {

  }

  ngOnInit() {
    this._utilService.deleteCookie('token');
    this._utilService
    .getCookie('Abp.TenantId')
    .then(data => this.cookieTenantIdValue = data);
    this._utilService
      .getCookie('Abp.TenantName')
      .then(data => this.tenantName = data);
  }

  async onLogin(form: NgForm) {
    const loading = await this._loadingCtrl.create({
      message: 'Loggin you in...',
      duration: 2000
    });
    loading.present();

    const model = new AuthenticateModel();
    model.userNameOrEmailAddress=form.value.username;
    model.password= form.value.password;
    model.rememberClient= true;
    this._authService.authenticateModel = model;

    this.signIn(form.value.username, form.value.password);
  }
  signIn(username: string, password: string) {
    this._authService
      .signIn(username, password)
      .subscribe({
        next: (result: AuthenticateResultModel) => {
          const utilservice = this._utilService;
          const router = this._router;
          const sessionService = this._sessionService;
          const tokenService = this._tokenService;
          const tokenExpireDate = new Date(
            new Date().getTime() + 1000 * result.expireInSeconds
          );

          tokenService.setToken(
            result.accessToken,
            tokenExpireDate
          );

          utilservice.setCookieValue(
            AppConsts.authorization.encrptedAuthTokenName,
            result.accessToken,
            tokenExpireDate
          );
          sessionService.init().then((result) => {
              router.navigate(['app/main/home']);
            }, (err) => {
          });
        },
        error: async (err: any) => {
          const reader = new FileReader();
          const showError = this.ShowError;
          const alertController = this._alertController;
          const obj = JSON.parse(err);
          const alert = await alertController.create({
            header: 'Đăng nhập',
            subHeader: 'Đăng nhập thất bại',
            message: obj.error.details,
            buttons: ['OK']
          });

          await alert.present();
        }
    });
  }

  private GetBlobData(data: {}) {
    const reader = new FileReader();
    const utilservice = this._utilService;
    const router = this._router;
    const sessionService = this._sessionService;
    reader.onload = function() {
      const obj = JSON.parse(this.result);
      const tokenExpireDate = new Date(
        new Date().getTime() + 1000 * obj.result.expireInSeconds
      );
      
    };
    reader.readAsText(data['body']);
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

  // public async changeTenant() {
  //   const alert = await this._alertController.create({
  //     header: 'Enter Tenant',
  //     inputs: [
  //       {
  //         name: 'tenant',
  //         type: 'text',
  //         placeholder: 'Tenant'
  //       }
  //     ],
  //     buttons: [
  //       {
  //         text: 'Cancel',
  //         role: 'cancel',
  //         cssClass: 'secondary',
  //         handler: () => {
  //           console.log('Confirm Cancel');
  //         }
  //       },
  //       {
  //         text: 'Ok',
  //         handler: data => {
  //           console.log(data);
  //           this.setTenant(data.tenant);
  //         }
  //       }
  //     ]
  //   });

  //   await alert.present();
  // }
  // setTenant(name: string): any {
  //   const utilservice = this._utilService;
  //   this._accountService.isTenantAvailable(name).subscribe(data => {
  //     const tokenExpireDate = new Date(
  //       new Date().getTime() + 1000 * 1000
  //     );
  //     switch (data.body.result.state) {
  //       case AppTenantAvailabilityState.Available:
  //         utilservice.setCookieValue('Abp.TenantId', data.body.result.tenantId, tokenExpireDate);
  //         utilservice.setCookieValue('Abp.TenantName', name, tokenExpireDate);
  //         this.tenantName = name;
  //         return;
  //       case AppTenantAvailabilityState.InActive:
  //         alert('Tenant not active');
  //         break;
  //       case AppTenantAvailabilityState.NotFound:
  //         alert('Tenant not available');
  //         break;
  //     }
  //   });
  // }
}
