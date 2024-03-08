import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'src/shared/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  userRole: string;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.getUserById().subscribe({
      next: (res) => {
        this.userRole = res.role;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onLogout(): void {
    this.cookieService.delete('jwt');
    this.cookieService.delete('session');
    this.router.navigateByUrl('/auth');
  }
}
