import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AccordionModule } from 'primeng/accordion';     //accordion and accordion tab
import { LandingModule } from './landing/landing.module';
import { environment } from 'src/environments/environment';
import { AnimalClient, API_BASE_URL } from './client/apiclient';
import { AuthHelper } from './helpers/auth.helper';
import { DateHelper } from './helpers/date.helper';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { AdminModule } from './admin/admin.module';
import { LoadingspinnerComponent } from './shared/loadingspinner/loadingspinner.component';
import { LoaderService } from './shared/loadingspinner/loader.service';
import { LoaderInterceptorService } from './shared/loadingspinner/loader-interceptor.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
        LoadingspinnerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AccordionModule, LandingModule,AdminModule,ProgressSpinnerModule ,CommonModule
  ],
  exports:[LoadingspinnerComponent],
  bootstrap: [AppComponent],
  providers: [AnimalClient,AuthHelper,DateHelper,
    { provide: API_BASE_URL, useFactory: getBaseUrl },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    LoaderService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptorService,
      multi: true
    }
  ]
})
export class AppModule { }

export function getBaseUrl(): string {
  return environment.baseUrl;
}

