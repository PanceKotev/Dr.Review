import { Subject, takeUntil, switchMap, EMPTY, of } from 'rxjs';
import { DoctorApiService, ReviewApiService, GetDoctorDetailsDto, GetReviewsDto } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.scss']
})
export class DoctorDetailsComponent implements OnInit, OnDestroy {

  public doctor: GetDoctorDetailsDto | undefined;

  public doctorReviews: GetReviewsDto[] = [];

  public refreshReviews$ = new Subject<boolean>();

  public rating = 0;

  public ratingToSave = 0;

  public saveButtonClicked$ = new Subject();

  public comment: string | undefined;

  public commentToSave: string | undefined;

  public doctorSuid: string;

  private destroying$ = new Subject();

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

    this.refreshReviews$.pipe(
      takeUntil(this.destroying$),
      switchMap(() => {
        console.log('refreshed');
        if(!this.doctor?.suid){
          return of([]);
        }

        return this.reviewApi.getReviewsForDoctor(this.doctor.suid, 'FRtJ-4ZPYkyh8d-fJGuXVg');
      })
    ).subscribe({
      next: val => {
        console.log("reviews", val);
        this.doctorReviews = val;
      },
      error: err => {
        console.error(err);
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

  public ngOnDestroy(): void {
      this.destroying$.next(true);
      this.destroying$.complete();
  }
}
