import { MatCardModule } from '@angular/material/card';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleSubscriptionRangeInputComponent }
   from './components/schedule-subscription-range-input/schedule-subscription-range-input.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatIconModule } from '@angular/material/icon';
import { ScheduleSubscriptionCardComponent } from './components/schedule-subscription-card/schedule-subscription-card.component';
import { MatCheckboxModule } from '@angular/material/checkbox';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatIconModule,
    MatDatepickerModule,
    MatSlideToggleModule
  ],
  declarations: [
    ScheduleSubscriptionRangeInputComponent,
    ScheduleSubscriptionCardComponent
  ],
  exports: [ScheduleSubscriptionRangeInputComponent, ScheduleSubscriptionCardComponent]
})
export class ReviewNotificationAppScheduleSubscriptionUiModule {}
