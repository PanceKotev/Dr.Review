import { Injectable } from "@angular/core";
import { DateRange } from "@angular/material/datepicker";
import { ApiService, GetScheduleSubscriptionsDto, Result } from "@drreview/shared/data-access";
import { drReviewDate } from "@drreview/shared/utils/date";
import { map, Observable } from "rxjs";
import {
  SubscribeToMultipleDoctorsSchedulesRequest,
  UpdateSubscriptionsRangeRequest } from "../models/schedule-subscription.requests";

@Injectable({
  providedIn: 'root'
})
export class ScheduleSubscriptionApiService {
  public constructor(private apiService: ApiService) {
  }

  public subscribeToDoctorSchedules(request: SubscribeToMultipleDoctorsSchedulesRequest): Observable<Result<void>>{
    return this.apiService.post<Result<void>>("v1/schedules", request);
  }

  public updateScheduleSubscriptions(updateSubscriptions: UpdateSubscriptionsRangeRequest): Observable<Result<void>>{
    return this.apiService.put<Result<void>>("v1/schedules", updateSubscriptions);
  }

  public unsubscribeFromDoctorSchedule(scheduleSuid: string):  Observable<Result<void>>{
    return this.apiService.delete<Result<void>>(`v1/schedules/${scheduleSuid}`);
  }

  public unsubscribeFromDoctorSchedules(scheduleSuids: string[]):  Observable<Result<void>>{
    return this.apiService.deleteWithBody<Result<void>>(`v1/schedules`, {
      scheduleSuids: scheduleSuids
    });
  }

  public getScheduleSubscriptions(
    page: number = 0,
    itemsPerPage: number = 50,
    range?: DateRange<Date>): Observable<GetScheduleSubscriptionsDto>{
    const rangeQuery = this.dateRangeToQuery(range);

    return this.apiService.get<Result<GetScheduleSubscriptionsDto>>(`v1/schedules?${rangeQuery}&page=${page}&itemsPerPage=${itemsPerPage}`)
    .pipe(
      map(r => r.value)
    );
  }

  public dateRangeToQuery(range?: DateRange<Date>): string {
    if(!range){
      return '';
    }

    const stringifiedFrom = range.start ? drReviewDate(range.start).format('YYYY-MM-DD')
    .toString() : undefined;

    const stringifiedTo = range.end ? drReviewDate(range.end).format('YYYY-MM-DD')
    .toString() : undefined;

    if(!stringifiedFrom || !stringifiedTo){
      return '';
    }

    return `rangeFrom=${stringifiedFrom}&rangeTo=${stringifiedTo}`;
  }
}
