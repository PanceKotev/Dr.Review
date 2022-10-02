import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  forwardRef,
  OnInit,
  OnDestroy,
  Input
} from '@angular/core';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';
import { FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, Subject, takeUntil } from 'rxjs';
import { ThemesService } from '@drreview/shared/services/themes';
import { ScheduleNotificationRange } from '@drreview/shared/data-access';

@Component({
  selector: 'drreview-schedule-subscription-range-input',
  templateUrl: './schedule-subscription-range-input.component.html',
  styleUrls: ['./schedule-subscription-range-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ScheduleSubscriptionRangeInputComponent),
      multi: true
    }
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScheduleSubscriptionRangeInputComponent
  extends BaseControlValueAccessor<ScheduleNotificationRange | null | undefined>
  implements OnInit, OnDestroy {
  private isDestroying$ = new Subject<boolean>();

  public fg: FormGroup = new FormGroup({
    from: new FormControl<Date | undefined | null>(null),
    to: new FormControl<Date | undefined | null>(null),
    subscribedTo: new FormControl<boolean | undefined>(false)
  });

  @Input()
  public notificationSwitchUnder = true;

  @Input()
  public appearance: 'fill' | 'standard' | 'outline' | 'legacy' = 'fill';

  public override writeValue(
    obj: ScheduleNotificationRange | undefined | null
  ): void {
      // this.value = obj;
      this.fg.patchValue({
        from: obj?.from,
        to: obj?.to,
        subscribedTo: !!obj?.subscribedTo
      }, { emitEvent: false, onlySelf: true });
      this.fg.updateValueAndValidity({ emitEvent: false, onlySelf: true });
      this.cdr?.markForCheck();

  }

  public constructor(
    public override cdr: ChangeDetectorRef,
    public themeService: ThemesService
  ) {
    super(cdr);
  }

  public ngOnInit(): void {
    this.fg.valueChanges.pipe(debounceTime(250),takeUntil(this.isDestroying$)).subscribe((c) => {
      console.log('ooh value change', c);
      this.onChange(c);
    });
  }

  public ngOnDestroy(): void {
    this.isDestroying$.next(true);
    this.isDestroying$.complete();
  }
}
