import { BehaviorSubject, Observable } from "rxjs";
import { HttpClient, HttpEvent, HttpRequest } from "@angular/common/http";
import { map } from "rxjs/operators"; 
import { Inject, Injectable, InjectionToken, Optional } from "@angular/core";
import { Route } from "@angular/compiler/src/core";
import { Router } from "@angular/router";
import { AppComponent } from 'src/app/app.component';
import { MessageService } from 'primeng/api';
import { Observer } from "rxjs/internal/types";
import { User } from "./User";
export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');
import jwt_decode from "jwt-decode";
import { AccountClient } from "../client/apiclient";

@Injectable()
export class AuthHelper {
  private currentUserSubject: BehaviorSubject<User>;
  private tokenRefreshing = false;
  public currentUser: Observable<User>;
  //loginDTO: LoginDTO = Object.create(null);
  userData: User = Object.create(null);
  TOKEN_KEY = "Token";
  REFRESH_TOKEN_KEY = "RefreshToken";
  constructor(  private router: Router,private userClient: AccountClient,
    private messageService: MessageService) {
    this.currentUserSubject = new BehaviorSubject<User>(
      JSON.parse(sessionStorage.getItem("currentUser")!)
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }
  public login(username: string, password: string): Observable<boolean> {

    return new Observable<boolean>((observer: Observer<boolean>) => {

      this.userClient.login(username, password).subscribe(response => {
        if (response.isSuccess) {
          this.userData.userName = username;

          this.userData.token = response!.data!.token!;
          this.userData.name = username;

          sessionStorage.setItem(this.TOKEN_KEY, response!.data!.token!);
          sessionStorage.setItem('currentUser', JSON.stringify(this.userData));
          this.currentUserSubject.next(this.userData);
          if (response.errors!.length > 0) {
            
          }
          else {
            this.router.navigateByUrl('/admin/home');
          }
          observer.next(true);
        }
        else {
          this.messageService.add({
            severity: "error",
            summary: "Error",
            detail: "Invalid username or password"
          });
          observer.next(false); 
        }
      });
    });
  }
  logout() {
    // remove user from session storage to log user out
    sessionStorage.removeItem('currentUser');
    sessionStorage.removeItem(this.TOKEN_KEY);
    sessionStorage.removeItem(this.REFRESH_TOKEN_KEY);

    this.currentUserSubject.next(Object.create(null));

    this.router.navigateByUrl('/landing/login');
  }
  public loginUsingToken(token: string, username: string): void {
    this.userData.userName = username;
    this.userData.token = token;
    sessionStorage.setItem(this.TOKEN_KEY, token);
    sessionStorage.setItem('currentUser', JSON.stringify(this.userData));
    this.currentUserSubject.next(this.userData);
  }

  get Token(): string | null {
    return sessionStorage.getItem(this.TOKEN_KEY);
  }
  setToken(token: string) {
    sessionStorage.setItem(this.TOKEN_KEY, token);
  }
  public isAuthenticated(): boolean {
    const token = sessionStorage.getItem(this.TOKEN_KEY);
    if (token == null) {
      return false;
    } else {
      return true;
    }
  }
  private getDataFromToken(token: any) {
    let data = {};
    if (token !== null) {
      data = jwt_decode(token);
    }
    return data;
  }
  public hasPermission(action: Actions) {
    var data: any = this.getDataFromToken(this.Token);
    var role: string[] = data["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    if (typeof role == "string") {
      return role == action
    }
    else {
      let f = role.find(r => r == action);
      return f != undefined;
    }
  } 
}
enum ErrorCodes {
  NoProfileRegisteredWarningCode = "WRN01",
  InvalidUserNameOrPasswordCode = "ERR01",
  AccountAlreadyConfirmedCode = "ERR02",
  AccountMustBeConfirmedCode = "ERR03",
  AccountAlreadyBeConfirmedCode = "ERR04"
}
export enum Actions {
  Dashboard_GetStatistics = "Dashboard.GetStatistics",
} 