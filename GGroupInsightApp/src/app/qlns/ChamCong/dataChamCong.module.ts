import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { dataChamCong } from './dataChamCong.page';

const routes: Routes = [
  {
    path: '',
    component: dataChamCong
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [dataChamCong]
})
export class dataChamCongModule {}
