import { ApiService } from './../base/api.service';
import { Injectable } from '@angular/core';
import { CreateReviewRequest } from '../models/review';
import { Observable } from 'rxjs';
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
}
