import { Component, Input } from '@angular/core';
import { BaseControlValueAccessor } from '@drreview/shared/utils/form';

@Component({
  selector: 'drreview-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent extends BaseControlValueAccessor<string | undefined> {
  @Input()
  public placeholder = '';
}
