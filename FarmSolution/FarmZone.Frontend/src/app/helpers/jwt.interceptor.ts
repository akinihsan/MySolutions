import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable, Injector } from '@angular/core';
import { AuthHelper } from './auth.helper';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private injector: Injector, private http: HttpClient) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authHelper = this.injector.get(AuthHelper);
        let currentUser = authHelper.currentUserValue;
        if (currentUser && currentUser.token) {
            if (request.headers.get('Authorization') == null) {
                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${currentUser.token}`
                    }
                });
            }
        }

        return next.handle(request);
    }
}
