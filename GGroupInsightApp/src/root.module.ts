import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouteReuseStrategy, Routes } from '@angular/router';

import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
// import { TokenService } from './service/token.service';
import { IonicStorageModule } from '@ionic/Storage';

import { PermissionCheckerService } from 'abp-ng2-module/dist/src/auth/permission-checker.service';
import { RootComponent } from './root.component';
import { RootRoutingModule } from './root-routing.module';
import { API_BASE_URL, TokenAuthServiceProxy, QuanLyNghiPhepsServiceProxy } from './shared/service-proxies/service-proxies';
import { AuthService } from './service/auth.service';
import { AppConsts } from './shared/AppConsts';
import { TokenService } from 'abp-ng2-module/src/auth/token.service';
import { LogService } from 'abp-ng2-module/src/log/log.service';
import { UtilsService } from 'abp-ng2-module/src/utils/utils.service';
import { ZonesService } from './service/zones.service';
import { AbpSessionService } from 'abp-ng2-module/src/session/abp-session.service';
import { AppModule } from './app/app.module';
import { AbpMultiTenancyService } from 'abp-ng2-module/src/multi-tenancy/abp-multi-tenancy.service';
import { ServiceProxyModule } from './shared/service-proxies/service-proxy.module';
import { AbpModule } from 'abp-ng2-module/src/abp.module';

export function getRemoteServiceBaseUrl(): string {
  return AppConsts.remoteServiceBaseUrl;
}
@NgModule({
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    FormsModule,
    AppModule,
    AbpModule,
    ServiceProxyModule,
    RootRoutingModule,
    HttpClientModule,
    IonicStorageModule.forRoot()
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: API_BASE_URL, useFactory: getRemoteServiceBaseUrl },
    PermissionCheckerService
  ],
  declarations: [RootComponent],
  entryComponents: [],
  bootstrap: [RootComponent]
})
export class RootModule {}
