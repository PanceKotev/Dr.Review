import { ReviewNotificationAppScheduleSubscriptionUiModule } from '@drreview/review-notification-app/schedule-subscription/ui';
import { routes } from './routes';
import { MatCardModule } from '@angular/material/card';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorDetailsComponent } from './components/doctor-details/doctor-details.component';
import { RouterModule } from '@angular/router';
import { SharedUiReviewModule } from '@drreview/shared/ui/review';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    RouterModule.forChild(routes),
    ReviewNotificationAppScheduleSubscriptionUiModule,
    FormsModule,
    ReactiveFormsModule,
    SharedUiReviewModule],
  declarations: [DoctorDetailsComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppDoctorFeatureModule {}
