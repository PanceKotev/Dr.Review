import { ReviewNotificationAppDoctorUiModule } from '@drreview/review-notification-app/doctor/ui';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SharedUiSearchModule } from '@drreview/shared/ui/search';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomepageComponent } from './components/homepage/homepage.component';
import { homeRoutes } from './routes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(homeRoutes),
    SharedUiSearchModule,
    MatFormFieldModule,
    ReviewNotificationAppDoctorUiModule],
  declarations: [HomepageComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppHomeFeatureModule {}
