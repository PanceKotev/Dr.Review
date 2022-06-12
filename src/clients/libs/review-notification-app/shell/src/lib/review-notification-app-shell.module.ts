import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { mainRoutes } from './routes';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { SharedDataAccessModule } from '@drreview/shared/data-access';
import { MainNavigationComponent } from './components/main-navigation/main-navigation.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SharedUiThemesModule } from '@drreview/shared/ui/themes';
import { ReviewNotificationAppNavigationModule } from '@drreview/review-notification-app/navigation';
import { SharedServicesThemesModule } from '@drreview/shared/services/themes';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(mainRoutes),
    BrowserAnimationsModule,
    SharedDataAccessModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    SharedUiThemesModule,
    SharedServicesThemesModule,
    ReviewNotificationAppNavigationModule],
  declarations: [MainLayoutComponent, MainNavigationComponent],
  exports: [
    SharedDataAccessModule,
    MainLayoutComponent,
    RouterModule
    ]
})
export class ReviewNotificationAppShellModule {}
