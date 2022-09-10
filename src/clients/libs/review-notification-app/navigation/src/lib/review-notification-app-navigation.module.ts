import { HttpClientModule } from '@angular/common/http';

import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TopbarComponent } from './components/topbar/topbar.component';
import { MatIconModule } from '@angular/material/icon';
import { SharedUiThemesModule } from '@drreview/shared/ui/themes';
import { BottomMobileNavigationComponent } from './components/bottom-mobile-navigation/bottom-mobile-navigation.component';
import { UserDropdownComponent } from './components/user-dropdown/user-dropdown.component';
import { SharedUiAvatarModule } from '@drreview/shared/ui/avatar';
import {MatMenuModule} from '@angular/material/menu';

@NgModule({
  imports: [
    CommonModule,
    MatToolbarModule,
    MatMenuModule,
    RouterModule,
    HttpClientModule,
    MatIconModule,
    MatButtonModule,
    SharedUiAvatarModule,
    SharedUiThemesModule
  ],
  declarations: [
    TopbarComponent,
    BottomMobileNavigationComponent,
    UserDropdownComponent
  ],
  exports: [TopbarComponent, BottomMobileNavigationComponent]
})
export class ReviewNotificationAppNavigationModule {}
