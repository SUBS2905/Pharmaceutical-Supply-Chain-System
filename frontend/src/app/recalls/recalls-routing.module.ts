import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecallsComponent } from './recalls.component';
import { AuthGuard } from 'src/shared/guards/auth.guard';
import { ARGuard } from 'src/shared/guards/AR.guard';
import { RecallProductComponent } from './pages/recall-product/recall-product.component';

const routes: Routes = [
  { path: '', component: RecallsComponent, canActivate: [AuthGuard, ARGuard] },
  {
    path: 'recall/:productId',
    component: RecallProductComponent,
    canActivate: [AuthGuard, ARGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RecallsRoutingModule {}
