import { Component, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ScheduleSubscriptionApiService } from '@drreview/review-notification-app/schedule-subscription/data-access';
import { GetScheduleSubscriptionDto } from '@drreview/shared/data-access';
import { SnackBarService } from '@drreview/shared/services/snack-bar';
import { ThemesService } from '@drreview/shared/services/themes';
import { Subject, takeUntil } from 'rxjs';
import { CreateNewScheduleSubscriptionDialogComponent }
from '../create-new-schedule-subscription-dialog/create-new-schedule-subscription-dialog.component';

@Component({
  selector: 'drreview-schedule-subscriptions-root',
  templateUrl: './schedule-subscriptions-root.component.html',
  styleUrls: ['./schedule-subscriptions-root.component.scss']
})
export class ScheduleSubscriptionsRootComponent implements OnDestroy {
  private destroying$ = new Subject<void>();

  public checkedSubscriptions: Record<string, boolean> = {};
  public expandedSubscriptions: Record<string, boolean> = {};

  public allExpanded = false;

  public someChecked = false;
  public allChecked = false;

  public editMode = false;
  public totalCount = 0;
  public isDarkTheme = false;

  public subscriptions: GetScheduleSubscriptionDto[] = [];

  public constructor(
    private snackbar: SnackBarService,
    private dialogService: MatDialog,
    private themeService: ThemesService,
    private scheduleApi: ScheduleSubscriptionApiService){
    this.scheduleApi.getScheduleSubscriptions(0, 50)
      .pipe(takeUntil(this.destroying$))
      .subscribe({
        next : (value) => {
            this.subscriptions = value.subscriptions;
            this.totalCount = value.totalCount;
        }
      });
    this.themeService.isDarkTheme$.pipe(takeUntil(this.destroying$)).subscribe((val) => this.isDarkTheme = val);


  }
  public toggleAllExpanded(): void {
    this.allExpanded = !this.allExpanded;
    this.subscriptions.forEach(y => this.expandedSubscriptions[y.suid] = this.allExpanded);

  }

  public onCheckboxClick(scheduleSuid: string, checked: boolean): void {
    this.checkedSubscriptions[scheduleSuid] = checked;
    this.setCheckboxState();

  }

  public selectAllCheckboxClicked(checked: boolean): void {
    this.subscriptions.forEach(x => this.checkedSubscriptions[x.suid] = checked);
    this.setCheckboxState();
  }

  public setCheckboxState(): void {
    const allChecked = this.subscriptions.filter(x => !!this.checkedSubscriptions[x.suid]);
    this.someChecked = allChecked.length > 0 && allChecked.length !== this.subscriptions.length;
    this.allChecked =allChecked.length > 0 && allChecked.length === this.subscriptions.length;
  }

  public editModeChange(change: boolean): void {
    this.snackbar.openInfo(`Сега сте во состојба на ${change ? 'едитирање' : 'филтрирање'}`);
  }

  public handleCreateSubscription(): void {
    const dialogRef = this.dialogService.open(CreateNewScheduleSubscriptionDialogComponent, {
      width: '550px',
      minHeight: '450px',
      hasBackdrop: true,
      panelClass: this.isDarkTheme ? 'dark-theme' : ''
    });
    dialogRef.afterClosed().subscribe(() => console.log('closed'));
  }

  public ngOnDestroy(): void {
      this.destroying$.next();
      this.destroying$.complete();
  }
}
