import { SubscribeToMultipleDoctorsSchedulesRequest, CreateNewScheduleSubscriptionsDialogData }
  from '@drreview/review-notification-app/schedule-subscription/data-access';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DoctorApiService, ScheduleNotificationRange, SearchDoctorDto } from '@drreview/shared/data-access';
import { inputToCyrillic } from '@drreview/shared/utils/rxjs';
import { concat, distinctUntilChanged, Observable, of, Subject, switchMap, tap, debounceTime, catchError } from 'rxjs';
import * as dayjs from 'dayjs';

@Component({
  selector: 'drreview-create-new-schedule-subscription-dialog',
  templateUrl: './create-new-schedule-subscription-dialog.component.html',
  styleUrls: ['./create-new-schedule-subscription-dialog.component.scss']
})
export class CreateNewScheduleSubscriptionDialogComponent {
  public searchForm: string | undefined;

  public rangeForm: ScheduleNotificationRange | undefined;
  public hiddenDoctors: SearchDoctorDto[] = [];
  public doctors$: Observable<SearchDoctorDto[]>;
  public doctorsLoading = false;
  public searchInput$ = new Subject<string>();
  public selectedDoctors: SearchDoctorDto[] = [];

  public trackByFn(item: SearchDoctorDto): string {
      return item.suid;
  }


  public constructor(
    private dialogRef: MatDialogRef<CreateNewScheduleSubscriptionDialogComponent>,
    private doctorsApi: DoctorApiService,
    @Inject(MAT_DIALOG_DATA) public data: CreateNewScheduleSubscriptionsDialogData | undefined){
      this.rangeForm = data?.selectedRange;
      this.doctors$ = concat(
        of([]), // default items
        this.searchInput$.pipe(
            debounceTime(200),
            distinctUntilChanged(),
            inputToCyrillic(),
            tap(() =>  this.doctorsLoading = true),
            switchMap(term => {
              if (!term?.length || term?.length < 3) {
                return of([]).pipe(tap(() => this.doctorsLoading = false));
              }

              return this.doctorsApi.searchDoctors(term).pipe(
                catchError(() => of([])),
                tap(() => this.doctorsLoading = false));

              })));
    }


  public closeDialog(): void {
    this.dialogRef.close();
  }

  public resetForm(): void {
  }
  public returnRequest(): void {
    if (!this.selectedDoctors.length || !this.rangeForm?.from || !this.rangeForm?.to){
      this.dialogRef.close();

      return;
    }

    const data: SubscribeToMultipleDoctorsSchedulesRequest = {
      doctorSuids: this.selectedDoctors.map(x => x.suid),
      rangeFrom: this.convertToString(this.rangeForm.from),
      rangeTo: this.convertToString(this.rangeForm.to)
    };

    this.dialogRef.close(data);
  }

  private convertToString(range: Date | undefined | null): string | undefined {

    return range ? dayjs(range).format('YYYY-MM-DD')
    .toString() : undefined;
  }

  public valueChangeLog(value: ScheduleNotificationRange | undefined): void {
    console.log(value);
  }
}
