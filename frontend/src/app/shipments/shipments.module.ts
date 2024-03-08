import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShipmentsRoutingModule } from './shipments-routing.module';
import { ShipmentsComponent } from './shipments.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/shared/shared.module';
import { AddShipmentComponent } from './pages/add-shipment/add-shipment.component';


@NgModule({
  declarations: [
    ShipmentsComponent,
    AddShipmentComponent
  ],
  imports: [
    CommonModule,
    ShipmentsRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class ShipmentsModule { }
