import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RecallService } from 'src/shared/services/recall.service';

@Component({
  selector: 'app-recall-product',
  templateUrl: './recall-product.component.html',
  styleUrls: ['./recall-product.component.scss'],
})
export class RecallProductComponent implements OnInit {
  productId: string;
  recallForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private recallService: RecallService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (param) => {
        this.productId = param['productId'];
      },
      error: (err) => console.log(err),
    });

    this.initializeForm();
  }

  initializeForm(): void {
    this.recallForm = this.fb.group({
      reason: ['', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.recallForm.valid) {
      this.recallService
        .recallProduct(this.productId, this.recallForm.value.reason)
        .subscribe({
          next: () => {
            this.router.navigateByUrl('/recalls');
          },
          error: (err) => {
            console.log(err);
            alert('Unable to recall product');
          },
        });
    } else {
      console.log('Invalid Form');
    }
  }
}
