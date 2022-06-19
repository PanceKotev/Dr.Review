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
    MatFormFieldModule],
  declarations: [HomepageComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppHomeFeatureModule {}
