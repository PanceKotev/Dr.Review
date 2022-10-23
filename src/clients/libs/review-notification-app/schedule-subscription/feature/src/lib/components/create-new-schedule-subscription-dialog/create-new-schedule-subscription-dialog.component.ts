import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'drreview-create-new-schedule-subscription-dialog',
  templateUrl: './create-new-schedule-subscription-dialog.component.html',
  styleUrls: ['./create-new-schedule-subscription-dialog.component.scss']
})
export class CreateNewScheduleSubscriptionDialogComponent {

  public constructor(
    private dialogRef: MatDialogRef<CreateNewScheduleSubscriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: unknown){

    }


  public closeDialog(): void {
    this.dialogRef.close();
  }
}
