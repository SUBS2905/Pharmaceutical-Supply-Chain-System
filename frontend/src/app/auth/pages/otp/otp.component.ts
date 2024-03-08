import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import User from 'src/shared/models/User';
import { AuthService } from 'src/shared/services/auth.service';

@Component({
  selector: 'app-otp',
  templateUrl: './otp.component.html',
  styleUrls: ['./otp.component.scss'],
})
export class OtpComponent implements OnInit {
  otpForm: FormGroup;
  userData: User;
  token: string;
  // loginFailed = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    // this.isFailed();
    this.subscribeToUserData();
  }

  initializeForm(): void {
    this.otpForm = this.fb.group({
      otp: this.fb.control('', [Validators.required]),
    });
  }

  subscribeToUserData(): void {
    this.authService.userSubject.subscribe({
      next: (res) => {
        this.userData = res;
        // console.log('UserData:', this.userData);
      },
    });
  }

  // isFailed(): void {
  //   this.authService.errorSubject.subscribe({
  //     next: (err) => {
  //       this.loginFailed = err;
  //     },
  //   });
  // }

  onSubmit(): void {
    this.authService.verifyOtp(this.userData.email, this.otpForm.value.otp);
  }
}
