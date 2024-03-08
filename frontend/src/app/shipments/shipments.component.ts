import { Component, OnInit } from '@angular/core';
import Shipment from 'src/shared/models/Shipment';
import { AuthService } from 'src/shared/services/auth.service';
import { ShipmentService } from 'src/shared/services/shipment.service';

@Component({
  selector: 'app-shipments',
  templateUrl: './shipments.component.html',
  styleUrls: ['./shipments.component.scss'],
})
export class ShipmentsComponent implements OnInit {
  userRole: string;
  shipments: Shipment[];

  constructor(
    private shipmentService: ShipmentService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getUserRole();
  }

  getUserRole(): void {
    this.authService.getUserById().subscribe({
      next: (user) => {
        this.userRole = user.role;
        if (this.userRole === 'admin' || this.userRole === 'distributor') {
          this.getAllShipments();
        }
      },
      error: (err) => console.log(err),
    });
  }

  getAllShipments(): void {
    this.shipmentService.getAllShipments().subscribe({
      next: (res) => {
        this.shipments = res;
        // console.log(this.shipments);
      },
      error: (err) => console.log(err),
    });
  }

  extractDate(date: Date): string {
    const dateStr = date.toString();
    return dateStr.split('T')[0];
  }
}
