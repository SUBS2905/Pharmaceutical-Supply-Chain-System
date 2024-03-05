import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import User from 'src/shared/models/User';
import { AuthService } from 'src/shared/services/auth.service';
import { OrderService } from 'src/shared/services/order.service';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss'],
})
export class CreateOrderComponent implements OnInit {
  createOrderForm: FormGroup;
  createOrderFailed = false;
  productID: string;
  userDetails: User;

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private authService: AuthService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.getUserDetails();

    this.route.params.subscribe({
      next: (params) => {
        this.productID = params['productID'];
      },
    });
  }

  initializeForm(): void {
    this.createOrderForm = this.fb.group({
      quantity: [[Validators.required, Validators.min(1)]],
    });
  }

  onSubmit(): void {
    if (this.createOrderForm.valid) {
      const formValue = this.createOrderForm.value;
      formValue.orderID = '';
      formValue.productID = this.productID;
      formValue.customerID = this.userDetails.userID;
      formValue.orderStatus = 'placed';
      console.log(formValue);
      
      this.orderService.createOrder(formValue);
    } else {
      console.log('Form is invalid');
    }
  }

  onFailed(): void {
    this.orderService.error$.subscribe({
      next: (err) => {
        this.createOrderFailed = err;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getUserDetails(): void {
    this.authService.getUserById().subscribe({
      next: (user) => {
        this.userDetails = user;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
