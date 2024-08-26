export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api',
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
};
