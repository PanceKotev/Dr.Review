import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  forwardRef,
  OnInit,
  OnDestroy,
  Input,
  Output,
  EventEmitter
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
  extends BaseControlValueAccessor<ScheduleNotificationRange | undefined>
  implements OnInit, OnDestroy {
  private isDestroying$ = new Subject<boolean>();

  public fg: FormGroup = new FormGroup({
    from: new FormControl<Date | null>(null),
    to: new FormControl<Date | null>(null),
    subscribedTo: new FormControl<boolean | null>(null)
  });

  @Input()
  public notificationSwitchUnder = true;

  @Input()
  public appearance: 'fill' | 'standard' | 'outline' | 'legacy' = 'fill';

  @Input()
  public hideNotificationSwitch = false;

  @Output()
  public selectionFinished = new EventEmitter<boolean>(true);

  @Input()
  public disabled = false;
  public override writeValue(
    obj: ScheduleNotificationRange | undefined
  ): void {
      this.value = obj;
        this.fg.setValue({
          from: obj?.from ?? null,
          to: obj?.to ?? null ,
          subscribedTo: obj?.subscribedTo ?? null
        }, { emitEvent: false, onlySelf: true });

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
        if(c.subscribedTo !== null || this.hideNotificationSwitch){
          this.onChange(c);
        }
    });
  }

  public checkBoxChange(val: boolean): void {
    this.value = {
      ...this.fg.value,
      subscribedTo: val
    };
    this.onChange(this.value);

  }
  public calendarStateChanged(state: boolean): void {
    this.selectionFinished.next(!state);
  }

  public ngOnDestroy(): void {
    this.isDestroying$.next(true);
    this.isDestroying$.complete();
  }
}
