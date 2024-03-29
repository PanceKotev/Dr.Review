import { ScheduleNotificationRange, ScheduleNotificationRangeString } from "./schedule-subscription.dto";

export interface SearchDoctorDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly institution: string;
  readonly specialization: string;
  fullName: string;
}

export interface GetDoctorDetailsDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly institution: string;
  readonly specialization: string;
  readonly location: string;
  readonly scheduleSubscription?: GetDoctorDetailsScheduleSubscriptionDto;
};

export interface GetDoctorDetailsScheduleSubscriptionDto {
  readonly scheduleSuid: string;
  readonly range: ScheduleNotificationRange;
}
export interface GetDoctorsDto {
  readonly totalCount: number;
  readonly doctors: GetDoctorDto[];
}

export interface GetDoctorDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  fullName: string;
  readonly institution: string;
  readonly specialization: string;
  readonly scheduleSubscription: GetDoctorScheduleSubscriptionDto | null;
}
export interface GetDoctorScheduleSubscriptionDto {
  readonly suid: string;
  range: ScheduleNotificationRangeString;
}
export interface TopDoctor {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly institution: string;
  readonly specialization: string;
  readonly location: string;
  readonly distance: number;
}
