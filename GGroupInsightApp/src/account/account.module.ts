import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';
import { AccountRoutingModule } from './account-routing.module';
import { LoginPage } from './login/login.page';
import { LogoutPage } from './logout/logout.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AccountRoutingModule
  ],
  declarations: [
    LoginPage, LogoutPage
  ]
})
export class AccountModule {}
