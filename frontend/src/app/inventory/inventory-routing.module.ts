import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventoryComponent } from './inventory.component';
import { AddInventoryComponent } from './pages/add-inventory/add-inventory.component';
import { UpdateQuantityComponent } from './pages/update-quantity/update-quantity.component';
import { AuthGuard } from 'src/shared/guards/auth.guard';
import { AMDRetGuard } from 'src/shared/guards/AMDRet.guard';
import { AMDGuard } from 'src/shared/guards/AMD.guard';




const routes: Routes = [
  {
    path: '',
    component: InventoryComponent,
    canActivate: [AuthGuard, AMDRetGuard],
  },
  {
    path: 'add',
    component: AddInventoryComponent,
    canActivate: [AuthGuard, AMDGuard],
  },
  {
    path: 'update/:batchNo',
    component: UpdateQuantityComponent,
    canActivate: [AuthGuard, AMDGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InventoryRoutingModule {}
