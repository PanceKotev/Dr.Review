import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { SharedDataAccessModule } from '@drreview/shared/data-access';

@NgModule({
  imports: [
    CommonModule,
    SharedDataAccessModule],
  declarations: [MainLayoutComponent],
  exports: [
    SharedDataAccessModule,
    MainLayoutComponent
    ]
})
export class ReviewNotificationAppShellModule {}
