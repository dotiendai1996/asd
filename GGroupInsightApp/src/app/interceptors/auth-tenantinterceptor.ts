
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UtilService } from '../../service/util.service';

@Injectable()
export class AuthTenantInterceptor implements HttpInterceptor {

    cookieTenantIdValue: string;
    constructor(private util: UtilService) {
        this.util
          .getCookie('Abp.TenantId')
          .then(data => (this.cookieTenantIdValue = data));
          // abp.multiTenancy.setTenantIdCookie(1);
          // console.log(abp.multiTenancy.tenantIdCookieName);
          // console.log(abp.multiTenancy.getTenantIdCookie() + '');
     }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        // Clone the request and replace the original headers with
        // cloned headers, updated with the authorization.
        if(this.cookieTenantIdValue!=undefined){
          const authReq = req.clone({
          headers: req.headers.set(
            'Abp.TenantId',
              "1"
          )
        });
          return next.handle(authReq);
        }
        return next.handle(req);
        
        // send cloned request with header to the next handler.
        
    }
}