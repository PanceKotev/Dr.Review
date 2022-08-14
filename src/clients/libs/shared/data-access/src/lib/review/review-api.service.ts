import { ApiService } from './../base/api.service';
import { Injectable } from '@angular/core';
import { CreateReviewRequest, GetReviewsDto } from '../models/review';
import { map, Observable } from 'rxjs';
import { Result } from '../models/common/result';

@Injectable({
  providedIn: 'root'
})
export class ReviewApiService {

  public constructor(private apiService: ApiService) {
  }

  public addNewReview(request: CreateReviewRequest): Observable<Result<void>>{
    return this.apiService.post<Result<void>>("v1/reviews", request);
  }

  public getReviewsForDoctor(revieweeSuid: string, userSuid: string): Observable<GetReviewsDto[]> {
    return this.apiService.get<Result<GetReviewsDto[]>>(`v1/reviews?reviewerSuid=${userSuid}&revieweeSuid=${revieweeSuid}`)
                  .pipe(map((res: Result<GetReviewsDto[]>) => res.value));
  }
}
