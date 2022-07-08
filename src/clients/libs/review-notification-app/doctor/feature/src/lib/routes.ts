import { Route } from "@angular/router";
import { DoctorDetailsComponent } from "./components/doctor-details/doctor-details.component";

export const routes: Route[] = [
  {path: ":doctorSuid", component: DoctorDetailsComponent}
];
