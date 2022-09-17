import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filterOptions } from '@drreview/review-notification-app/doctor/data-access';
import { FilterBy,  DoctorApiService, SearchDoctorDto, GetDoctorsFilter } from '@drreview/shared/data-access';
import { BehaviorSubject, Subject, takeUntil, switchMap, Observable, EMPTY } from 'rxjs';

@Component({
  selector: 'drreview-doctors-root',
  templateUrl: './doctors-root.component.html',
  styleUrls: ['./doctors-root.component.scss']
})
export class DoctorsRootComponent implements OnInit{
  public filterOptions = filterOptions;
  public filterValue = '';
  public destroying$ = new Subject();

  public refreshDoctors$ = new BehaviorSubject<FilterBy | undefined>(undefined);
  public doctors$: Observable<SearchDoctorDto[]> | undefined;

  public constructor(private route: ActivatedRoute, private doctorApiService: DoctorApiService){

  }

  public ngOnInit(): void {
      this.doctors$ = this.refreshDoctors$.pipe(
        takeUntil(this.destroying$),
        switchMap((filterVal) => {
          if(filterVal === undefined){
            return EMPTY;
          }

          return this.returnFilterValue(filterVal);
        }));

  }
  public filterChanged(value: FilterBy): void {
    console.log(value);
    this.refreshDoctors$.next(value);
  }


  public returnFilterValue(value: FilterBy): Observable<SearchDoctorDto[]>{
    switch(value) {
      case FilterBy.ALL: {
        return this.doctorApiService.getDoctors();
      }
      case FilterBy.LOCATION: {
        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.LOCATION, value: 'Гази Баба'}));
      }
      case FilterBy.INSTITUTION: {
        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.INSTITUTION, value: 'ЈЗУ Градска општа болница 8-ми Септември'}));
      }
      case FilterBy.SPECIALIZATION: {
        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.SPECIALIZATION, value: 'Медицинска биохемија'}));
      }
      default: {
        return this.doctorApiService.getDoctors();
      }
    }
  }
}
