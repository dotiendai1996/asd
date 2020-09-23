import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanLoad, Router, RouterStateSnapshot } from '@angular/router';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { Observable } from '@node_modules/rxjs/internal/Observable';

@Injectable()
export class AppRouteGuard implements CanActivate, CanActivateChild, CanLoad {

    constructor(
        private _permissionChecker: PermissionCheckerService,
        private _router: Router,
        private _sessionService: AppSessionService
    ) { }

    canActivateInternal(data: any, state: RouterStateSnapshot): boolean {
        if (UrlHelper.isInstallUrl(location.href)) {
            return true;
        }

        if (!this._sessionService.user) {
            console.log("vào");
            this._router.navigate(['/account/login']);
            return false;
        }

        if (!data || !data['permission']) {
            return true;
        }

        if (this._permissionChecker.isGranted(data['permission'])) {
            return true;
        }

        this._router.navigate([this.selectBestRoute()]);
        return false;
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivateInternal(route.data, state);
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }

    canLoad(route: any): Observable<boolean> | Promise<boolean> | boolean {
        return this.canActivateInternal(route.data, null);
    }

    selectBestRoute(): string {

        if (!this._sessionService.user) {
            return '/app/main/qlns/ungViens';
        }

        if (this._permissionChecker.isGranted('Pages.Administration.Host.Dashboard')) {
            return '/app/main/qlns/ungViens';
        }

        if (this._permissionChecker.isGranted('Pages.Tenant.Dashboard')) {
            return '/app/main/qlns/ungViens';
        }

        if (this._permissionChecker.isGranted('Pages.Tenants')) {
            return '/app/main/qlns/ungViens';
        }

        if (this._permissionChecker.isGranted('Pages.Administration.Users')) {
            return '/app/main/qlns/ungViens';
        }

        return '/app/main/qlns/ungViens';
    }
}
