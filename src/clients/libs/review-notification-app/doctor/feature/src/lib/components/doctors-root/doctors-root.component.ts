import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { filterOptions } from '@drreview/review-notification-app/doctor/data-access';
import { FilterBy,  DoctorApiService, SearchDoctorDto, GetDoctorsFilter, PagingFilter } from '@drreview/shared/data-access';
import { BehaviorSubject, Subject, takeUntil, switchMap, Observable, EMPTY, combineLatest } from 'rxjs';

@Component({
  selector: 'drreview-doctors-root',
  templateUrl: './doctors-root.component.html',
  styleUrls: ['./doctors-root.component.scss']
})
export class DoctorsRootComponent implements OnInit{
  public filterOptions = filterOptions;
  public filterValue = FilterBy.ALL;
  public pageIndex = 0;
  public pageSize = 100;

  @ViewChild(MatPaginator, {static: false}) public paginator!: MatPaginator;

  public destroying$ = new Subject();
  public pagingChanged$ = new BehaviorSubject<PagingFilter>({page: 0, itemsPerPage: 100});

  public refreshDoctors$ = new BehaviorSubject<FilterBy | undefined>(undefined);
  public doctors$: Observable<SearchDoctorDto[]> | undefined;

  public constructor(private route: ActivatedRoute, private doctorApiService: DoctorApiService){
    const filterByEntries = Object.entries(FilterBy);

    this.route.paramMap.pipe(takeUntil(this.destroying$)).subscribe({
      next : res => {
        const paramFilterBy = res.get('filterType');

        if(!paramFilterBy){
          return;
        }

        const isFilterBy = filterByEntries.find(([k, ,]) => k === paramFilterBy.toUpperCase());


        if(isFilterBy){
          this.filterValue = isFilterBy[1] as FilterBy;
          this.refreshDoctors$.next(isFilterBy[1] as FilterBy);
          this.resetPaginationValues();

        }
      }
    });
  }

  public ngOnInit(): void {
      this.doctors$ = combineLatest([this.refreshDoctors$, this.pagingChanged$]).pipe(
        switchMap(([filterVal, page]) => {
          if(filterVal === undefined){
            return EMPTY;
          }

          return this.returnFilterValue(filterVal, page);
        }));

  }
  public filterChanged(value: FilterBy): void {
    this.refreshDoctors$.next(value);
    this.resetPaginationValues();

  }


  public returnFilterValue(value: FilterBy, page: PagingFilter): Observable<SearchDoctorDto[]>{
    switch(value) {
      case FilterBy.ALL: {
        return this.doctorApiService.getDoctors(new GetDoctorsFilter(undefined, page.page, page.itemsPerPage));
      }
      case FilterBy.LOCATION: {
        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.LOCATION, value: 'Гази Баба'}, page.page, page.itemsPerPage));
      }
      case FilterBy.INSTITUTION: {
        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.INSTITUTION,
            value: 'ЈЗУ Градска општа болница 8-ми Септември'}, page.page, page.itemsPerPage));
      }
      case FilterBy.SPECIALIZATION: {
        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.SPECIALIZATION, value: 'Медицинска биохемија'}, page.page, page.itemsPerPage));
      }
      default: {
        return this.doctorApiService.getDoctors(new GetDoctorsFilter(undefined, page.page, page.itemsPerPage));
      }
    }
  }

  public pageChanged(page: PageEvent): void {
    this.pagingChanged$.next({page: page.pageIndex, itemsPerPage: page.pageSize});
  }

  public resetPaginationValues(): void {
    if(this.paginator){
      this.paginator.pageIndex = 0;
      this.paginator.pageSize = 100;
    }
    this.pagingChanged$.next({page: 0, itemsPerPage: 100});
  }
}
