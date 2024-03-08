import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products.component';
import { AddProductComponent } from './pages/add-product/add-product.component';
import { UpdateProductComponent } from './pages/update-product/update-product.component';
import { AuthGuard } from 'src/shared/guards/auth.guard';
import { AMRGuard } from 'src/shared/guards/AMR.guard';

const routes: Routes = [
  {
    path: '',
    component: ProductsComponent,
    canActivate: [AuthGuard, AMRGuard],
  },
  {
    path: 'add',
    component: AddProductComponent,
    canActivate: [AuthGuard, AMRGuard],
  },
  {
    path: 'update/:productId',
    component: UpdateProductComponent,
    canActivate: [AuthGuard, AMRGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductsRoutingModule {}
