export interface CreateReviewRequest{
  revieweeSuid: string;
  comment?: string;
  score: number;
  anonymous: boolean;
}

export interface UpdateReviewRequest{
  reviewSuid: string;
  comment?: string;
  score: number;
  anonymous: boolean;
}

export interface VoteOnReviewRequest{
  reviewSuid: string;
  vote: boolean | undefined;
}
