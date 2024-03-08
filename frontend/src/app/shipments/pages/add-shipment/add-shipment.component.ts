import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ShipmentService } from 'src/shared/services/shipment.service';

@Component({
  selector: 'app-add-shipment',
  templateUrl: './add-shipment.component.html',
  styleUrls: ['./add-shipment.component.scss'],
})
export class AddShipmentComponent implements OnInit {
  addShipmentForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private shipmentService: ShipmentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.addShipmentForm = this.fb.group({
      shipperID: ['', [Validators.required]],
      shipperName: ['', [Validators.required]],
      currentLocation: ['', [Validators.required]],
      estimatedDelivery: ['', [Validators.required]],
      orderIds: ['', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.addShipmentForm.valid) {
      const formValue = this.addShipmentForm.value;

      //split orderIds string into an array
      formValue.orderIds = formValue.orderIds
        .split(',')
        .map((orderId) => orderId.trim());

      formValue.trackingNumber = '';

      // console.log(formValue);

      this.shipmentService.addShipment(formValue).subscribe({
        next: () => {
          // console.log(res);
          this.router.navigateByUrl('/shipments');
        },
        error: (err) => {
          console.log(err);
          alert('Unable to add shipment');
        },
      });
    } else {
      console.log('Form is invalid!');
    }
  }
}
