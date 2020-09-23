import { Component } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';

@Component({
  selector: 'app-root',
  templateUrl: 'root.component.html'
})
export class RootComponent {
  public appPages = [
    {
      title: 'Home',
      url: '/app/main/home',
      icon: 'home'
    },
    {
      title: 'Nghỉ Phép',
      url: '/app/main/nghiphep',
      icon: 'medkit'
    },
    {
      title: 'Thông tin chấm công',
      url: '/app/main/qlns/dataChamCong',
      icon: 'list-box'
    },
    {
      title: 'Logout',
      url: '/app/logout',
      icon: 'exit'
    }
  ];

  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }
}
