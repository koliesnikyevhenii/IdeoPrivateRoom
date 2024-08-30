import {  
  MsalInterceptor,
  MSAL_INSTANCE,
  MsalInterceptorConfiguration,
  MsalGuardConfiguration,
  MSAL_GUARD_CONFIG,
  MSAL_INTERCEPTOR_CONFIG,
  MsalService,
  MsalGuard,
  MsalBroadcastService } from '@azure/msal-angular';

import { 
  PublicClientApplication, 
  IPublicClientApplication,
  InteractionType,
  BrowserCacheLocation,
  LogLevel} from '@azure/msal-browser';

import { environment } from '../environments/environment';


export function initializeMsal(msalService: MsalService) {
  return () => {
    const accounts = msalService.instance.getAllAccounts();
    if (accounts.length > 0) {
      msalService.instance.setActiveAccount(accounts[0]);
    }
  };
}

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.msalConfig.auth.clientId,
      authority: environment.msalConfig.auth.authority,
      redirectUri: '/calendar',
      postLogoutRedirectUri: '/login',
      navigateToLoginRequestUrl: true

    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
     // storeAuthStateInCookie: true
    },
    system: {
      allowNativeBroker: false, // Disables WAM Broker
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
      },
    },
  });
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set(
    environment.apiConfig.uri,
    environment.apiConfig.scopes
  );


  protectedResourceMap.set(
    environment.apiConfig.backuri,
    environment.apiConfig.backscopes
  );

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [...environment.apiConfig.scopes],
    },
    loginFailedRoute: '/login'
  };
}


export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}