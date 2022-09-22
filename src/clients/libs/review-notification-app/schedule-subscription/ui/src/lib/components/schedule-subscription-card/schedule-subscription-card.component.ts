import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ScheduleNotificationRange } from '@drreview/shared/data-access';

@Component({
  selector: 'drreview-schedule-subscription-card',
  templateUrl: './schedule-subscription-card.component.html',
  styleUrls: ['./schedule-subscription-card.component.scss']
})
export class ScheduleSubscriptionCardComponent implements OnInit {

  @Input()
  public rangeSelection: ScheduleNotificationRange | null | undefined;

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
    console.log(this.rangeSelection?.subscribedTo, value?.subscribedTo);
    if(value?.from && value.to  && this.rangeSelection?.subscribedTo !== value.subscribedTo){
      this.rangeSelectionChange.emit(value);
    }
  }
}
