import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  ScheduleNotificationRange,
  ScheduleNotificationRangeString
} from '@drreview/shared/data-access';
import * as dayjs from 'dayjs';

@Component({
  selector: 'drreview-schedule-subscription-card',
  templateUrl: './schedule-subscription-card.component.html',
  styleUrls: ['./schedule-subscription-card.component.scss']
})
export class ScheduleSubscriptionCardComponent {
  @Input()
  public set rangeSelection(
    range: ScheduleNotificationRangeString | undefined | null
  ) {
    console.log('I GOT THIS', range);
    this.rangeSelectionValue= this.convertStringToDate(range);
  }

  public rangeSelectionValue: ScheduleNotificationRange | undefined | null = undefined;

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
    console.log('from range', value);
    if (
      value?.from &&
      value.to &&
      value.subscribedTo !== null
    ) {
      console.log('card changed');
      this.rangeSelectionChange.emit(value);
    }
  }

  private convertStringToDate(
    range: ScheduleNotificationRangeString | null | undefined
  ): ScheduleNotificationRange | undefined {
    if (!range) {
      return undefined;
    }

    console.log('gotten range from backend', range);

    const convertedRange = {
      from: range.from ? dayjs(range.from).toDate() : null,
      to: range.to? dayjs(range.to).toDate() : null,
      subscribedTo: range.subscribedTo
    };
    console.log(convertedRange);

    return convertedRange;
  }
}
