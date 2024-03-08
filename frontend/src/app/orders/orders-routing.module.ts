import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { AuthGuard } from 'src/shared/guards/auth.guard';
import { CreateOrderComponent } from './pages/create-order/create-order.component';
import { ARetGuard } from 'src/shared/guards/ARet.guard';
import { UpdateStatusComponent } from './pages/update-status/update-status.component';
import { ADGuard } from 'src/shared/guards/AD.guard';
import { ADRetGuard } from 'src/shared/guards/ADRet.guard';

const routes: Routes = [
  { path: '', component: OrdersComponent, canActivate: [AuthGuard, ADRetGuard] },
  {
    path: 'create/:productID',
    component: CreateOrderComponent,
    canActivate: [AuthGuard, ARetGuard],
  },
  {
    path: 'update/:orderId',
    component: UpdateStatusComponent,
    canActivate: [AuthGuard, ADGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrdersRoutingModule {}
