import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileDetailsRootComponent } from './components/profile-details-root/profile-details-root.component';
import { RouterModule } from '@angular/router';
import { routes } from './routes';
import { AppearanceSettingsComponent } from './components/appearance-settings/appearance-settings.component';
import { BasicInfoSettingsComponent } from './components/basic-info-settings/basic-info-settings.component';
import { MatIconModule } from '@angular/material/icon';
import { NotificationSettingsComponent } from './components/notification-settings/notification-settings.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SharedUiAvatarModule } from '@drreview/shared/ui/avatar';

@NgModule({
  imports: [
    CommonModule,
    MatListModule,
    MatIconModule,
    MatCheckboxModule,
    MatInputModule,
    MatFormFieldModule,
    SharedUiAvatarModule,
    MatSlideToggleModule,
    MatButtonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    ProfileDetailsRootComponent,
    AppearanceSettingsComponent,
    BasicInfoSettingsComponent,
    NotificationSettingsComponent
  ],
  exports: [RouterModule]
})
export class ReviewNotificationAppProfileFeatureModule {}
