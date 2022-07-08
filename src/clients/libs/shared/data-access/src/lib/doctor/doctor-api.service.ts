import { Result } from './../models/common/result';
import { ApiService } from './../base/api.service';
import { Injectable } from '@angular/core';
import { GetDoctorDetailsDto, SearchDoctorDto } from '../models/doctor';
import { map, Observable } from 'rxjs';

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
}
