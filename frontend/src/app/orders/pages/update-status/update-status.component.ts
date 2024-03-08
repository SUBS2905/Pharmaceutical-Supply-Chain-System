import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/shared/services/order.service';

@Component({
  selector: 'app-update-status',
  templateUrl: './update-status.component.html',
  styleUrls: ['./update-status.component.scss'],
})
export class UpdateStatusComponent implements OnInit {
  updateStatusForm: FormGroup;
  orderId: string;

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForm();

    this.route.params.subscribe({
      next: (params) => {
        this.orderId = params['orderId'];
      },
    });
  }

  initializeForm(): void {
    this.updateStatusForm = this.fb.group({
      newStatus: ['placed', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.updateStatusForm.valid) {
      const formValue = this.updateStatusForm.value;
      formValue.orderId = this.orderId;
      // console.log(formValue);

      this.orderService.updateOrderStatus(
        formValue.orderId,
        formValue.newStatus
      );
    } else {
      console.log('Form is invalid');
    }
  }
}
