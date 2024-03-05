import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrdersRoutingModule } from './orders-routing.module';
import { OrdersComponent } from './orders.component';
import { SharedModule } from 'src/shared/shared.module';
import { CreateOrderComponent } from './pages/create-order/create-order.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UpdateStatusComponent } from './pages/update-status/update-status.component';


@NgModule({
  declarations: [
    OrdersComponent,
    CreateOrderComponent,
    UpdateStatusComponent
  ],
  imports: [
    CommonModule,
    OrdersRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class OrdersModule { }
