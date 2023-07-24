import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { LoaderService } from './loader.service';
import { LoadingspinnerComponent } from './loadingspinner.component';
import { Observable } from 'rxjs';

@Injectable()
export class LoaderInterceptorService implements HttpInterceptor {

  constructor(private injector: Injector, private loaderService: LoaderService) {

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const loaderS = this.injector.get(LoaderService);

    // setTimeout(function () {
    // loaderS.show();
    // }, 200);

    this.loaderService.show();

    return next.handle(req).pipe(tap((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {

        setTimeout(function () {
          loaderS.hide();
        }, 500);
        // this.onEnd();
      }
    },
      (err: any) => {
        setTimeout(function () {
          loaderS.hide();
        }, 500);
        // this.onEnd();
      }));

  }

  private onEnd(): void {
    this.hideLoader();
  }

  private showLoader(): void {
    this.loaderService.show();
  }

  private hideLoader(): void {
    this.loaderService.hide();
  }

}
