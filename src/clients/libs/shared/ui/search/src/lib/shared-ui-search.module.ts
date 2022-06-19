import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchinputComponent } from './components/searchinput/searchinput.component';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatAutocompleteModule
  ],
  declarations: [SearchinputComponent],
  exports: [SearchinputComponent]
})
export class SharedUiSearchModule {}
