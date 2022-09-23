import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  ScheduleNotificationRange,
  ScheduleNotificationRangeString
} from '@drreview/shared/data-access';
import * as dayjs from 'dayjs';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'drreview-schedule-subscription-card',
  templateUrl: './schedule-subscription-card.component.html',
  styleUrls: ['./schedule-subscription-card.component.scss']
})
export class ScheduleSubscriptionCardComponent {
  @Input()
  public set rangeSelection(
    range: ScheduleNotificationRangeString | undefined
  ) {
    this.rangeSelectionValue$.next(this.convertStringToDate(range ? { ...range } : {
      from: null,
      to: null,
      subscribedTo: false
    }));
  }

  public rangeSelectionValue$ = new BehaviorSubject<ScheduleNotificationRange | undefined>(undefined);

  @Input()
  public isTicked = false;

  @Input()
  public cardTitle = 'Доктор Докторски';

  @Input()
  public cardSubtitle = 'Институција, Специјализација';

  @Output()
  public rangeSelectionChange = new EventEmitter<
    ScheduleNotificationRange | undefined
  >();

  public selectionChanged(value: ScheduleNotificationRange | undefined): void {
    if (
      value?.from &&
      value.to &&
      this.rangeSelection?.subscribedTo !== value.subscribedTo
    ) {
      this.rangeSelectionChange.emit(value);
    }
  }

  private convertStringToDate(
    range: ScheduleNotificationRangeString | null | undefined
  ): ScheduleNotificationRange | undefined {
    if (!range) {
      return undefined;
    }

    return {
      from: dayjs(range.from).toDate(),
      to: dayjs(range.to).toDate(),
      subscribedTo: range.subscribedTo
    };
  }
}
