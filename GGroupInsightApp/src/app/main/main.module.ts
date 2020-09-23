import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { MainRoutingModule } from './main-routing.module';
import { HomePage } from './home/home.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MainRoutingModule
  ],
  declarations: [
    
  ]
})
export class MainModule {}
