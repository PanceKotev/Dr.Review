import { Route } from "@angular/router";
import { DoctorDetailsComponent } from "./components/doctor-details/doctor-details.component";
import { DoctorsRootComponent } from "./components/doctors-root/doctors-root.component";

export const routes: Route[] = [
  {path: '', component: DoctorsRootComponent},
  {path: 'filters/:filterType/:filterValue',  component: DoctorsRootComponent},
  {path: '*', component: DoctorsRootComponent},
  {path: ":doctorSuid", component: DoctorDetailsComponent}
];
