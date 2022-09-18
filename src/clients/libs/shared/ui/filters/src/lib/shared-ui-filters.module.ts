import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChipFilterComponent } from './components/chip-filter/chip-filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AdditionalSelectFilterComponent } from './components/additional-select-filter/additional-select-filter.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatSelectModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatRippleModule,
    MatFormFieldModule
  ],
  declarations: [ChipFilterComponent, AdditionalSelectFilterComponent],
  exports: [ChipFilterComponent, AdditionalSelectFilterComponent]
})
export class SharedUiFiltersModule {}
