import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorComponent } from './components/error/error.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [ErrorComponent, NavbarComponent],
  imports: [CommonModule, RouterModule],
  exports: [NavbarComponent],
})
export class SharedModule {}
