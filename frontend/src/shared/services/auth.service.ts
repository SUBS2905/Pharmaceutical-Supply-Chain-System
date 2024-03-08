import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import User from '../models/User';
import { HttpClient } from '@angular/common/http';
import LoginResponse from '../models/LoginResponse';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authEndpoint = 'https://localhost:7024/api/Auth';
  private loginEndpoint = `${this.authEndpoint}/login`;
  private registerEndpoint = `${this.authEndpoint}/register`;
  private otpEndpoint = `${this.authEndpoint}/verify-otp`;
  private getUserEndpoint = `${this.authEndpoint}/user`;

  userSubject = new BehaviorSubject<User>(null);

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router
  ) {}

  registerUser(email: string, role: string, password: string): void {
    const body = {
      userID: '',
      role,
      email,
      password,
      mfaData: {
        otpSecretKey: '',
        lastOTPGeneration: '2024-02-29T14:02:37.253Z',
        deliveryEmail: '',
        recoveryCodes: [],
        attemptCounter: 0,
      },
    };

    this.http.post<User>(this.registerEndpoint, body).subscribe({
      next: (res) => {
        this.userSubject.next(res);
        this.router.navigateByUrl('/auth/login')
      },
      error: (err) => {
        alert('User already exists');
        console.log(err);
      },
    });
  }

  loginUser(email: string, password: string): void {
    const body = { email, password };
    this.http.post<User>(this.loginEndpoint, body).subscribe({
      next: (res) => {
        this.userSubject.next(res);
        this.router.navigateByUrl('/auth/otp');
      },
      error: (err) => {
        alert('Incorrect Credentials');
        console.error(err);
      },
    });
  }

  verifyOtp(email: string, otp: string): void {
    const body = { email, otp };
    // console.log(body);

    this.http.post<LoginResponse>(this.otpEndpoint, body).subscribe({
      next: (res) => {
        this.userSubject.next(res.user);
        this.cookieService.set('jwt', res.token, 2, '/');
        this.cookieService.set('session', res.user.userID, 2, '/');
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        // this.errorSubject.next(true);
        alert('Incorrect OTP');
        console.error(err);
      },
    });
  }

  getUserById(): Observable<User> {
    const userId = this.cookieService.get('session');
    return this.http.get<User>(`${this.getUserEndpoint}?userId=${userId}`);
  }
}
