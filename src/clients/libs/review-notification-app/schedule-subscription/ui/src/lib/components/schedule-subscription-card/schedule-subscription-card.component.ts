import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ScheduleNotificationRange } from '@drreview/review-notification-app/schedule-subscription/data-access';

@Component({
  selector: 'drreview-schedule-subscription-card',
  templateUrl: './schedule-subscription-card.component.html',
  styleUrls: ['./schedule-subscription-card.component.scss']
})
export class ScheduleSubscriptionCardComponent implements OnInit {

  public rangeSelection: ScheduleNotificationRange | undefined;

  @Input()
  public isTicked = false;

  @Input()
  public cardTitle = 'Доктор Докторски';

  @Input()
  public cardSubtitle = 'Институција, Специјализација';

  @Output()
  public rangeSelectionChange = new EventEmitter<ScheduleNotificationRange | undefined>();

  public ngOnInit(): void {
    console.log(this.rangeSelection);
  }

  public selectionChanged(value: ScheduleNotificationRange | undefined): void{
    this.rangeSelectionChange.emit(value);
  }
}
