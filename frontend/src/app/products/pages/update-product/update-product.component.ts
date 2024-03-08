import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/shared/services/product.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss'],
})
export class UpdateProductComponent implements OnInit {
  updateProductForm: FormGroup;
  productId: string;
  // updateProductFailed = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForm();

    this.route.params.subscribe({
      next: (params) => (this.productId = params['productId']),
      error: (err) => console.log(err),
    });
  }

  initializeForm(): void {
    this.updateProductForm = this.fb.group({
      // productID: ['', [Validators.required]],
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
    if (this.updateProductForm.valid) {
      const formValue = this.updateProductForm.value;

      formValue.productID = this.productId;
      formValue.ingredients = formValue.ingredients
        .split(',')
        .map((ingredient) => ingredient.trim());

      // console.log(formValue);

      this.productService.updateProduct(formValue);
    } else {
      // console.log(this.addProductForm.value);
      console.log('Form is invalid');
    }
  }

  // onFailed(): void {
  //   this.productService.error$.subscribe({
  //     next: (err) => {
  //       this.updateProductFailed = err;
  //     },
  //     error: (err) => {
  //       console.log(err);
  //     },
  //   });
  // }
}
