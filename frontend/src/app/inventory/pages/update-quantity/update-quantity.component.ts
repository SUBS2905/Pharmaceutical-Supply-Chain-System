import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { InventoryService } from 'src/shared/services/inventory.service';

@Component({
  selector: 'app-update-quantity',
  templateUrl: './update-quantity.component.html',
  styleUrls: ['./update-quantity.component.scss'],
})
export class UpdateQuantityComponent implements OnInit {
  updateQuantityForm: FormGroup;
  updateQuantityFailed = false;
  batchNumber: string;

  constructor(
    private fb: FormBuilder,
    private inventoryService: InventoryService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.route.params.subscribe({
      next: (params) => {
        this.batchNumber = params['batchNo'];
      },
    });
  }

  initializeForm(): void {
    this.updateQuantityForm = this.fb.group({
      // batchNumber: ['', Validators.required],
      quantityAdded: [],
      quantitySubtracted: [],
    });
  }

  onSubmit(): void {
    if (this.updateQuantityForm.valid) {
      const formValue = this.updateQuantityForm.value;

      formValue.batchNumber = this.batchNumber;
      if (formValue.quantityAdded === null) formValue.quantityAdded = 0;
      if (formValue.quantitySubtracted === null)
        formValue.quantitySubtracted = 0;

      // console.log(formValue);

      this.inventoryService.updateQuantity(
        formValue.batchNumber,
        formValue.quantityAdded,
        formValue.quantitySubtracted
      );
    } else {
      console.log('Form is invalid');
    }
  }

  onFailed(): void {
    this.inventoryService.error$.subscribe({
      next: (err) => {
        this.updateQuantityFailed = err;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
