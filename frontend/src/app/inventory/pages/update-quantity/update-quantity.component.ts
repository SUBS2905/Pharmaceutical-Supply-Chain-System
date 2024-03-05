import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InventoryService } from 'src/shared/services/inventory.service';

@Component({
  selector: 'app-update-quantity',
  templateUrl: './update-quantity.component.html',
  styleUrls: ['./update-quantity.component.scss'],
})
export class UpdateQuantityComponent implements OnInit {
  updateQuantityForm: FormGroup;
  updateQuantityFailed = false;

  constructor(
    private fb: FormBuilder,
    private inventoryService: InventoryService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.updateQuantityForm = this.fb.group({
      batchNumber: ['', Validators.required],
      quantityAdded: [],
      quantitySubtracted: [],
    });
  }

  onSubmit(): void {
    if (this.updateQuantityForm.valid) {
      const formValue = this.updateQuantityForm.value;
      if (formValue.quantityAdded === null) formValue.quantityAdded = 0;
      if (formValue.quantitySubtracted === null)
        formValue.quantitySubtracted = 0;

      console.log(formValue);

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
