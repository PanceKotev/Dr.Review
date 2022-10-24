import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeleteDialogData } from '@drreview/shared/data-access';

@Component({
  selector: 'drreview-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.scss']
})
export class DeleteDialogComponent {
  public constructor(
    private dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DeleteDialogData | undefined){
    }


  public cancel(): void {
    this.dialogRef.close(false);
  }

  public deleteItem(): void {
    this.dialogRef.close(true);
  }
}
