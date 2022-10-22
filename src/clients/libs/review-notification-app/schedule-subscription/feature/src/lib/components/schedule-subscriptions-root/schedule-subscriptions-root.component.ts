import { Component } from '@angular/core';

@Component({
  selector: 'drreview-schedule-subscriptions-root',
  templateUrl: './schedule-subscriptions-root.component.html',
  styleUrls: ['./schedule-subscriptions-root.component.scss']
})
export class ScheduleSubscriptionsRootComponent{

  public allExpanded = false;


  public toggleAllExpanded(): void {
    this.allExpanded = !this.allExpanded;
  }
}
