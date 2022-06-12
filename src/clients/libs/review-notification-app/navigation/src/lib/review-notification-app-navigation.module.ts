import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopbarComponent } from './components/topbar/topbar.component';
import { MatIconModule } from '@angular/material/icon';
import { SharedUiThemesModule } from '@drreview/shared/ui/themes';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    SharedUiThemesModule
  ],
  declarations: [TopbarComponent],
  exports: [TopbarComponent]
})
export class ReviewNotificationAppNavigationModule {}
