import { ChangeDetectionStrategy, ChangeDetectorRef, Component, forwardRef, OnInit, OnDestroy, Input } from '@angular/core';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';
import { ScheduleNotificationRange } from '@drreview/review-notification-app/schedule-subscription/data-access';
import { FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { ThemesService } from '@drreview/shared/services/themes';

@Component({
  selector: 'drreview-schedule-subscription-range-input',
  templateUrl: './schedule-subscription-range-input.component.html',
  styleUrls: ['./schedule-subscription-range-input.component.scss'],
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => ScheduleSubscriptionRangeInputComponent), multi: true }],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScheduleSubscriptionRangeInputComponent extends BaseControlValueAccessor<ScheduleNotificationRange>
      implements OnInit, OnDestroy {

  private isDestroying$ = new Subject<boolean>();

  public fg: FormGroup = new FormGroup({
    from: new FormControl<Date | null>(null),
    to: new FormControl<Date | null>(null),
    subscribedTo: new FormControl<boolean>(false)
  });

  @Input()
  public notificationSwitchUnder = true;

  @Input()
  public appearance: 'fill' | 'standard' | 'outline' | 'legacy' = 'fill';

  public constructor(public override cdr: ChangeDetectorRef, public themeService: ThemesService) {
    super(cdr);
  }

  public ngOnInit(): void {
    this.fg.valueChanges.pipe(takeUntil(this.isDestroying$)).subscribe(c => {
      this.onChange(c);
    });
  }

  public ngOnDestroy(): void {
    this.isDestroying$.next(true);
    this.isDestroying$.complete();
  }
}
