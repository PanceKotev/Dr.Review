export interface SubscribeToDoctorsScheduleRequest {
  readonly doctorSuid: string;

  readonly rangeFrom: string | undefined | null;

  readonly rangeTo: string | undefined | null;
};


export interface UpdateSubscriptionRangeRequest {
  readonly scheduleSuid: string;

  readonly rangeFrom: string | undefined | null;

  readonly rangeTo: string | undefined | null;
};
