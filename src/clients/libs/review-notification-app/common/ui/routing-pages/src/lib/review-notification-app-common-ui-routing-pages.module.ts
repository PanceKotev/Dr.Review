import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ErrorPageComponent } from './components/error-page/error-page.component';

@NgModule({
  imports: [CommonModule],
  declarations: [
    NotFoundComponent,
    ErrorPageComponent],
  exports: [
    NotFoundComponent,
    ErrorPageComponent
  ]
})
export class ReviewNotificationAppCommonUiRoutingPagesModule {}
