import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DFormatPipe } from './pipes/d-format.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    DFormatPipe
  ],
  providers: [
    DFormatPipe
  ],
  exports: [
    DFormatPipe
  ]
})
export class SharedUtilsDateModule {}
