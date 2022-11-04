import { ScheduleNotificationRange } from '@drreview/shared/data-access';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { DateRange } from '@angular/material/datepicker';
import { drReviewDate } from '@drreview/shared/utils/date';

@Component({
  selector: 'drreview-schedule-subscription-item',
  templateUrl: './schedule-subscription-item.component.html',
  styleUrls: ['./schedule-subscription-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScheduleSubscriptionItemComponent {
  @Input()
  public expanded = false;

  @Input()
  public institution = 'Институција';

  @Input()
  public specialization = 'Специјализација';

  @Input()
  public location = 'Локација';

  @Input()
  public firstName = 'Доктор';

  @Input()
  public lastName = 'Докторски';

  @Input()
  public checked = false;

  @Input()
  public doctorSuid = '';

  public get status(): 'expired' | 'normal' | 'close-to-expiry' {
    if(!this._range){
      return 'normal';
    }

    const past = drReviewDate().isAfter(this._range.end, 'days');

    const daysToExpiry = drReviewDate().diff(this._range.end, 'days');

    return past ? 'expired' : daysToExpiry <= 5 && daysToExpiry >= 0? 'close-to-expiry' : 'normal';
  }

  @Output()
  public deleteClicked = new EventEmitter<void>();

  @Output()
  public checkedChanged = new EventEmitter<boolean>();

  @Input()
  public set range(range: ScheduleNotificationRange) {
    this._range = new DateRange<Date>(range.from, range.to);
  };

  public _range: DateRange<Date> | undefined;

  @Input()
  public suid = '';

  public handlePanelDelete(): void {
    this.deleteClicked.emit();
  }

  public handleCheckedChange(event: boolean): void {
    this.checkedChanged.emit(event);
  }
}
