import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthHelper } from './auth.helper';

@Injectable()
export class AuthGuardService implements CanActivate {

    constructor(private router: Router, private authService: AuthHelper) {

    }

    canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | UrlTree { 
        if (!this.authService.isAuthenticated()) {
            alert('You dont have permission to see this page.'); 
            this.router.navigate(["landing/login"], { queryParams: { retUrl: route.url } });
            return false; 
        }
 
        return true;
    }
    getResolvedUrl(route: ActivatedRouteSnapshot): string {
        return route.pathFromRoot
            .map(v => v.url.map(segment => segment.toString()).join('/'))
            .join('/');
    }

}