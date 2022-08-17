import { Subject, takeUntil, switchMap, EMPTY, of, combineLatest } from 'rxjs';
import { DoctorApiService,
   ReviewApiService,
   GetDoctorDetailsDto,
   GetReviewDto, GetReviewSummaryDto, GetReviewsDto, VoteOnReviewRequest } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

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

  public rating = 0;

  public ratingToSave = 0;

  public saveButtonClicked$ = new Subject();

  public comment: string | undefined;

  public commentToSave: string | undefined;

  public doctorSuid: string;

  private destroying$ = new Subject();

  private voteOnReview$ = new Subject();

  private voteOnReviewRequest: VoteOnReviewRequest | undefined;

  public constructor(
    private doctorsApi: DoctorApiService,
    private reviewApi: ReviewApiService,
    private route: ActivatedRoute) {
      this.doctorSuid = this.route.snapshot.params['doctorSuid'];
  }

  public ngOnInit(): void {
    if(!this.doctorSuid){
      return;
    }

    this.doctorsApi.getDoctorDetails(this.doctorSuid).pipe(
      takeUntil(this.destroying$)
    )
    .subscribe({
      next: val => {
        console.log("doctor", val);
        this.doctor = val;
        this.refreshReviews$.next(true);
      },
      error: err => {
        console.error(err);
        this.refreshReviews$.next(true);
      }
    });
    this.voteOnReview$.pipe(
      takeUntil(this.destroying$),
      switchMap(() => {
        if(!this.voteOnReviewRequest){
          return of(null);
        }

        return this.reviewApi.voteOnReview(this.voteOnReviewRequest);
      })
    ).subscribe(() => this.refreshReviews$.next(true));

    this.refreshReviews$.pipe(
      takeUntil(this.destroying$),
      switchMap(() => {
        console.log('refreshed');
        if(!this.doctor?.suid){
          return combineLatest([of({
            reviews: [],
            currentUserReview: undefined
          }),
          of({
            rating: 0,
            reviewCountByStar: {
              1: 0,
              2: 0,
              3: 0,
              4: 0,
              5: 0
            }})]);
        }

        return combineLatest([
                              this.reviewApi.getReviewsForDoctor(this.doctor.suid, 'FRtJ-4ZPYkyh8d-fJGuXVg'),
                              this.reviewApi.getReviewSummaryForDoctor(this.doctor.suid)]);
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

    this.saveButtonClicked$.pipe(
      switchMap(() => {

        if(!this.doctor){
          return EMPTY;
        }

        return this.reviewApi.addNewReview({
          revieweeSuid: this.doctor.suid,
          score: this.ratingToSave,
          comment: this.commentToSave
          });
      })).subscribe({
        next: res => {
          console.log("success", res);
          this.refreshReviews$.next(true);
        },
        error: err => {
          console.error(err);
        }
      });
  }

  public saveCommentClicked({comment, rating}: {comment: string | undefined, rating: number}): void {
    console.log(comment);
    this.commentToSave = comment;
    this.ratingToSave = rating;
    this.saveButtonClicked$.next(true);
  }

  public handleReviewVote(reviewSuid: string, vote: boolean | undefined) : void {
    this.voteOnReviewRequest = {
      reviewSuid: reviewSuid,
      vote: vote
    };

    this.voteOnReview$.next(true);
  }

  public ngOnDestroy(): void {
      this.destroying$.next(true);
      this.destroying$.complete();
  }
}
