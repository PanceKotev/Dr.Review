import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorsFacade, filterOptions } from '@drreview/review-notification-app/doctor/data-access';
import { ScheduleSubscriptionApiService } from '@drreview/review-notification-app/schedule-subscription/data-access';
import { FilterBy,
  DoctorApiService,
  GetDoctorsFilter, PagingFilter, GetDoctorsDto,
  IAdditionalSelectConfig, OptionApiService,
  ScheduleNotificationRange,
  GetDoctorScheduleSubscriptionDto} from '@drreview/shared/data-access';
import { valueNotNull } from '@drreview/shared/utils/typescript-helpers';
import * as dayjs from 'dayjs';
import { BehaviorSubject, Subject, takeUntil, switchMap, Observable, EMPTY, combineLatest, tap, of, startWith } from 'rxjs';

@Component({
  selector: 'drreview-doctors-root',
  templateUrl: './doctors-root.component.html',
  styleUrls: ['./doctors-root.component.scss']
})
export class DoctorsRootComponent implements OnInit{
  public filterOptions = filterOptions;
  public filterValue = FilterBy.ALL;

  public defaultRange: ScheduleNotificationRange = {
    from: null,
    to: null,
    subscribedTo: false
  };

  public selectedFilter = '';

  public onlySubscriptions = false;

  public FilterBy = FilterBy;

  @ViewChild(MatPaginator, {static: false}) public paginator!: MatPaginator;

  public destroying$ = new Subject();
  public pagingChanged$ = new BehaviorSubject<PagingFilter>({page: 0, itemsPerPage: 100});
  public pageCount: number | undefined;
  public refreshDoctors$ = new BehaviorSubject<FilterBy>(FilterBy.ALL);
  public doctors$: Observable<GetDoctorsDto> | undefined;
  public scheduleSubscriptionGroups = new Map<string, GetDoctorScheduleSubscriptionDto>();
  public additionalFilterSelectConfig$ = new BehaviorSubject<IAdditionalSelectConfig>({
    filterType: FilterBy.ALL,
    items$: of([])
  });

  public constructor(
    private route: ActivatedRoute,
    private router: Router,
    private scheduleSubscriptionApiService : ScheduleSubscriptionApiService,
    private optionsApiService: OptionApiService,
    private doctorsFacade: DoctorsFacade,
    private doctorApiService: DoctorApiService){
    const filterByEntries = Object.entries(FilterBy);

    combineLatest([this.route.paramMap, this.route.queryParamMap.pipe(startWith(undefined))]).pipe(takeUntil(this.destroying$))
      .subscribe({
      next : ([res, query]) => {
        const subscriptionToggled = !!query?.get('onlySubscriptions');
        this.onlySubscriptions = subscriptionToggled;


        const paramFilterBy = res.get('filterType');
        const selectedValue = res.get('filterValue');

        if(!paramFilterBy){
          return;
        }

        const isFilterBy = filterByEntries.find(([k, ,]) => k === paramFilterBy.toUpperCase());


        if(isFilterBy){
          this.selectedFilter = selectedValue ?? '';

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
      }), tap((val) => {
        this.pageCount = val ? val.totalCount : 0;
        const group = new Map<string, GetDoctorScheduleSubscriptionDto>();
        if(val){
          val.doctors.filter(valueNotNull("scheduleSubscription"))
            .forEach(x => group.set(x.scheduleSubscription.suid, x.scheduleSubscription));
        }
        this.scheduleSubscriptionGroups = group;
      }));

  }

  public filterValueChanged(value: string | undefined): void {
    this.selectedFilter = value || '';
  }

  public changeSubscriptionMode(value: boolean): void {
    this.router.navigate(['/doctors/filter/', FilterBy[this.filterValue].toLowerCase(), this.selectedFilter],
      {queryParams: value? { 'onlySubscriptions': value } : {}});
  }

  public filterChanged(value: FilterBy): void {
    this.router.navigate(['/doctors/filter/', FilterBy[value].toLowerCase(), this.selectedFilter]);
    this.refreshDoctors$.next(value);
    this.resetPaginationValues();
  }


  public rangeChanged(doctorSuid: string, range: ScheduleNotificationRange | undefined, scheduleSuid?: string): void {

    const existingSchedule = scheduleSuid ? this.scheduleSubscriptionGroups.get(scheduleSuid) : undefined;

    if(range && !scheduleSuid && range.subscribedTo && !existingSchedule){
      this.scheduleSubscriptionApiService.subscribeToDoctorSchedules(
        {doctorSuids: [doctorSuid],
          rangeFrom: this.convertToString(range.from),
          rangeTo: this.convertToString(range.to)})
      .subscribe(() => {
        this.refreshDoctors$.next(this.filterValue);
      });
    } else if(range && scheduleSuid && range.subscribedTo !== existingSchedule?.range.subscribedTo){
      this.scheduleSubscriptionApiService.unsubscribeFromDoctorSchedule(scheduleSuid).subscribe(() => {
        this.refreshDoctors$.next(this.filterValue);
      });
    } else if (range && scheduleSuid && (range.from !== existingSchedule?.range.from || range?.to !== existingSchedule?.range.to)){
      this.scheduleSubscriptionApiService
        .updateScheduleSubscription({
          rangeFrom: this.convertToString(range.from),
          rangeTo:this.convertToString(range.to),
          scheduleSuid: scheduleSuid}).subscribe(() => {
            this.refreshDoctors$.next(this.filterValue);
          });
    }
  }

  private convertToString(range: Date | undefined | null): string | undefined {

    return range ? dayjs(range).format('YYYY-MM-DD')
    .toString() : undefined;
  }

  public returnFilterValue(value: FilterBy, page: PagingFilter): Observable<GetDoctorsDto>{
    switch(value) {
      case FilterBy.LOCATION: {
        this.additionalFilterSelectConfig$.next({filterType: FilterBy.LOCATION, items$: this.optionsApiService.getLocationOptions(),
           onlySubscriptions: !!this.onlySubscriptions});

        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.LOCATION, value: this.selectedFilter}, page.page, page.itemsPerPage), true);
      }
      case FilterBy.INSTITUTION: {
        this.additionalFilterSelectConfig$.next({filterType: FilterBy.INSTITUTION, items$: this.optionsApiService.getInstitutionOptions(),
          onlySubscriptions: !!this.onlySubscriptions});

        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.INSTITUTION,
            value: this.selectedFilter}, page.page, page.itemsPerPage), true);
      }
      case FilterBy.SPECIALIZATION: {
        this.additionalFilterSelectConfig$.next({filterType: FilterBy.SPECIALIZATION,
          items$: this.optionsApiService.getSpecializationOptions(), onlySubscriptions: !!this.onlySubscriptions});

        return this.doctorApiService.getDoctors(
          new GetDoctorsFilter({property: FilterBy.SPECIALIZATION, value: this.selectedFilter}, page.page, page.itemsPerPage), true);
      }
      default: {
        this.additionalFilterSelectConfig$.next({filterType: FilterBy.ALL,
          items$:
          of([]),
        onlySubscriptions: !!this.onlySubscriptions});

        return this.doctorApiService.getDoctors(new GetDoctorsFilter(undefined, page.page, page.itemsPerPage), true)
          .pipe(tap(dr => this.doctorsFacade.setDoctors(dr)));
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
