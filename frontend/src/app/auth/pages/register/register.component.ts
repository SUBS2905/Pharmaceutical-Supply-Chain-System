import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  showPassword = false;

  constructor(private fb: FormBuilder, private authService: AuthService) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.registerForm = this.fb.group({
      email: this.fb.control('', [Validators.required, Validators.email]),
      role: this.fb.control('retailer', [Validators.required]),
      password: this.fb.control('', [Validators.required]),
    });
  }

  onSubmit(): void {
    // console.log(this.registerForm.value);
    if (this.registerForm.valid) {
      this.authService.registerUser(
        this.registerForm.value.email,
        this.registerForm.value.role,
        this.registerForm.value.password
      );
    } else {
      console.log('Invalid Form');
    }
  }
}
