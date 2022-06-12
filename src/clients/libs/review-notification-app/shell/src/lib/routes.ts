import {Route} from '@angular/router';
export const mainRoutes: Route[] = [
  {
    path: '',
    loadChildren: async() =>
    (await import('@drreview/review-notification-app/home/feature')).ReviewNotificationAppHomeFeatureModule
  }
];
