import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from 'src/shared/services/auth.service';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ARGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(): Observable<boolean> {
    return this.authService.getUserById().pipe(
      map((user) => {
        if (user.role === 'admin' || user.role === 'regulatoryAuthority') {
          return true;
        } else {
          this.router.navigateByUrl('/');
          return false;
        }
      })
    );
  }
}
