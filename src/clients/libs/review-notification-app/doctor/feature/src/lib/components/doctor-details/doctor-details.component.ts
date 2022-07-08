import { Subject, takeUntil } from 'rxjs';
import { DoctorApiService, GetDoctorDetailsDto } from '@drreview/shared/data-access';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './doctor-details.component.html',
  styleUrls: ['./doctor-details.component.scss']
})
export class DoctorDetailsComponent implements OnInit {

  public doctor: GetDoctorDetailsDto | undefined;

  public doctorSuid: string;

  private destroying$ = new Subject();

  public constructor(
    private doctorsApi: DoctorApiService,
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
  }
}
