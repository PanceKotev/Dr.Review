import { UpdateUserRequest } from './../models/user/user.requests';
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { ApiService } from "../base/api.service";
import { Result } from "../models/common";
import { CurrentUserDetailsDto } from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  public constructor(private apiService: ApiService) {
  }

  public getUserDetails(): Observable<CurrentUserDetailsDto> {
    return this.apiService.get<Result<CurrentUserDetailsDto>>(`v1/users/me`)
                  .pipe(map((res: Result<CurrentUserDetailsDto>) => res.value));
  }

  public updateUser(request: UpdateUserRequest): Observable<Result<void>> {
    return this.apiService.put<Result<void>>(`v1/users/update`, request);
  }
}
