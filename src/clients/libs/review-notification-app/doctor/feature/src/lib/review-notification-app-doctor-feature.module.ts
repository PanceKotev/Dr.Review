import { routes } from './routes';
import { MatCardModule } from '@angular/material/card';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorDetailsComponent } from './components/doctor-details/doctor-details.component';
import { RouterModule } from '@angular/router';
import { SharedUiReviewModule } from '@drreview/shared/ui/review';

@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    RouterModule.forChild(routes),
    SharedUiReviewModule],
  declarations: [DoctorDetailsComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppDoctorFeatureModule {}
