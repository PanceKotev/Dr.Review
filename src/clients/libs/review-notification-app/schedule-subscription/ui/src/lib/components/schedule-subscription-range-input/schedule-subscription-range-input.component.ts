import { ChangeDetectionStrategy, ChangeDetectorRef, Component,
  forwardRef, OnInit, OnDestroy, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';
import { FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { ThemesService } from '@drreview/shared/services/themes';
import { ScheduleNotificationRange } from '@drreview/shared/data-access';

@Component({
  selector: 'drreview-schedule-subscription-range-input',
  templateUrl: './schedule-subscription-range-input.component.html',
  styleUrls: ['./schedule-subscription-range-input.component.scss'],
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => ScheduleSubscriptionRangeInputComponent), multi: true }],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ScheduleSubscriptionRangeInputComponent extends BaseControlValueAccessor<ScheduleNotificationRange>
      implements OnInit, OnChanges, OnDestroy {

  private isDestroying$ = new Subject<boolean>();

  public fg: FormGroup = new FormGroup({
    from: new FormControl<Date | undefined | null>(null),
    to: new FormControl<Date | undefined | null>(null),
    subscribedTo: new FormControl<boolean>(false)
  });

  @Input()
  public notificationSwitchUnder = true;


  @Input()
  public appearance: 'fill' | 'standard' | 'outline' | 'legacy' = 'fill';

  public override writeValue(obj: ScheduleNotificationRange): void {
    this.value = obj;
    this.fg.patchValue(obj);
    this.cdr?.markForCheck();
  }

  public constructor(public override cdr: ChangeDetectorRef, public themeService: ThemesService) {
    super(cdr);
  }

  public ngOnInit(): void {
    if (this.value) {
      this.fg.patchValue(this.value);
      this.fg.updateValueAndValidity({emitEvent: false});
    };

    this.fg.valueChanges.pipe(takeUntil(this.isDestroying$)).subscribe(c => {
      this.onChange(c);
    });
  }

  public ngOnChanges(changes: SimpleChanges): void {
      if(changes['value']){
        console.log('cange');

        if (this.value) {

          this.fg.patchValue(this.value);
          this.fg.updateValueAndValidity({emitEvent: false});

        };
      }
  }
  public ngOnDestroy(): void {
    this.isDestroying$.next(true);
    this.isDestroying$.complete();
  }
}
