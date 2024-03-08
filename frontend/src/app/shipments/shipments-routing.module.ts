import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShipmentsComponent } from './shipments.component';
import { AuthGuard } from 'src/shared/guards/auth.guard';
import { ADGuard } from 'src/shared/guards/AD.guard';
import { AddShipmentComponent } from './pages/add-shipment/add-shipment.component';

const routes: Routes = [
  {
    path: '',
    component: ShipmentsComponent,
    canActivate: [AuthGuard, ADGuard],
  },
  {
    path: 'add',
    component: AddShipmentComponent,
    canActivate: [AuthGuard, ADGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShipmentsRoutingModule {}
