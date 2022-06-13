import { Route } from "@angular/router";
import { HomepageComponent } from "./components/homepage/homepage.component";

export const homeRoutes: Route[] = [
  {
    path: '',
    component: HomepageComponent
  },
  {
    path: '*',
    redirectTo: ''
  }
];
