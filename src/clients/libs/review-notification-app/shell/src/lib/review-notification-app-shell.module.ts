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

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(mainRoutes),
    BrowserAnimationsModule,
    QuillModule.forRoot({}),
    ReviewNotificationAppCommonConfigurationAuthConfigModule,
    ReviewNotificationAppCommonServicesAuthModule,
    SharedDataAccessModule,
    MatSidenavModule,
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

    ]
})
export class ReviewNotificationAppShellModule {}
