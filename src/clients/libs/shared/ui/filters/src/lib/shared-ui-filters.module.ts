import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChipFilterComponent } from './components/chip-filter/chip-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatRippleModule,
    MatFormFieldModule],
  declarations: [ChipFilterComponent],
  exports: [ChipFilterComponent]
})
export class SharedUiFiltersModule {}
