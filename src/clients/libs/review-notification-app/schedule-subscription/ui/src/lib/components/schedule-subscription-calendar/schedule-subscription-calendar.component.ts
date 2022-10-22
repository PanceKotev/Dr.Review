import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { DateRange } from '@angular/material/datepicker';
import { drReviewDate } from '@drreview/shared/utils/date';

@Component({
  selector: 'drreview-schedule-subscription-calendar',
  templateUrl: './schedule-subscription-calendar.component.html',
  styleUrls: ['./schedule-subscription-calendar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScheduleSubscriptionCalendarComponent{
  @Input()
  public selectedRangeValue: DateRange<Date> =
    new DateRange<Date>(drReviewDate().toDate(),  drReviewDate().add(7, 'days')
    .toDate());
}
