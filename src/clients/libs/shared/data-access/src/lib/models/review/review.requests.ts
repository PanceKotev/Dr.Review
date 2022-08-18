export interface CreateReviewRequest{
  revieweeSuid: string;
  comment?: string;
  score: number;
}

export interface UpdateReviewRequest{
  reviewSuid: string;
  comment?: string;
  score: number;
}

export interface VoteOnReviewRequest{
  reviewSuid: string;
  vote: boolean | undefined;
}
