import { Injectable, Injector } from "@angular/core";
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpClient, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { Observable, throwError, BehaviorSubject, of } from "rxjs";
import { catchError, filter, map } from "rxjs/operators";
import { MessageService, Message } from 'primeng/api';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;
  constructor(private messageService: MessageService, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse, caught: any) => {
        if (err.status === 400) {
          if (err.error) {
            this.blobToText(err.error).pipe(_observableMergeMap(_responseText => {
              let resultData = _responseText === "" ? null : JSON.parse(_responseText);

              (resultData.errors as any[]).forEach(msg => { console.log(msg); this.messageService.add({ severity: 'error', summary: 'Service Message', detail: msg.message }) });

              let errMsg = (resultData.message) ? resultData.message :
                resultData.status ? `${resultData.status} - ${resultData.statusText}` : 'Server error';

              return throwError(() => errMsg);
            })).subscribe();
          }
        }

        return throwError(() => err);
      }));
  }

  blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
      if (!blob) {
        observer.next("");
        observer.complete();
      } else {
        let reader = new FileReader();

        reader.onload = event => {
          observer.next((<any>event.target).result);
          observer.complete();
        };

        reader.readAsText(blob);
      }
    });
  }
}
