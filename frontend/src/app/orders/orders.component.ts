import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Order from 'src/shared/models/Order';
import User from 'src/shared/models/User';
import { AuthService } from 'src/shared/services/auth.service';
import { OrderService } from 'src/shared/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  userData: User;
  orders: Order[];
  constructor(
    private orderService: OrderService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getUserRole();
  }

  getAllOrders(): void {
    this.orderService.getAllOrders().subscribe({
      next: (res) => {
        this.orders = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getCurrUserOrders(): void {
    this.orderService.getOrdersByCustomerID(this.userData.userID).subscribe({
      next: (res) => {
        this.orders = res;
      },
      error: (err) => console.log(err),
    });
  }

  getUserRole(): void {
    this.authService.getUserById().subscribe({
      next: (user) => {
        this.userData = user;
        if (this.userData.role === 'retailer') {
          this.getCurrUserOrders();
        } else {
          this.getAllOrders();
        }
      },
      error: (err) => console.log(err),
    });
  }

  updateStatus(orderId: string): void {
    const url = `/orders/update/${orderId}`;
    this.router.navigateByUrl(url);
  }
}
