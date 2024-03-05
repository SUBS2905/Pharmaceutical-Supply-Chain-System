import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InventoryRoutingModule } from './inventory-routing.module';
import { InventoryComponent } from './inventory.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddInventoryComponent } from './pages/add-inventory/add-inventory.component';
import { SharedModule } from 'src/shared/shared.module';
import { UpdateQuantityComponent } from './pages/update-quantity/update-quantity.component';


@NgModule({
  declarations: [
    InventoryComponent,
    AddInventoryComponent,
    UpdateQuantityComponent
  ],
  imports: [
    CommonModule,
    InventoryRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class InventoryModule { }
