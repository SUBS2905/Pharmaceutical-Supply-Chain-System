import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import Product from '../models/Product';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private productEndpoint = 'https://localhost:7024/api/Product';
  private jwt = this.cookieService.get('jwt');
  private headers = new HttpHeaders().set(
    'Authorization',
    `Bearer ${this.jwt}`
  );
  products$ = new BehaviorSubject<Product[]>(null);
  error$ = new BehaviorSubject<boolean>(false);

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router
  ) {}

  getAllProducts(): void {
    this.http
      .get<Product[]>(this.productEndpoint, { headers: this.headers })
      .subscribe({
        next: (res) => {
          // console.log(res);
          this.products$.next(res);
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  addProduct(product: Product): void {
    this.http
      .post<Product>(this.productEndpoint, product, { headers: this.headers })
      .subscribe({
        next: () => {
          alert('Product added successfully!');
          this.router.navigateByUrl('/products');
        },
        error: (err) => {
          this.error$.next(true);
          console.error(err);
        },
      });
  }

  // delete method can't have a body
  deleteProduct(productID: string): void {
    const deleteURI = `${this.productEndpoint}?productID=${productID}`;
    this.http.delete(deleteURI, { headers: this.headers }).subscribe({
      next: (res) => {
        console.log(res);
        const currentState = this.products$.getValue();
        const updatedState = currentState.filter(
          (p) => p.productID != productID
        );
        alert('Product Deleted successfully!');
        this.products$.next(updatedState);
      },
      error: (err) => {
        this.error$.next(true);
        console.error(err);
      },
    });
  }

  updateProduct(product: Product): void {
    this.http
      .put<Product>(this.productEndpoint, product, { headers: this.headers })
      .subscribe({
        next: (res) => {
          console.log(res);
          alert('Product updated successfully!');
          this.router.navigateByUrl('/products');
        },
        error: (err) => {
          console.error(err);
          alert('Product does not exist');
          this.error$.next(true);
        },
      });
  }
}
