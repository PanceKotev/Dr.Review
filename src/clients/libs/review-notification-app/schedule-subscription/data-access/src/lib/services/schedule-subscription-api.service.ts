import { Injectable } from "@angular/core";
import { ApiService, Result } from "@drreview/shared/data-access";
import { Observable } from "rxjs";
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
}
