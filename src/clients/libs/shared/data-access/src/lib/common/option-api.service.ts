import { map, Observable } from 'rxjs';
import { Injectable } from "@angular/core";
import { ApiService } from "../base/api.service";
import { ILocation, IOptionItemWithLink, Result } from '../models/common';

@Injectable({
  providedIn: 'root'
})
export class OptionApiService {

  public constructor(private apiService: ApiService) {
  }

  public getLocationOptions(): Observable<IOptionItemWithLink<string>[]> {
      return this.apiService.get<Result<ILocation[]>>(`Location/options`)
      .pipe(
        map((res: Result<ILocation[]>) => res.value),
        map((val) => val.map(x =>
          (
            {link: `/doctors/filter/location/${x.name}`, label: x.name, value: x.name})
          )));
    }

  public getInstitutionOptions(): Observable<IOptionItemWithLink<string>[]> {
    return this.apiService.get<Result<string[]>>(`Institution/options`)
    .pipe(
      map((res: Result<string[]>) => res.value),
      map((val) => val.map(x =>
        (
          {link: `/doctors/filter/institution/${x}`, label: x, value: x})
        )));
  }

  public getSpecializationOptions(): Observable<IOptionItemWithLink<string>[]> {
    return this.apiService.get<Result<string[]>>(`Specialization/options`)
    .pipe(
      map((res: Result<string[]>) => res.value),
      map((val) => val.map(x =>
        (
          {link: `/doctors/filter/specialization/${x}`, label: x, value: x})
        )));
  }


}
