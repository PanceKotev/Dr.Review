import { GetScheduleSubscriptionDto } from "./schedule-subscription.dto";

export interface SearchDoctorDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly institution: string;
  readonly specialization: string;
}

export interface GetDoctorDetailsDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly institution: string;
  readonly specialization: string;
  readonly location: string;
};

export interface GetDoctorsDto {
  readonly totalCount: number;
  readonly doctors: GetDoctorDto[];
}

export interface GetDoctorDto {
  readonly suid: string;
  readonly firstName: string;
  readonly lastName: string;
  readonly institution: string;
  readonly specialization: string;
  readonly scheduleSubscription: GetScheduleSubscriptionDto | null;
}
