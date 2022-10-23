import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseDialogComponent } from './components/base-dialog/base-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { DeleteDialogComponent } from './components/delete-dialog/delete-dialog.component';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    MatDialogModule],
  declarations: [BaseDialogComponent, DeleteDialogComponent],
  exports: [BaseDialogComponent, DeleteDialogComponent]
})
export class SharedUiDialogModule {}
