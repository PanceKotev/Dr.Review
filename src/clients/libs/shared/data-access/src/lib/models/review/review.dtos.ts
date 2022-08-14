export interface GetReviewsDto {
  readonly suid: string;

  readonly reviewerName: string;

  readonly revieweeName: string;

  readonly lastUpdatedOn: Date;

  readonly comment: string | undefined;

  readonly score: number;

  readonly upvotes: number;

  readonly downvotes: number;
}
