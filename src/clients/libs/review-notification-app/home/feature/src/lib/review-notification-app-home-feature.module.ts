import { ReviewNotificationAppDoctorUiModule } from '@drreview/review-notification-app/doctor/ui';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SharedUiSearchModule } from '@drreview/shared/ui/search';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomepageComponent } from './components/homepage/homepage.component';
import { homeRoutes } from './routes';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRippleModule } from '@angular/material/core';
import { SharedUiMapsModule } from '@drreview/shared/ui/maps';
import { SharedUiPopoversModule } from '@drreview/shared/ui/popovers';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(homeRoutes),
    FormsModule,
    ReactiveFormsModule,
    SharedUiSearchModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatRippleModule,
    MatFormFieldModule,
    ReviewNotificationAppDoctorUiModule,
    SharedUiPopoversModule,
    SharedUiMapsModule],
  declarations: [HomepageComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppHomeFeatureModule {}
