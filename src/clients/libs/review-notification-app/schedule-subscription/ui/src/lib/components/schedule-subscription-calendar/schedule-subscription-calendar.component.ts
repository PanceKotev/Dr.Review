import { ChangeDetectionStrategy, Component, HostBinding, Input, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { DateRange } from '@angular/material/datepicker';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';

@Component({
  selector: 'drreview-schedule-subscription-calendar',
  templateUrl: './schedule-subscription-calendar.component.html',
  styleUrls: ['./schedule-subscription-calendar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => ScheduleSubscriptionCalendarComponent), multi: true}]
})
export class ScheduleSubscriptionCalendarComponent extends BaseControlValueAccessor<DateRange<Date>>{

  @Input()
  @HostBinding("style.--selector-color")
  public selectorColor = 'var(--color-primary)';


  @Input()
  public comparsionRange: DateRange<Date> | undefined;

  public onSelectedChange(date: Date): void {
    if (
      this.value &&
      this.value.start &&
      date > this.value.start &&
      !this.value.end
    ) {
      this.value = new DateRange(
        this.value.start,
        date
      );
      this.onChange(this.value);
    } else {
      this.value = new DateRange(date, null);
      this.onChange(this.value);
    }
  }
}
