import { Component, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { ScheduleSubscriptionApiService, SubscribeToMultipleDoctorsSchedulesRequest, CreateNewScheduleSubscriptionsDialogData }
from '@drreview/review-notification-app/schedule-subscription/data-access';
import { DeleteDialogData, GetScheduleSubscriptionDto, PagingFilter } from '@drreview/shared/data-access';
import { SnackBarService } from '@drreview/shared/services/snack-bar';
import { ThemesService } from '@drreview/shared/services/themes';
import { DeleteDialogComponent } from '@drreview/shared/ui/dialog';
import { BehaviorSubject, Subject, takeUntil, switchMap, combineLatest, filter } from 'rxjs';
import { CreateNewScheduleSubscriptionDialogComponent }
from '../create-new-schedule-subscription-dialog/create-new-schedule-subscription-dialog.component';

@Component({
  selector: 'drreview-schedule-subscriptions-root',
  templateUrl: './schedule-subscriptions-root.component.html',
  styleUrls: ['./schedule-subscriptions-root.component.scss']
})
export class ScheduleSubscriptionsRootComponent implements OnDestroy {
  private destroying$ = new Subject<void>();
  private pagingChanged$ = new BehaviorSubject<PagingFilter>({page: 0, itemsPerPage: 10});
  private refreshSubscriptions$ = new BehaviorSubject<boolean>(true);
  private deleteSubscriptions$ = new Subject<string[]>();
  private createSubscriptions$ = new Subject<SubscribeToMultipleDoctorsSchedulesRequest>();

  public pageCount: number | undefined;


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
     this.initializeSubscriptions();
  }

  private initializeSubscriptions(): void {
    combineLatest([this.refreshSubscriptions$, this.pagingChanged$]).pipe(
      takeUntil(this.destroying$),
      switchMap(([,pagingFilter]) => this.scheduleApi.getScheduleSubscriptions(pagingFilter.page, pagingFilter.itemsPerPage)))
      .subscribe({
        next : (value) => {
            this.subscriptions = value.subscriptions;
            this.totalCount = value.totalCount;
            this.setCheckboxState();
        }
      });

    this.deleteSubscriptions$.pipe(
            takeUntil(this.destroying$),
            switchMap((scheduleSuids) => this.scheduleApi.unsubscribeFromDoctorSchedules(scheduleSuids))
          ).subscribe(() => this.refreshSubscriptions$.next(true));

    this.createSubscriptions$.pipe(
      takeUntil(this.destroying$),
      switchMap((scheduleSuidRequest) => this.scheduleApi.subscribeToDoctorSchedules(scheduleSuidRequest))
    ).subscribe(() => this.refreshSubscriptions$.next(true));


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

  public unsubscribeMultiple(): void {
    console.log('clicked');
    if(this.someChecked || this.allChecked){
      const itemsToDelete =  this.subscriptions.filter(x => !!this.checkedSubscriptions[x.suid]).map(x => x.suid);
      if(itemsToDelete.length){
        const dialogRef = this.dialogService.open<DeleteDialogComponent, DeleteDialogData, boolean>(DeleteDialogComponent, {

          width: '550px',
          minHeight: '150px',
          hasBackdrop: true,
          panelClass: this.isDarkTheme ? 'dark-theme' : '',
          data: {
            deleteTitle: 'Избриши ги селектираните претплати за нотификации?',
            deleteButtonName: undefined
          }});

        dialogRef.afterClosed().pipe(takeUntil(this.destroying$), filter(x => !!x))
          .subscribe(() => this.deleteSubscriptions$.next(itemsToDelete));;
        }
    }
  }

  public pageChanged(page: PageEvent): void {
    this.pagingChanged$.next({page: page.pageIndex, itemsPerPage: page.pageSize});
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

  public unsubsribeFromSubscriptions(subscriptionSuids: string[]): void {
    const dialogRef = this.dialogService.open<DeleteDialogComponent, DeleteDialogData, boolean>(DeleteDialogComponent, {

        width: '550px',
        minHeight: '150px',
        hasBackdrop: true,
        panelClass: this.isDarkTheme ? 'dark-theme' : '',
        data: {
          deleteTitle: 'Избриши ги претплатите за нотификации?',
          deleteButtonName: undefined
        }});

    dialogRef.afterClosed().pipe(takeUntil(this.destroying$), filter(x => !!x))
      .subscribe(() => this.deleteSubscriptions$.next(subscriptionSuids));
  }

  public handleCreateSubscription(): void {
    const dialogRef = this.dialogService
      .open<CreateNewScheduleSubscriptionDialogComponent,
            CreateNewScheduleSubscriptionsDialogData,
            SubscribeToMultipleDoctorsSchedulesRequest | undefined>(CreateNewScheduleSubscriptionDialogComponent, {
                                                                    width: '550px',
                                                                    height: '650px',
                                                                    hasBackdrop: true,
                                                                    disableClose: true,
                                                                    panelClass: this.isDarkTheme ? 'dark-theme' : '',
                                                                    data: {
                                                                      selectedRange: undefined
                                                                    }
    });
    dialogRef.afterClosed().pipe(takeUntil(this.destroying$), filter(x => !!x))
      .subscribe((data) => {
        if(data) {
          this.createSubscriptions$.next(data);
        }
      });
  }

  public ngOnDestroy(): void {
      this.destroying$.next();
      this.destroying$.complete();
  }
}
