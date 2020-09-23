import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent
  ]
})
export class AppModule {}
