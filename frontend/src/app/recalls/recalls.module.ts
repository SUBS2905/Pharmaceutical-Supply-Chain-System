import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecallsRoutingModule } from './recalls-routing.module';
import { RecallsComponent } from './recalls.component';
import { RecallProductComponent } from './pages/recall-product/recall-product.component';
import { SharedModule } from 'src/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    RecallsComponent,
    RecallProductComponent
  ],
  imports: [
    CommonModule,
    RecallsRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class RecallsModule { }
