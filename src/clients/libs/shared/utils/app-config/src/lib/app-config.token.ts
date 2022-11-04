import { InjectionToken, ValueProvider } from '@angular/core';
import { AppConfig } from './app-config.abstract';

export const APP_CONFIG = new InjectionToken<AppConfig>(
  'drreview.angular.config'
);

export const appConfigProvider = (value: AppConfig): ValueProvider => ({
  provide: APP_CONFIG,
  useValue: value
});
