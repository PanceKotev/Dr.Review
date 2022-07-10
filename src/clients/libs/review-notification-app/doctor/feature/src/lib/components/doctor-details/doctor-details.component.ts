/* eslint-disable @typescript-eslint/no-non-null-assertion */

import { Subject, takeUntil, switchMap, EMPTY } from 'rxjs';
import { DoctorApiService, ReviewApiService, GetDoctorDetailsDto } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.scss']
})
export class DoctorDetailsComponent implements OnInit, OnDestroy {

  public doctor: GetDoctorDetailsDto | undefined;

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
          score: 0,
          comment: this.commentToSave
          });
      })).subscribe({
        next: res => {
          console.log("success", res);
        },
        error: err => {
          console.error(err);
        }
      });
  }

  public saveCommentClicked(comment: string | undefined): void {
    console.log(comment);
    this.commentToSave = comment;
    this.saveButtonClicked$.next(true);
  }

  public ngOnDestroy(): void {
      this.destroying$.next(true);
      this.destroying$.complete();
  }
}
