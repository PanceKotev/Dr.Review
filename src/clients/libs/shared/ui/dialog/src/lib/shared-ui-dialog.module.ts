import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseDialogComponent } from './components/base-dialog/base-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  imports: [
    CommonModule,
    MatDialogModule],
  declarations: [BaseDialogComponent],
  exports: [BaseDialogComponent]
})
export class SharedUiDialogModule {}
