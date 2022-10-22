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

  @Output()
  public deleteClicked = new EventEmitter<void>();


  @Input()
  public range: DateRange<Date> = new DateRange<Date>(
    drReviewDate().toDate(),
    drReviewDate().add(7, 'days')
    .toDate()
  );

  public handlePanelDelete(): void {
    this.deleteClicked.emit();
  }
}
