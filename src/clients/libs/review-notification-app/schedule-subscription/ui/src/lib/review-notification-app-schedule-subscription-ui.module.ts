import { MatCardModule } from '@angular/material/card';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleSubscriptionRangeInputComponent } from
  './components/schedule-subscription-range-input/schedule-subscription-range-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatIconModule } from '@angular/material/icon';
import { ScheduleSubscriptionCardComponent } from './components/schedule-subscription-card/schedule-subscription-card.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ScheduleSubscriptionItemComponent } from './components/schedule-subscription-item/schedule-subscription-item.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { ScheduleSubscriptionCalendarComponent } from
  './components/schedule-subscription-calendar/schedule-subscription-calendar.component';
import { SharedUtilsDateModule } from '@drreview/shared/utils/date';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatDatepickerModule,
    MatSlideToggleModule,
    MatExpansionModule,
    SharedUtilsDateModule,
    RouterModule
  ],
  declarations: [
    ScheduleSubscriptionRangeInputComponent,
    ScheduleSubscriptionCardComponent,
    ScheduleSubscriptionItemComponent,
    ScheduleSubscriptionCalendarComponent
  ],
  exports: [
    ScheduleSubscriptionRangeInputComponent,
    ScheduleSubscriptionCardComponent,
    ScheduleSubscriptionItemComponent,
    ScheduleSubscriptionCalendarComponent
  ]
})
export class ReviewNotificationAppScheduleSubscriptionUiModule {}
