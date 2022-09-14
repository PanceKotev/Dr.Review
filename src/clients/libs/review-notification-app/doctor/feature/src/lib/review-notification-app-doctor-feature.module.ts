import { ReviewNotificationAppScheduleSubscriptionUiModule } from '@drreview/review-notification-app/schedule-subscription/ui';
import { routes } from './routes';
import { MatCardModule } from '@angular/material/card';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorDetailsComponent } from './components/doctor-details/doctor-details.component';
import { RouterModule } from '@angular/router';
import { SharedUiReviewModule } from '@drreview/shared/ui/review';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DoctorsRootComponent } from './components/doctors-root/doctors-root.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SharedUiFiltersModule } from '@drreview/shared/ui/filters';

@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    RouterModule.forChild(routes),
    ReviewNotificationAppScheduleSubscriptionUiModule,
    SharedUiFiltersModule,
    FormsModule,
    MatCardModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    SharedUiReviewModule
  ],
  declarations: [DoctorDetailsComponent, DoctorsRootComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppDoctorFeatureModule {}
