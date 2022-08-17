import { ChangeDetectionStrategy, Component, forwardRef, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';

@Component({
  selector: 'drreview-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => RatingComponent), multi: true }]
})
export class RatingComponent extends BaseControlValueAccessor<number> {

  @Input()
  public override value = 0.0;

  @Input()
  public readOnly = false;

  @Input()
  public useAppColors = false;
}
