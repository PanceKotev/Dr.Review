
export interface UpdateSubscriptionRangeRequest {
  readonly scheduleSuid: string;

  readonly rangeFrom: string | undefined | null;

  readonly rangeTo: string | undefined | null;
};

export interface UpdateSubscriptionsRangeRequest {
  readonly scheduleSuids: string[];

  readonly rangeFrom: string | undefined | null;

  readonly rangeTo: string | undefined | null;
};

export interface SubscribeToMultipleDoctorsSchedulesRequest {
  readonly doctorSuids: string[];
  readonly rangeFrom: string | undefined | null;
  readonly rangeTo: string | undefined | null;
};
