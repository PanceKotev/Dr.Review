import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'drreview-search-location',
  templateUrl: './search-location.component.html',
  styleUrls: ['./search-location.component.scss']
})
export class SearchLocationComponent {
  @Input()
  public parentFormGroup!: FormGroup;

  @Input()
  public searchFormControlName!: string;

  @Input()
  public placeholder = '';

  @Input()
  public name = '';

  public get control(): FormControl {
    return this.parentFormGroup.get(this.searchFormControlName) as FormControl;
  };

}
