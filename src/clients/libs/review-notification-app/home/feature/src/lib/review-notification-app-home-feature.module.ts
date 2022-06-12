import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomepageComponent } from './components/homepage/homepage.component';
import { homeRoutes } from './routes';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(homeRoutes)],
  declarations: [HomepageComponent],
  exports: [RouterModule]
})
export class ReviewNotificationAppHomeFeatureModule {}
