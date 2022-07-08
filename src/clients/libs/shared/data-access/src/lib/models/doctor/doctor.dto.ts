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
