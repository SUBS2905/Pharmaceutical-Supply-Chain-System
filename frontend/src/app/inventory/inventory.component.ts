import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Inventory from 'src/shared/models/Inventory';
import { AuthService } from 'src/shared/services/auth.service';
import { InventoryService } from 'src/shared/services/inventory.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.scss'],
})
export class InventoryComponent implements OnInit {
  inventories: Inventory[];
  userRole: string;

  constructor(
    private inventoryService: InventoryService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.inventoryService.getAllInventories();
    this.subscribeToInventories();

    this.authService.getUserById().subscribe({
      next: (res) => {
        this.userRole = res.role;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getCardColour(level: string): string {
    switch (level) {
      case 'low':
        return '#FFF78A';
      case 'very low':
        return '#FFC47E';
      case 'critically low':
        return '#FF6868';
      default:
        return '#FFF';
    }
  }

  subscribeToInventories(): void {
    this.inventoryService.inventories$.subscribe({
      next: (res) => {
        this.inventories = res;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onDelete(batchNo: string): void {
    this.inventoryService.deleteInventory(batchNo);
  }

  extractDate(date: Date): string {
    const dateStr = date.toString();
    return dateStr.split('T')[0];
  }

  checkExpiry(date: Date): string {
    const today = new Date();

    const diffInTime = date.getTime() - today.getTime();
    const diffInDays = diffInTime / (1000 * 3600 * 24); // Convert milliseconds to days

    if (diffInDays <= 30 && diffInDays > 0) {
      return 'About to expire';
    } else if (diffInDays < 0) {
      return 'Expired';
    } else {
      return 'Not expired';
    }
  }

  getExpiryColour(expirationDate: Date): string {
    const date = new Date(expirationDate);
    const status = this.checkExpiry(date);
    switch (status) {
      case 'About to expire':
        return 'yellow';
      case 'Expired':
        return 'red';
      default:
        return 'none';
    }
  }

  orderProduct(productID: string): void {
    this.inventoryService.orderProduct(productID);
  }

  onUpdate(batchNumber: string): void{
    const url = `/inventory/update/${batchNumber}`
    this.router.navigateByUrl(url);
  }
}
