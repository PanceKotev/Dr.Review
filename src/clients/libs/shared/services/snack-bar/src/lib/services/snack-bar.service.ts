import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {
  private readonly closeMessage = "Затвори";
  public constructor(
    private zone: NgZone,
    private snackBar: MatSnackBar) {

   }

   public openInfo(message: string): void {
    this.zone.run(() => {
      this.snackBar.open(message, this.closeMessage);
    });
   }

   public openWarning(message: string): void {
    this.zone.run(() => {
      this.snackBar.open(message, this.closeMessage,  {panelClass: 'warning-snackbar'});
    });
   }

   public openSuccess(message: string): void {
    this.zone.run(() => {
      this.snackBar.open(message, this.closeMessage, {panelClass: 'success-snackbar'});
    });

   }
}
