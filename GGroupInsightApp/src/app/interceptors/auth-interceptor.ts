 
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { UtilService } from '../service/util.service';
import { UtilService } from '../../service/util.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private util: UtilService) {
        this.util.getCookieValue('token');
    }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        // Clone the request and replace the original headers with
        // cloned headers, updated with the authorization.
        const token=abp.auth.getToken();
        const authReq = req.clone({
          headers: req.headers.set(
            'Authorization',
              'Bearer ' + token
          )
        });

        // send cloned request with header to the next handler.
        return next.handle(authReq);
    }
}