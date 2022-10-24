import {Route} from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { NotFoundComponent, ErrorPageComponent } from '@drreview/review-notification-app/common/ui/routing-pages';
export const mainRoutes: Route[] = [
  {
    path: '',
    loadChildren: async() =>
    (await import('@drreview/review-notification-app/home/feature')).ReviewNotificationAppHomeFeatureModule
  },
  {
    path: 'doctors',
    canActivate: [MsalGuard],
    loadChildren: async() =>
    (await import('@drreview/review-notification-app/doctor/feature')).ReviewNotificationAppDoctorFeatureModule
  },
  {
    path: 'subscriptions',
    canActivate: [MsalGuard],
    loadChildren: async() =>
    (await import('@drreview/review-notification-app/schedule-subscription/feature')).ReviewNotificationAppScheduleSubscriptionFeatureModule
  },
  {
    path: 'profile',
    canActivate: [MsalGuard],
    loadChildren: async() =>
    (await import('@drreview/review-notification-app/profile/feature')).ReviewNotificationAppProfileFeatureModule
  },
  {
    path: 'notfound',
    canActivate: [MsalGuard],
    component: NotFoundComponent
  },
  {
    path: 'error',
    component: ErrorPageComponent
  }
];
