import { Subject, takeUntil, switchMap, combineLatest } from 'rxjs';
import { DoctorApiService,
   ReviewApiService,
   GetDoctorDetailsDto,
   GetReviewDto, GetReviewSummaryDto, GetReviewsDto, VoteOnReviewRequest, UpdateReviewRequest } from '@drreview/shared/data-access';
import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReviewChangedEvent } from '@drreview/shared/ui/review';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.scss']
})
export class DoctorDetailsComponent implements OnInit, OnDestroy {

  public doctor: GetDoctorDetailsDto | undefined;

  public doctorReviews: GetReviewDto[] = [];

  public doctorReviewSummary: GetReviewSummaryDto | undefined;

  public currentUserReview: GetReviewDto | undefined;

  public refreshReviews$ = new Subject<boolean>();

  public updateReview$ = new Subject<UpdateReviewRequest>();

  public createNewReview$ = new Subject<ReviewChangedEvent>();

  public deleteReview$ = new Subject<string>();

  public doctorSuid: string;

  private destroying$ = new Subject();

  private voteOnReview$ = new Subject<VoteOnReviewRequest>();

  public constructor(
    private doctorsApi: DoctorApiService,
    private reviewApi: ReviewApiService,
    private zone: NgZone,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute) {
      this.doctorSuid = this.route.snapshot.params['doctorSuid'];
  }

  public ngOnInit(): void {
    if(!this.doctorSuid){
      return;
    }

    this.setUpSubscriptions();
  }

  private setUpSubscriptions(): void {
    // ---- Doctors ----
    this.doctorsApi.getDoctorDetails(this.doctorSuid).pipe(
      takeUntil(this.destroying$)
    )
    .subscribe({
      next: val => {
        this.doctor = val;
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
                              this.reviewApi.getReviewsForDoctor(this.doctorSuid, 'FRtJ-4ZPYkyh8d-fJGuXVg'),
                              this.reviewApi.getReviewSummaryForDoctor(this.doctorSuid)]);
      })
    ).subscribe({
      next: ([reviews, summary] : [GetReviewsDto, GetReviewSummaryDto]) => {
        console.log("reviews", reviews);
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
          this.zone.run(() => {
            this.snackBar.open("Рецензијата е успешно променета", "Затвори");
          });
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
        this.zone.run(() => {
          this.snackBar.open("Рецензијата е успешно избришена",  "Затвори");
        });

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
