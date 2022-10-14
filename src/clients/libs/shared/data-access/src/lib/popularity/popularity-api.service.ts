import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { ApiService } from "../base/api.service";
import { Result } from "../models/common";
import { TopDoctor } from "../models/doctor";

@Injectable({
  providedIn: 'root'
})
export class PopularityApiService {

  public constructor(private apiService: ApiService) {
  }

  public getTopDoctorsNearLocation(locationSuid: string, distance: number = 26, doctorLimit: number = 3): Observable<TopDoctor[]> {
    return this.apiService.get<Result<TopDoctor[]>>(
      `popularity/doctors/location/${locationSuid}?distance=${distance}&doctorLimit=${doctorLimit}`)
    .pipe(
      map((res: Result<TopDoctor[]>) => res.value));
  }
};
