import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import User from 'src/shared/models/User';
import { AuthService } from 'src/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  userData: User;
  loginFailed = false;
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.isFailed();
  }

  initializeForm(): void {
    this.loginForm = this.fb.group({
      email: this.fb.control('', [Validators.required, Validators.email]),
      password: this.fb.control('', [Validators.required]),
    });
  }

  onSubmit(): void {
    // console.log(this.loginForm.value);

    this.authService.loginUser(
      this.loginForm.value.email,
      this.loginForm.value.password
    );
    this.updateUser();
  }

  updateUser(): void {
    this.authService.userSubject.subscribe({
      next: (user) => {
        this.userData = user;
        //how to prevent navigation if incorrect credentials
        if (!this.loginFailed) this.router.navigateByUrl('/auth/otp');
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  isFailed(): void {
    this.authService.errorSubject.subscribe({
      next: (err) => {
        this.loginFailed = err;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
