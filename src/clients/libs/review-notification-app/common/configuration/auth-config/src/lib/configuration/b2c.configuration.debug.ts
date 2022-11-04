import { BrowserCacheLocation, LogLevel } from "@azure/msal-browser";
const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;

export const b2cConfiguration = {
  auth: {
    clientId: '60f939e5-439b-4bd9-b747-b8b54870e359',
    authority: 'https://drreview.b2clogin.com/drreview.onmicrosoft.com/B2C_1A_SIGNUP_SIGNIN',
    knownAuthorities: ['drreview.b2clogin.com'],
    redirectUri: '/',
    postLogoutRedirectUri: '/',
    navigateToLoginRequestUrl: true
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage,
    storeAuthStateInCookie: isIE
  },
  system: {
    loggerOptions: {
        loggerCallback(logLevel: LogLevel, message: string): void {
            console.log(message);
        },
        logLevel: LogLevel.Error,
        piiLoggingEnabled: false
    }
  }
};

export const b2cApiScopes = [
  'https://drreview.onmicrosoft.com/drreview_api/drreview.write',
  'https://drreview.onmicrosoft.com/drreview_api/drreview.read'];

export const baseApi = "http://localhost:4300/api/";

export const b2cScopes = {
  scopes: ['openid', "profile", 'offline_access', ...b2cApiScopes]
};
