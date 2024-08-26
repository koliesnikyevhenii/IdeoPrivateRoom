// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
   
    msalConfig: {
      auth: {
        clientId: '7fbf6fd3-834a-4c22-8b46-4e804d30d2f8',
        authority: 'https://login.microsoftonline.com/dc44b20e-b6ce-46ee-a913-49ae62ecb2d6',
      },
    },
    apiConfig: {
      scopes: ['user.read', 'openid', 'profile'],
      uri: 'https://graph.microsoft.com/v1.0/me',
    },
  production: false,
  apiUrl: 'https://localhost:5001/api'
};
  
  /*
   * For easier debugging in development mode, you can import the following file
   * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
   *
   * This import should be commented out in production mode because it will have a negative impact
   * on performance if an error is thrown.
   */
  // import 'zone.js/plugins/zone-error';  // Included with Angular CLI.