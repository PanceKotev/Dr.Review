import { Injectable } from "@angular/core";
import { GetDoctorsDto } from "@drreview/shared/data-access";
import { DoctorsStore } from "./doctors.store";

@Injectable({
  providedIn: 'root'
})
export class DoctorsFacade {
  public constructor(private doctorsStore: DoctorsStore) {}


  public setDoctors(doctor: GetDoctorsDto): void {
    this.doctorsStore.setLoading(true);
    this.doctorsStore.set(doctor.doctors.map(e => ({
      suid: e.suid,
      firstName: e.firstName,
      lastName: e.lastName,
      specialization: e.specialization,
      location: '',
      institution: e.institution,
      scheduleSubscription: e.scheduleSubscription
    })));
    this.doctorsStore.setLoading(true);
  }
}
