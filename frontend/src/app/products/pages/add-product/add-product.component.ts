import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from 'src/shared/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss'],
})
export class AddProductComponent implements OnInit {
  addProductForm: FormGroup;
  addProductFailed = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.addProductForm = this.fb.group({
      productID: ['', [Validators.required]],
      productName: ['', [Validators.required]],
      description: ['', [Validators.required]],
      formulation: ['', Validators.required],
      ingredients: [''],
      expirationDate: ['', [Validators.required]],
      compliance: this.fb.group({
        fda: [false],
        ema: [false],
      }),
    });
  }

  onSubmit(): void {
    if (this.addProductForm.valid) {
      const formValue = this.addProductForm.value;
      // Split the ingredients string into an array
      formValue.ingredients = formValue.ingredients
        .split(',')
        .map((ingredient) => ingredient.trim());
      // console.log(formValue);

      this.productService.addProduct(formValue);
    } else {
      // console.log(this.addProductForm.value);
      console.log('Form is invalid');
    }
  }

  onFailed(): void {
    this.productService.error$.subscribe({
      next: (err) => {
        this.addProductFailed = err;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
