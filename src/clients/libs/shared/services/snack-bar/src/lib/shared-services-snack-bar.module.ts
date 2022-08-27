import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { SnackBarService } from './services/snack-bar.service';

@NgModule({
  imports: [
    CommonModule,
    MatSnackBarModule],
  providers: [
    {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 1250}},
    SnackBarService
  ]
})
export class SharedServicesSnackBarModule {}
