import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import Inventory from '../models/Inventory';
import { BehaviorSubject } from 'rxjs';
import AddInventoryRequest from '../models/AddInventoryRequest';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  private jwt = this.cookieService.get('jwt');
  private inventoryEndpoint = 'https://localhost:7024/api/Inventory';
  private headers = new HttpHeaders().set(
    'Authorization',
    `Bearer ${this.jwt}`
  );

  error$ = new BehaviorSubject<boolean>(false);
  inventory$ = new BehaviorSubject<Inventory>(null);
  inventories$ = new BehaviorSubject<Inventory[]>(null);

  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router
  ) {}

  getAllInventories(): void {
    this.http
      .get<Inventory[]>(this.inventoryEndpoint, { headers: this.headers })
      .subscribe({
        next: (res) => {
          this.inventories$.next(res);
        },
        error: (err) => {
          this.error$.next(true);
          console.error(err);
        },
      });
  }

  getInventoryById(batchNumber: string): void {
    const body = {
      batchNo: batchNumber,
    };
    this.http
      .post<Inventory>(`${this.inventoryEndpoint}/batchNo`, body, {
        headers: this.headers,
      })
      .subscribe({
        next: (res) => {
          console.log(res);
          this.inventory$.next(res);
        },
        error: (err) => {
          this.error$.next(true);
          console.error(err);
        },
      });
  }

  addInventory(inventory: AddInventoryRequest): void {
    this.http
      .post<Inventory>(this.inventoryEndpoint, inventory, {
        headers: this.headers,
      })
      .subscribe({
        next: () => {
          alert('Inventory added successfully!');
          this.router.navigateByUrl('/inventory');
        },
        error: (err) => {
          this.error$.next(true);
          alert('Unable to add inventory');
          console.error(err);
        },
      });
  }

  // delete method can't have a body
  deleteInventory(batchNo: string): void {
    const deleteURI = `${this.inventoryEndpoint}?batchNo=${batchNo}`;
    this.http.delete(deleteURI, { headers: this.headers }).subscribe({
      next: (res) => {
        console.log(res);
        const currentInventory = this.inventories$.getValue();
        const updatedInventory = currentInventory.filter(
          (inv) => inv.batchNumber != batchNo
        );
        alert('Inventory Deleted successfully!');
        this.inventories$.next(updatedInventory);
      },
      error: (err) => {
        if (err.status === 400) alert('Inventory does not exist');
        this.error$.next(true);
        console.error(err);
      },
    });
  }

  updateQuantity(
    batchNo: string,
    qtyAdded: number,
    qtySubtracted: number
  ): void {
    const body = {
      batchNumber: batchNo,
      quantityAdded: qtyAdded,
      quantitySubtracted: qtySubtracted,
    };
    this.http
      .put(`${this.inventoryEndpoint}/quantity`, body, {
        headers: this.headers,
      })
      .subscribe({
        next: (res) => {
          console.log(res);
          alert('Quantity updated successfully!');
          this.router.navigateByUrl('/inventory');
        },
        error: (err) => {
          if (err.status === 400) alert('Inventory does not exist');
          this.error$.next(true);
          console.error(err);
        },
      });
  }

  orderProduct(productID: string): void{
    const createOrderURL = `/orders/create/${productID}`;
    this.router.navigateByUrl(createOrderURL);
  }
}
