
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopbarComponent } from './components/topbar/topbar.component';
import { MatIconModule } from '@angular/material/icon';
import { SharedUiThemesModule } from '@drreview/shared/ui/themes';
import { BottomMobileNavigationComponent } from './components/bottom-mobile-navigation/bottom-mobile-navigation.component';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    RouterModule,
    MatIconModule,
    MatButtonModule,
    SharedUiThemesModule
  ],
  declarations: [TopbarComponent, BottomMobileNavigationComponent],
  exports: [TopbarComponent, BottomMobileNavigationComponent]
})
export class ReviewNotificationAppNavigationModule {}
