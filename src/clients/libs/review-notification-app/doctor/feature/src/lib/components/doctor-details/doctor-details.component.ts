import { Subject, takeUntil, switchMap, combineLatest, startWith } from 'rxjs';
import { DoctorApiService,
   ReviewApiService,
   GetDoctorDetailsDto,
   GetReviewDto,
    GetReviewSummaryDto,
     GetReviewsDto,
     VoteOnReviewRequest, UpdateReviewRequest, ScheduleNotificationRange } from '@drreview/shared/data-access';
import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReviewChangedEvent } from '@drreview/shared/ui/review';
import { SnackBarService } from '@drreview/shared/services/snack-bar';
import { Location } from '@angular/common';
import { ScheduleSubscriptionApiService } from '@drreview/review-notification-app/schedule-subscription/data-access';
import { dateToString } from '@drreview/shared/utils/date';

@Component({
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.scss']
})
export class DoctorDetailsComponent implements OnInit, OnDestroy {

  public doctor: GetDoctorDetailsDto | undefined;

  public doctorReviews: GetReviewDto[] = [];

  public doctorReviewSummary: GetReviewSummaryDto | undefined;

  public range: ScheduleNotificationRange | undefined;

  public initialDoctorRange:ScheduleNotificationRange | undefined;

  public rangesDiffering = false;

  public currentUserReview: GetReviewDto | undefined;

  public refreshReviews$ = new Subject<boolean>();

  public updateReview$ = new Subject<UpdateReviewRequest>();

  public createNewReview$ = new Subject<ReviewChangedEvent>();

  public deleteReview$ = new Subject<string>();

  public refreshDoctor$ = new Subject<boolean>();

  public doctorSuid: string;

  private destroying$ = new Subject();

  private voteOnReview$ = new Subject<VoteOnReviewRequest>();

  private scheduleSuid: string | undefined = '';

  public rangeSelectionFinished = true;

  public constructor(
    private doctorsApi: DoctorApiService,
    private reviewApi: ReviewApiService,
    private schedulesApi: ScheduleSubscriptionApiService,
    private zone: NgZone,
    private location: Location,
    private snackBar: SnackBarService,
    private route: ActivatedRoute) {
      this.doctorSuid = this.route.snapshot.params['doctorSuid'];
  }

  public ngOnInit(): void {
    if(!this.doctorSuid){
      return;
    }

    this.setUpSubscriptions();
  }

  public updateSchedule(): void {
    const deleteSubscription = this.range?.subscribedTo !== this.initialDoctorRange?.subscribedTo;
    const rangeValidState = this.range?.from && this.range.to;
    const rangeChanged = this.range ? this.range?.from !== this.initialDoctorRange?.from || this.range?.to !== this.initialDoctorRange?.to
      : undefined !== this.initialDoctorRange;

    if(this.scheduleSuid && deleteSubscription){
      this.schedulesApi.unsubscribeFromDoctorSchedules([this.scheduleSuid])
        .pipe(takeUntil(this.destroying$))
        .subscribe(() =>  this.refreshDoctor$.next(true));
    } else if(this.scheduleSuid && !deleteSubscription && rangeValidState && rangeChanged) {
      this.schedulesApi.updateScheduleSubscriptions({
        scheduleSuids: [this.scheduleSuid],
        rangeFrom: dateToString(this.range?.from),
        rangeTo: dateToString(this.range?.to)
      })
      .pipe(takeUntil(this.destroying$))
      .subscribe(() => this.refreshDoctor$.next(true));
    } else if(!this.scheduleSuid && rangeValidState){
      this.schedulesApi.subscribeToDoctorSchedules({
        doctorSuids: [this.doctorSuid],
        rangeFrom: dateToString(this.range?.from),
        rangeTo: dateToString(this.range?.to)
      })
      .pipe(takeUntil(this.destroying$))
      .subscribe(() => this.refreshDoctor$.next(true));
    }
  }
  public handleRangeSelectionFinishing(isFinished: boolean): void {
    if(isFinished){
      this.setStateRangesDiffering();
    }
  }
  public rangeChanged(): void {
    this.setStateRangesDiffering();
  }

  public setStateRangesDiffering(): void {
    if(!this.range || !this.range.from || !this.range.to){
      this.rangesDiffering = false;

      return;
    }


    this.rangesDiffering =
      this.range.from !== this.initialDoctorRange?.from ||
      this.range.to !== this.initialDoctorRange?.to ||
      this.range.subscribedTo !== this.initialDoctorRange?.subscribedTo;
  }
  public navigateToPrevious(): void {
    this.location.back();
  }

  private setUpSubscriptions(): void {
    // ---- Doctors ----
    this.refreshDoctor$.pipe(
      startWith(true),
      switchMap(() => this.doctorsApi.getDoctorDetails(this.doctorSuid)),
      takeUntil(this.destroying$))
    .subscribe({
      next: val => {
        this.doctor = val;
        this.range = val.scheduleSubscription ?  {...val.scheduleSubscription.range} : undefined;
        this.initialDoctorRange = val.scheduleSubscription ? {...val.scheduleSubscription.range} : undefined;
        this.scheduleSuid = val.scheduleSubscription?.scheduleSuid;
        this.setStateRangesDiffering();
        this.refreshReviews$.next(true);
      },
      error: err => {
        console.error(err);
        this.refreshReviews$.next(true);
      }
    });

    // ---- Reviews ----

    this.refreshReviews$.pipe(
      takeUntil(this.destroying$),
      switchMap(() => {

        return combineLatest([
                              this.reviewApi.getReviewsForDoctor(this.doctorSuid),
                              this.reviewApi.getReviewSummaryForDoctor(this.doctorSuid)]);
      })
    ).subscribe({
      next: ([reviews, summary] : [GetReviewsDto, GetReviewSummaryDto]) => {
        this.doctorReviews = reviews.reviews;
        this.currentUserReview = reviews.currentUserReview;
        this.doctorReviewSummary = summary;
        console.log(summary);
      },
      error: err => {
        console.error(err);
      },
      complete: () => {

      }
    });

    this.createNewReview$.pipe(
      switchMap((doctorCreateEvent) => {

        return this.reviewApi.addNewReview({
          revieweeSuid: this.doctorSuid,
          score: doctorCreateEvent.rating,
          comment:  doctorCreateEvent.comment
          });
      })).subscribe({
        next: () => {
          this.refreshReviews$.next(true);
        },
        error: err => {
          console.error(err);
        }
      });

    this.updateReview$.pipe(
      takeUntil(this.destroying$),
      switchMap((updateReview) => {
        return this.reviewApi.updateReview(updateReview);
      })).subscribe({
        next: () => {
          this.snackBar.openInfo("Рецензијата е успешно променета");
          this.refreshReviews$.next(true);
        },
        error: err => {
          console.error(err);
        }
      });

    this.deleteReview$.pipe(
      takeUntil(this.destroying$),
      switchMap((reviewSuid) => {

        return this.reviewApi.deleteReview(reviewSuid);
      })
    ).subscribe({
      next: () =>{
        this.snackBar.openInfo("Рецензијата е успешно избришена");

        this.refreshReviews$.next(true);
      },
      error: err => {
        console.error(err);
      }});

    this.voteOnReview$.pipe(
      takeUntil(this.destroying$),
      switchMap((voteRequest) => {

        return this.reviewApi.voteOnReview(voteRequest);
      })
    ).subscribe(() => this.refreshReviews$.next(true));


  }

  public handleCreateReview(event: ReviewChangedEvent): void {
    this.createNewReview$.next(event);
  }

  public handleReviewVote(reviewSuid: string, vote: boolean | undefined) : void {

    this.voteOnReview$.next({reviewSuid, vote});
  }

  public handleReviewChanged(reviewSuid: string, event: ReviewChangedEvent): void {
    this.updateReview$.next({reviewSuid: reviewSuid, score: event.rating, comment: event.comment});
  }

  public handleReviewDelete(reviewSuid: string): void {
    this.deleteReview$.next(reviewSuid);
  }

  public ngOnDestroy(): void {
      this.destroying$.next(true);
      this.destroying$.complete();
  }
}
