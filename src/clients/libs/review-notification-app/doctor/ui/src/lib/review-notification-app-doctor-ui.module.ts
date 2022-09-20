import { ReviewNotificationAppScheduleSubscriptionUiModule } from '@drreview/review-notification-app/schedule-subscription/ui';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorCardComponent } from './components/doctor-card/doctor-card.component';
import { MatCardModule } from '@angular/material/card';
import { AvatarModule } from 'ngx-avatars';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    ReviewNotificationAppScheduleSubscriptionUiModule,
    AvatarModule,
    MatCardModule],
  declarations: [DoctorCardComponent],
  exports: [DoctorCardComponent]
})
export class ReviewNotificationAppDoctorUiModule {}
