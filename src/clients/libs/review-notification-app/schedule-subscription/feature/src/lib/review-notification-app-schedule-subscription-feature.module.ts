import { MatCheckboxModule } from '@angular/material/checkbox';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleSubscriptionsRootComponent } from './components/schedule-subscriptions-root/schedule-subscriptions-root.component';
import { RouterModule } from '@angular/router';
import { routes } from './routes';
import { ReviewNotificationAppScheduleSubscriptionUiModule } from '@drreview/review-notification-app/schedule-subscription/ui';
import { SharedUiFiltersModule } from '@drreview/shared/ui/filters';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatCheckboxModule,
    ReviewNotificationAppScheduleSubscriptionUiModule,
    SharedUiFiltersModule,
    MatButtonModule
  ],
  declarations: [ScheduleSubscriptionsRootComponent],
  exports: [
    RouterModule
  ]
})
export class ReviewNotificationAppScheduleSubscriptionFeatureModule {}
