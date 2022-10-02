import { IOptionItem, IOptionItemWithLink } from '@drreview/shared/data-access';
import { ChangeDetectionStrategy, Component, forwardRef, Input } from '@angular/core';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'drreview-chip-filter',
  templateUrl: './chip-filter.component.html',
  styleUrls: ['./chip-filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => ChipFilterComponent), multi: true}]
})
export class ChipFilterComponent<TValue = string>  extends BaseControlValueAccessor<TValue>{
  @Input()
  public filterOptions: (IOptionItem<TValue> & IOptionItemWithLink<TValue>)[] = [];

  @Input()
  public onlySubscriptions = false;
}
