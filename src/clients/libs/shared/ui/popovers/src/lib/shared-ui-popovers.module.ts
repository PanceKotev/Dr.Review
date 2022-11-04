import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SliderPopoverComponent } from './components/slider-popover/slider-popover.component';
import { MtxSliderModule } from '@ng-matero/extensions/slider';
import { MtxPopoverModule } from '@ng-matero/extensions/popover';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MtxPopoverModule,
    MtxSliderModule],
  declarations: [SliderPopoverComponent],
  exports: [SliderPopoverComponent]
})
export class SharedUiPopoversModule {}
