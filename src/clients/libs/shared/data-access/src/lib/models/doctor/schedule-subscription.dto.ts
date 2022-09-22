export interface GetScheduleSubscriptionDto {
  readonly suid: string;
  range: ScheduleNotificationRange;
}

export interface ScheduleNotificationRange {
  from: string | undefined | null;
  to: string | undefined | null;
  subscribedTo: boolean;
}
