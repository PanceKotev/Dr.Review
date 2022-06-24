import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { Component, ElementRef, HostBinding, Input, OnDestroy, Optional, Self } from '@angular/core';
import { ControlValueAccessor, FormBuilder, FormGroup, NgControl } from '@angular/forms';
import {MatFormFieldControl} from '@angular/material/form-field';
import { Subject } from 'rxjs';

class SearchInput {
  public constructor(public searchString: string, public location: string) {}
};

@Component({
  selector: 'drreview-searchinput',
  templateUrl: './searchinput.component.html',
  styleUrls: ['./searchinput.component.scss'],
  providers: [{provide: MatFormFieldControl, useExisting: SearchinputComponent}]
})
export class SearchinputComponent implements MatFormFieldControl<SearchInput>, ControlValueAccessor, OnDestroy{
  public static nextId = 0;

  public parts: FormGroup;

  @Input()
  public get value(): SearchInput | null {
    const n = this.parts.value;
    if (n.searchText.length && n.location.length) {
      console.log(n);

      return new SearchInput(n.searchText, n.location);
    }

    return null;
  }
  public set value(search: SearchInput | null) {
    console.log("asd");

    console.log(search);
    const s = search || new SearchInput('', '');
    this.parts.setValue({searchString: s.searchString, location: s.location});
    this.stateChanges.next();
  }

  public constructor(private fb: FormBuilder, private elementRef: ElementRef, @Optional() @Self() public ngControl: NgControl) {
    console.log(elementRef, ngControl);
    this.parts =  fb.group({
      'searchText': '',
      'location': ''
    });

    if (this.ngControl != null) {
      this.ngControl.valueAccessor = this;
    }
  }
  public onChange!: (search: SearchInput) => void;
  public focused = false;

  public stateChanges: Subject<void> = new Subject<void>();


  @HostBinding()
  public  id = `search-input-${SearchinputComponent.nextId++}`;

  @Input()
  public get placeholder(): string {
    return this._placeholder;
  }
  public set placeholder(plh: string) {
  this._placeholder = plh;
  this.stateChanges.next();
}

  private _placeholder = '';


  public get empty(): boolean{
    const n = this.parts.value;

    return !n.searchText && !n.location;
  }
  @HostBinding('class.floating')
  public get shouldLabelFloat():boolean{
    return this.focused || !this.empty;
  }
  @Input()
  public get required(): boolean {
    return this._required;
  }

  public set required(req: boolean) {
    this._required = coerceBooleanProperty(req);
    this.stateChanges.next();
  }

  private _required = false;
  @Input()
  public get disabled(): boolean {
    return this._disabled;
  }

  public set disabled(value: boolean) {
    this._disabled = coerceBooleanProperty(value);
    // eslint-disable-next-line no-unused-expressions
    this._disabled ? this.parts.disable() : this.parts.enable();
    this.stateChanges.next();
  }
  private _disabled = false;

  public get errorState(): boolean {
    return this.parts.invalid && this.touched;
  }
  public controlType = 'search-input';

  public touched = false;
  public autofilled?: boolean | undefined;

  // eslint-disable-next-line @angular-eslint/no-input-rename
  @Input('aria-describedby')
  public userAriaDescribedBy?: string | undefined;

  public setDescribedByIds(ids: string[]): void {
    if(this.elementRef){
      const controlElement = this.elementRef.nativeElement
      .querySelector('.search-input-input-container');
      if(controlElement){
        controlElement.setAttribute('aria-describedby', ids.join(' '));

      }
    }

  }

  public onContainerClick(event: MouseEvent): void {
    console.log(event);
    if ((event.target as Element).tagName.toLowerCase() !== 'input') {
      this.elementRef.nativeElement.querySelector('input').focus();
    }
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public writeValue(search: SearchInput): void {
    console.log("asd");

    console.log(search);
    this.value = search;
  }
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public registerOnChange(fn: any): void {
    console.log(fn);
    this.onChange = fn;
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public registerOnTouched(fn: any): void {
    console.log(fn);

    this.onTouched = fn;
  }

  public onFocusIn(): void {
    console.log(this.parts);

    if (!this.focused) {
      this.focused = true;
      this.stateChanges.next();
    }
  }
  public onTouched(): void {
    this.touched = true;
    this.stateChanges.next();
  }

  public onFocusOut(event: FocusEvent): void {
    if (!this.elementRef.nativeElement.contains(event.relatedTarget as Element)) {
      this.touched = true;
      this.focused = false;
      this.onTouched();
      this.stateChanges.next();
    }
  }

  public ngOnDestroy(): void {
      this.stateChanges.complete();
  }
}
