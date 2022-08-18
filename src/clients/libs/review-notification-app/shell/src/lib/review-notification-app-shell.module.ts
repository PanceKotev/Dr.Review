import { ReviewNotificationAppCommonServicesAuthModule } from '@drreview/review-notification-app/common/services/auth';
import { ReviewNotificationAppCommonConfigurationAuthConfigModule }
from '@drreview/review-notification-app/common/configuration/auth-config';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { mainRoutes } from './routes';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { SharedDataAccessModule } from '@drreview/shared/data-access';
import { MainNavigationComponent } from './components/main-navigation/main-navigation.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { SharedUiThemesModule } from '@drreview/shared/ui/themes';
import { ReviewNotificationAppNavigationModule } from '@drreview/review-notification-app/navigation';
import { SharedServicesThemesModule } from '@drreview/shared/services/themes';
import { RouterModule } from '@angular/router';
import { QuillModule } from 'ngx-quill';
import { StarRatingModule } from 'angular-star-rating';
import { MatSnackBarConfig, MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(mainRoutes),
    BrowserAnimationsModule,
    QuillModule.forRoot({}),
    StarRatingModule.forRoot(),
    ReviewNotificationAppCommonConfigurationAuthConfigModule,
    ReviewNotificationAppCommonServicesAuthModule,
    SharedDataAccessModule,
    MatSidenavModule,
    MatSnackBarModule,
    SharedUiThemesModule,
    SharedServicesThemesModule,
    ReviewNotificationAppNavigationModule],
  declarations: [MainLayoutComponent, MainNavigationComponent],
  exports: [
    ReviewNotificationAppCommonConfigurationAuthConfigModule,
    ReviewNotificationAppCommonServicesAuthModule,
    SharedDataAccessModule,
    MainLayoutComponent,
    RouterModule
    ],
  providers: [
    {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 1000}}
  ]
})
export class ReviewNotificationAppShellModule {}
