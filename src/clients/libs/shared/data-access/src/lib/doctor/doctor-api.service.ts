import { GetDoctorsFilter } from './../models/common/filters';
import { Result } from './../models/common/result';
import { ApiService } from './../base/api.service';
import { Injectable } from '@angular/core';
import {
  GetDoctorDetailsDto,
  GetDoctorsDto,
  SearchDoctorDto
} from '../models/doctor';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorApiService {
  public constructor(private apiService: ApiService) {}

  public searchDoctors(searchWord: string, filterSchedules: boolean = false): Observable<SearchDoctorDto[]> {
    return this.apiService
      .get<Result<SearchDoctorDto[]>>(
        `v1/doctors/search?searchword=${searchWord}${filterSchedules ? `'&filterSchedules=${filterSchedules}` : ''}`
      )
      .pipe(
        map((res: Result<SearchDoctorDto[]>) =>
          res.value.map((x) => ({...x, fullName: `${x.firstName} ${x.lastName}`}))
        )
      );
  }

  public getDoctorDetails(doctorSuid: string): Observable<GetDoctorDetailsDto> {
    return this.apiService
      .get<Result<GetDoctorDetailsDto>>(`v1/doctors/${doctorSuid}`)
      .pipe(map((res: Result<GetDoctorDetailsDto>) => res.value));
  }

  public getDoctors(
    getDoctorsFilter: GetDoctorsFilter | undefined = undefined
  ): Observable<GetDoctorsDto> {
    const filterByQuery = getDoctorsFilter
      ? getDoctorsFilter.toQueryProperties()
      : '';

    return this.apiService
      .get<Result<GetDoctorsDto>>(`v1/doctors/${filterByQuery}`)
      .pipe(
        map((res: Result<GetDoctorsDto>) => ({
          ...res.value,
          doctors: res.value.doctors.map((x) => ({...x, fullName: `${x.firstName} ${x.lastName}`}))})),
        tap((x) => console.log('doctors', x))
      );
  }

  public getDoctorsCount(): Observable<number> {
    return this.apiService
      .get<Result<number>>(`v1/doctors/count`)
      .pipe(map((res: Result<number>) => res.value));
  }
}
