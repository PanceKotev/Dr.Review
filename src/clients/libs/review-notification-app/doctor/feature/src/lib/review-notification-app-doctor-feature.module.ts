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
import { ReviewNotificationAppDoctorUiModule } from '@drreview/review-notification-app/doctor/ui';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { DoctorsCustomLabels } from './intl/doctors-custom-page';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
@NgModule({
  imports: [
    CommonModule,
    MatCardModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    RouterModule.forChild(routes),
    ReviewNotificationAppScheduleSubscriptionUiModule,
    SharedUiFiltersModule,
    FormsModule,
    MatCardModule,
    MatSlideToggleModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    SharedUiReviewModule,
    ReviewNotificationAppDoctorUiModule
  ],
  providers: [
    { provide: MatPaginatorIntl, useValue: DoctorsCustomLabels}
  ],
  declarations: [DoctorDetailsComponent, DoctorsRootComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppDoctorFeatureModule {}
