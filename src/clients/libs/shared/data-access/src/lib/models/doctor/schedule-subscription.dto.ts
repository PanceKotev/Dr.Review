export interface GetScheduleSubscriptionDto {
  readonly suid: string;
  range: ScheduleNotificationRange;
  doctor: GetScheduleSubscriptionDoctorDto;
}

export interface GetScheduleSubscriptionDoctorDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly location: string;
  readonly institution: string;
  readonly specialization: string;
}
export interface GetScheduleSubscriptionsDto {
  readonly totalCount: number;
  subscriptions: GetScheduleSubscriptionDto[];
}

export interface ScheduleNotificationRangeString {
  from: string | undefined | null;
  to: string | undefined | null;
  subscribedTo: boolean | null;
}
export interface ScheduleNotificationRange {
  from: Date  | null;
  to: Date  | null;
  subscribedTo: boolean | null;
}
