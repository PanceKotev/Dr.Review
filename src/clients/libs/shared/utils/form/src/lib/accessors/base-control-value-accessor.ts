/* eslint-disable @typescript-eslint/no-explicit-any */
import { ChangeDetectorRef, Directive, EventEmitter, Output } from "@angular/core";
import { ControlValueAccessor } from "@angular/forms";

@Directive()
export abstract class BaseControlValueAccessor<TValue> implements ControlValueAccessor {
  public value!: TValue;

  public constructor(public readonly cdr?: ChangeDetectorRef) {}

  @Output()
  public valueChanged: EventEmitter<TValue> = new EventEmitter<TValue>();

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  public propagateChange = (_: any): any => {};

  public propagateOnTouched = ():any => {};

  public writeValue(obj: TValue): void {
    this.value = obj;
    this.cdr?.markForCheck();
  }
  public registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  public registerOnTouched(fn: any): void {
    this.propagateOnTouched = fn;
  }

  public onChange(value: TValue): void {
    this.value = value;
    if (this.propagateChange) {
      this.propagateChange(this.value);
    }
    this.valueChanged.emit(this.value);
  }

  public onTouched(): void {
    this.propagateOnTouched();
  }
}
