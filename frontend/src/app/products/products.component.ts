import { Component, OnInit } from '@angular/core';
import Product from 'src/shared/models/Product';
import { ProductService } from 'src/shared/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  products: Product[];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getAllProducts();
    this.subscribeToProducts();
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

  onDelete(productID: string): void {
    this.productService.deleteProduct(productID);
  }

  extractDate(date: Date): string {
    const dateStr = date.toString();
    return dateStr.split('T')[0];
  }
}
