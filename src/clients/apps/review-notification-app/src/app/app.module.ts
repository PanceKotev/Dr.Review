import { SharedServicesThemesModule } from '@drreview/shared/services/themes';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { APP_CONFIG } from '@drreview/shared/utils/app-config';
import { environment } from '../environments/environment';

import { AppComponent } from './app.component';
import { ReviewNotificationAppShellModule } from '@drreview/review-notification-app/shell';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [AppComponent],
  providers: [{ provide: APP_CONFIG, useValue: environment}],
  imports: [
    BrowserModule,
    ReviewNotificationAppShellModule,
    SharedServicesThemesModule,
    BrowserAnimationsModule],
  bootstrap: [AppComponent]
})
export class AppModule {}
