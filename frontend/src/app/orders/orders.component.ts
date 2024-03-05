import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Order from 'src/shared/models/Order';
import { OrderService } from 'src/shared/services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  orders: Order[];
  constructor(private orderService: OrderService, private router: Router) {}

  ngOnInit(): void {
    this.orderService.getAllOrders().subscribe({
      next: (res) => {
        // console.log(res);
        this.orders = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  updateStatus(orderId: string): void{
    const url = `/orders/update/${orderId}`
    this.router.navigateByUrl(url);
  }
}
