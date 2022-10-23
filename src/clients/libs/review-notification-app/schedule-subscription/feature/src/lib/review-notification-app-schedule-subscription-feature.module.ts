import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleSubscriptionsRootComponent } from './components/schedule-subscriptions-root/schedule-subscriptions-root.component';
import { RouterModule } from '@angular/router';
import { routes } from './routes';
import { ReviewNotificationAppScheduleSubscriptionUiModule } from '@drreview/review-notification-app/schedule-subscription/ui';
import { SharedUiFiltersModule } from '@drreview/shared/ui/filters';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateNewScheduleSubscriptionDialogComponent } from
  './components/create-new-schedule-subscription-dialog/create-new-schedule-subscription-dialog.component';
import { SharedUiDialogModule } from '@drreview/shared/ui/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatCheckboxModule,
    ReviewNotificationAppScheduleSubscriptionUiModule,
    SharedUiFiltersModule,
    SharedUiDialogModule,
    MatPaginatorModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatIconModule,
    MatExpansionModule
  ],
  declarations: [
    ScheduleSubscriptionsRootComponent,
    CreateNewScheduleSubscriptionDialogComponent
  ],
  exports: [RouterModule]
})
export class ReviewNotificationAppScheduleSubscriptionFeatureModule {}
