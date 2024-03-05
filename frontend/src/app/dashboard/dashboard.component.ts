import { Component, OnInit } from '@angular/core';
import Inventory from 'src/shared/models/Inventory';
import Order from 'src/shared/models/Order';
import Product from 'src/shared/models/Product';
import User from 'src/shared/models/User';
import { AuthService } from 'src/shared/services/auth.service';
import { InventoryService } from 'src/shared/services/inventory.service';
import { OrderService } from 'src/shared/services/order.service';
import { ProductService } from 'src/shared/services/product.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  userData: User;
  productData: Product[];
  inventoryData: Inventory[];
  orderData: Order[];
  customerOrderData: Order[];

  constructor(
    private authService: AuthService,
    private inventoryService: InventoryService,
    private orderService: OrderService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.getUserDetails();
  }

  extractDate(date: Date): string {
    const dateStr = date.toString();
    return dateStr.split('T')[0];
  }

  getInventoryColour(level: string): string {
    if (level === 'low') return 'yellow';
    else if (level === 'very low') return 'orange';
    else if (level === 'critically low') return 'red';
    else return 'green';
  }

  getUserDetails(): void {
    this.authService.getUserById().subscribe({
      next: (res) => {
        this.userData = res;
        // console.log(this.userData);

        if (
          this.userData.role === 'admin' ||
          this.userData.role === 'manufacturer' ||
          this.userData.role === 'distributor' ||
          this.userData.role === 'retailer'
        ) {
          this.inventoryService.getAllInventories();
          this.getInventoryDetails();
        }

        if (
          this.userData.role === 'admin' ||
          this.userData.role === 'distributor'
        ) {
          this.getOrderDetails();
        }

        if (
          this.userData.role === 'admin' ||
          this.userData.role === 'retailer'
        ) {
          this.getOrdersByCustID(this.userData.userID);
        }
        if (
          this.userData.role === 'admin' ||
          this.userData.role === 'regulatoryAuthority'
        ) {
          this.productService.getAllProducts();
          this.getProducts();
        }
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getInventoryDetails(): void {
    this.inventoryService.inventories$.subscribe({
      next: (res) => {
        this.inventoryData = res;
        // console.log(this.inventoryData);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getOrderDetails(): void {
    this.orderService.getAllOrders().subscribe({
      next: (res) => {
        // console.log(res);
        this.orderData = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getOrdersByCustID(customerId: string): void {
    this.orderService.getOrdersByCustomerID(customerId).subscribe({
      next: (res) => {
        // console.log(res);
        this.customerOrderData = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getProducts(): void {
    this.productService.products$.subscribe({
      next: (res) => {
        console.log(res);
        
        this.productData = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
