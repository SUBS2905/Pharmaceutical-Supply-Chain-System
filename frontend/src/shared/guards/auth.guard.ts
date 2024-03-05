import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private cookieService: CookieService) {}

  canActivate(): boolean {
    const sessionToken = this.cookieService.get('session');
    if (sessionToken) {
      return true;
    } else {
      this.router.navigateByUrl('/auth');
      return false;
    }
  }
}
