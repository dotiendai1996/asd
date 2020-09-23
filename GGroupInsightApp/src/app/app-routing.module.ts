import { AppRouteGuard } from '../shared/auth/auth-route-guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: 'app',
    component: AppComponent,
    canActivate: [AppRouteGuard],
    canActivateChild: [AppRouteGuard],
    children: [
        // {
        //   path: '', redirectTo: '/main/home', pathMatch: 'full'
        // },
        {
          path: 'main', loadChildren: './main/main.module#MainModule'
        }, 
        {
            path: '**', redirectTo: 'notifications'
        }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
