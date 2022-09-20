import { Component } from '@angular/core';
import { filterChips } from '@drreview/review-notification-app/schedule-subscription/data-access';

@Component({
  selector: 'drreview-schedule-subscriptions-root',
  templateUrl: './schedule-subscriptions-root.component.html',
  styleUrls: ['./schedule-subscriptions-root.component.scss']
})
export class ScheduleSubscriptionsRootComponent{
  public filterOptions = filterChips;
}
