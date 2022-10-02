export interface GetScheduleSubscriptionDto {
  readonly suid: string;
  range: ScheduleNotificationRangeString;
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
