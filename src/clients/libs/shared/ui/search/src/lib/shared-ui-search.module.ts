import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchinputComponent } from './components/searchinput/searchinput.component';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SearchLocationComponent } from './components/search-location/search-location.component';
import { SearchComponent } from './components/search/search.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule,
    MatFormFieldModule,
    MatAutocompleteModule
  ],
  declarations: [
    SearchinputComponent,
    SearchLocationComponent,
    SearchComponent
  ],
  exports: [
    SearchinputComponent,
    SearchLocationComponent,
    SearchComponent
    ]
})
export class SharedUiSearchModule {}
