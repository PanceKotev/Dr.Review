import { GetDoctorsFilter } from './../models/common/filters';
import { Result } from './../models/common/result';
import { ApiService } from './../base/api.service';
import { Injectable } from '@angular/core';
import { GetDoctorDetailsDto, GetDoctorsDto, SearchDoctorDto } from '../models/doctor';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorApiService {

  public constructor(private apiService: ApiService) {
  }

  public searchDoctors(searchWord: string): Observable<SearchDoctorDto[]> {
    return this.apiService.get<Result<SearchDoctorDto[]>>(`v1/doctors/search?searchword=${searchWord}`)
    .pipe(
      map((res: Result<SearchDoctorDto[]>) => res.value));
  }

  public getDoctorDetails(doctorSuid: string): Observable<GetDoctorDetailsDto> {
    return this.apiService.get<Result<GetDoctorDetailsDto>>(`v1/doctors/${doctorSuid}`)
    .pipe(
      map((res: Result<GetDoctorDetailsDto>) => res.value));
  }

  public getDoctors(getDoctorsFilter: GetDoctorsFilter | undefined = undefined,
    withSubscription: boolean = false): Observable<GetDoctorsDto> {
    let filterByQuery = getDoctorsFilter ? getDoctorsFilter.toQueryProperties(): '';

    filterByQuery = filterByQuery && withSubscription ? `${filterByQuery}&&withSubscriptions=true` : filterByQuery;

    return this.apiService.get<Result<GetDoctorsDto>>(`v1/doctors/${filterByQuery}`)
    .pipe(
      map((res: Result<GetDoctorsDto>) => res.value),
      tap(x => console.log('doctors', x)));
  }

  public getDoctorsCount(): Observable<number> {
    return this.apiService.get<Result<number>>(`v1/doctors/count`)
    .pipe(
      map((res: Result<number>) => res.value));
  }
}
