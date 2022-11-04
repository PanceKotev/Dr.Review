import { ScheduleSubscriptionsRootComponent } from './components/schedule-subscriptions-root/schedule-subscriptions-root.component';
import { Route } from "@angular/router";

export const routes: Route[] = [
  {path: 'filter/:filterType/:filterValue',  component: ScheduleSubscriptionsRootComponent},
  {path: 'filter/:filterType',  component: ScheduleSubscriptionsRootComponent},
  { path: '', component: ScheduleSubscriptionsRootComponent },
  {path: '*', redirectTo: ''}
];
