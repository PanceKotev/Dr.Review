import { Route } from "@angular/router";
import { DoctorDetailsComponent } from "./components/doctor-details/doctor-details.component";
import { DoctorsRootComponent } from "./components/doctors-root/doctors-root.component";

export const routes: Route[] = [
  {path: 'filter/:filterType/:filterValue',  component: DoctorsRootComponent},
  {path: 'filter/:filterType',  component: DoctorsRootComponent},
  {path: '', component: DoctorsRootComponent},
  {path: '*', redirectTo: ''},
  {path: ":doctorSuid", component: DoctorDetailsComponent}
];
