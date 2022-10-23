import { Injectable } from "@angular/core";
import { DateRange } from "@angular/material/datepicker";
import { ApiService, GetScheduleSubscriptionsDto, Result } from "@drreview/shared/data-access";
import { map, Observable } from "rxjs";
import { SubscribeToDoctorsScheduleRequest, UpdateSubscriptionRangeRequest } from "../models/schedule-subscription.requests";

@Injectable({
  providedIn: 'root'
})
export class ScheduleSubscriptionApiService {
  public constructor(private apiService: ApiService) {
  }

  public subscribeToDoctorSchedule(request: SubscribeToDoctorsScheduleRequest): Observable<Result<void>>{
    return this.apiService.post<Result<void>>("v1/schedules", request);
  }

  public updateScheduleSubscription(updateReview: UpdateSubscriptionRangeRequest): Observable<Result<void>>{
    return this.apiService.put<Result<void>>("v1/schedules", updateReview);
  }

  public unsubscribeFromDoctorSchedule(scheduleSuid: string):  Observable<Result<void>>{
    return this.apiService.delete<Result<void>>(`v1/schedules/${scheduleSuid}`);
  }

  public getScheduleSubscriptions(
    page: number = 0,
    itemsPerPage: number = 50,
    range?: DateRange<Date>): Observable<GetScheduleSubscriptionsDto>{
    const rangeQuery = range ? `rangeFrom=${range.start}&rangeTo=${range.end}`: '';

    return this.apiService.get<Result<GetScheduleSubscriptionsDto>>(`v1/schedules?${rangeQuery}&page=${page}&itemsPerPage=${itemsPerPage}`)
    .pipe(
      map(r => r.value)
    );
  }
}
