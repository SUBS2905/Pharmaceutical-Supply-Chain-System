import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable } from 'rxjs';
import Order from '../models/Order';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private jwt = this.cookieService.get('jwt');
  private ordersEndpoint = 'https://localhost:7024/api/Order';
  private headers = new HttpHeaders().set(
    'Authorization',
    `Bearer ${this.jwt}`
  );

  order$ = new BehaviorSubject<Order>(null);

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router
  ) {}

  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.ordersEndpoint, {
      headers: this.headers,
    });
  }

  getOrdersByCustomerID(customerId: string): Observable<Order[]> {
    const body = {
      customerId,
    };
    return this.http.post<Order[]>(`${this.ordersEndpoint}/getByCId`, body, {
      headers: this.headers,
    });
  }

  createOrder(order: Order): void {
    this.http
      .post<Order>(this.ordersEndpoint, order, { headers: this.headers })
      .subscribe({
        next: (res) => {
          // console.log(res);
          this.order$.next(res);
          this.router.navigateByUrl('/orders');
        },
        error: (err) => {
          console.log(err);
          alert('Unable to place order');
        },
      });
  }

  updateOrderStatus(orderId: string, newStatus: string): void {
    const body = {
      orderId,
      newStatus,
    };
    this.http
      .post<Order>(`${this.ordersEndpoint}/status`, body, {
        headers: this.headers,
      })
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/orders');
        },
        error: (err) => {
          console.error(err);
        },
      });
  }
}
