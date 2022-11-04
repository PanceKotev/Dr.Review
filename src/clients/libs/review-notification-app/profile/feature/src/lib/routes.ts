import { Route } from "@angular/router";
import { MsalGuard } from "@azure/msal-angular";
import { AppearanceSettingsComponent } from "./components/appearance-settings/appearance-settings.component";
import { BasicInfoSettingsComponent } from "./components/basic-info-settings/basic-info-settings.component";
import { NotificationSettingsComponent } from "./components/notification-settings/notification-settings.component";
import { ProfileDetailsRootComponent } from "./components/profile-details-root/profile-details-root.component";

export const routes: Route[] = [
  {
    path: '',
    canActivate: [MsalGuard],
    component: ProfileDetailsRootComponent,
    children: [
      {
        path: 'basic',
        component: BasicInfoSettingsComponent
      },
      {
        path: 'appearance',
        component: AppearanceSettingsComponent
      },
      {
        path: 'notifications',
        component: NotificationSettingsComponent
      },
      {
        path: '**',
        redirectTo: 'basic'
      }
    ]
  },
  {
    path: '**',
    redirectTo: ''
  }
];
