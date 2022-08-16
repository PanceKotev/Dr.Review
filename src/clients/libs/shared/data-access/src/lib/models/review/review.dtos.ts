export interface GetReviewsDto {
  readonly reviews: GetReviewDto[];
  readonly currentUserReview: GetReviewDto | undefined;
}

export interface GetReviewDto {
  readonly suid: string;

  readonly reviewerName: string;

  readonly revieweeName: string;

  readonly lastUpdatedOn: Date;

  readonly comment: string | undefined;

  readonly score: number;

  readonly upvotes: number;

  readonly downvotes: number;
}

export interface GetReviewSummaryDto {
  readonly rating: number;

  readonly reviewCountByStar : {[key: number] : number};
}
