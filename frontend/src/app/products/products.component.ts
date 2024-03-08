import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Product from 'src/shared/models/Product';
import { AuthService } from 'src/shared/services/auth.service';
import { ProductService } from 'src/shared/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  userRole: string;
  products: Product[];

  constructor(
    private productService: ProductService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productService.getAllProducts();
    this.subscribeToProducts();
    this.getUserRole();
  }

  subscribeToProducts(): void {
    this.productService.products$.subscribe({
      next: (res) => {
        // console.log(res);
        this.products = res;
      },
      error: (err) => console.log(err),
    });
  }

  getUserRole(): void {
    this.authService.getUserById().subscribe({
      next: (user) => (this.userRole = user.role),
      error: (err) => console.log(err),
    });
  }

  onUpdate(productId: string): void {
    const url = `products/update/${productId}`;
    this.router.navigateByUrl(url);
  }

  onDelete(productID: string): void {
    this.productService.deleteProduct(productID);
  }

  onRecall(productId: string): void {
    const url = `recalls/recall/${productId}`;
    this.router.navigateByUrl(url);
  }

  extractDate(date: Date): string {
    const dateStr = date.toString();
    return dateStr.split('T')[0];
  }
}
