import { ApiService } from './../base/api.service';
import { Injectable } from '@angular/core';
import { CreateReviewRequest, GetReviewsDto, GetReviewSummaryDto, VoteOnReviewRequest } from '../models/review';
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

  public voteOnReview(request: VoteOnReviewRequest): Observable<Result<void>>{
    return this.apiService.post<Result<void>>("v1/reviews/vote", request);
  }

  public getReviewsForDoctor(revieweeSuid: string, userSuid: string): Observable<GetReviewsDto> {
    return this.apiService.get<Result<GetReviewsDto>>(`v1/reviews?reviewerSuid=${userSuid}&revieweeSuid=${revieweeSuid}`)
                  .pipe(map((res: Result<GetReviewsDto>) => res.value));
  }

  public getReviewSummaryForDoctor(revieweeSuid: string) : Observable<GetReviewSummaryDto> {
    return this.apiService.get<Result<GetReviewSummaryDto>>(`v1/reviews/${revieweeSuid}/summary`)
                  .pipe(map((res: Result<GetReviewSummaryDto>) => res.value));
  }
}
