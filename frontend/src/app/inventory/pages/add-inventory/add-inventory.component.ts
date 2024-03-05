import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InventoryService } from 'src/shared/services/inventory.service';

@Component({
  selector: 'app-add-inventory',
  templateUrl: './add-inventory.component.html',
  styleUrls: ['./add-inventory.component.scss'],
})
export class AddInventoryComponent implements OnInit {
  addInvenotryForm: FormGroup;
  addInventoryFailed = false;

  constructor(
    private fb: FormBuilder,
    private inventoryService: InventoryService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.addInvenotryForm = this.fb.group({
      productID: ['', [Validators.required]],
      location: ['', [Validators.required]],
      quantity: [Validators.required],
      batchNumber: ['', [Validators.required]],
      serialNumber: ['', [Validators.required]],
    });
  }

  onSubmit(): void{
    if(this.addInvenotryForm.valid){
      const formValue = this.addInvenotryForm.value;
      console.log(formValue);
      this.inventoryService.addInventory(formValue);
    }else{
      console.log('Form is invalid');
      
    }
  }

  onFailed(): void {
    this.inventoryService.error$.subscribe({
      next: (err) => {
        this.addInventoryFailed = err;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
