import {Route} from '@angular/router';
import { NotFoundComponent, ErrorPageComponent } from '@drreview/review-notification-app/common/ui/routing-pages';
export const mainRoutes: Route[] = [
  {
    path: '',
    loadChildren: async() =>
    (await import('@drreview/review-notification-app/home/feature')).ReviewNotificationAppHomeFeatureModule
  },
  {
    path: 'notfound',
    component: NotFoundComponent
  },
  {
    path: 'error',
    component: ErrorPageComponent
  }
];
