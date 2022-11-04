import { InteractionType, IPublicClientApplication, PublicClientApplication } from "@azure/msal-browser";
import { b2cApiScopes, b2cConfiguration, b2cScopes, baseApi } from "./b2c.configuration.debug";
import { MsalGuardConfiguration, MsalInterceptorConfiguration, ProtectedResourceScopes } from '@azure/msal-angular';


export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication(b2cConfiguration);
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: b2cScopes.scopes
    }
  };
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, (string | ProtectedResourceScopes)[] | null>([
    [`${baseApi}Location/options*`, null],
    [`${baseApi}Institution/options*`, null],
    [`${baseApi}Specialization/options*`, null],
    [`${baseApi}popularity*`, null],
    [`${baseApi}*`, b2cApiScopes]
  ]);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}
