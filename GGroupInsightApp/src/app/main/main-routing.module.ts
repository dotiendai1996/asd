import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        children: [
          {
            path: 'home', loadChildren: './home/home.module#HomePageModule'
          },
          {
            path: 'nghiphep', loadChildren: './nghi-phep/qlNghiPhep.module#CreateNghiPhepModule'
          }
        ]
      },
      {
        path: 'qlns',
        children: [
          {
            path: 'xinditre', loadChildren: '../qlns/diTres/qlDiTre.module#CreateDiTreModule'
          },
          {
            path: 'dataChamCong', loadChildren: '../qlns/ChamCong/dataChamCong.module#dataChamCongModule'
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class MainRoutingModule { }
