import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';
import { UtilsService } from '../../../node_modules/abp-ng2-module/dist/src/utils/utils.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.page.html',
  styleUrls: ['./logout.page.scss'],
})
export class LogoutPage implements OnInit {
  constructor(
    private _authService: AuthService,
    private _utilService: UtilsService,
    private _router: Router
  ) { }

  ngOnInit() {
    this._utilService
      .deleteCookie('enc_auth_token');
    this._router.navigate(['login']);
  }
}
