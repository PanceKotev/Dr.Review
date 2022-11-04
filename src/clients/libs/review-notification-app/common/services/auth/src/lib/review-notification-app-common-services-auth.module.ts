import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MsalModule } from '@azure/msal-angular';

@NgModule({
  imports: [
    CommonModule,
    MsalModule]
})
export class ReviewNotificationAppCommonServicesAuthModule {}
