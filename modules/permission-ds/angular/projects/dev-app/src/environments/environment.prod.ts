import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'DsPermissionManagement',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44342',
    redirectUri: baseUrl,
    clientId: 'DsPermissionManagement_App',
    responseType: 'code',
    scope: 'offline_access DsPermissionManagement',
  },
  apis: {
    default: {
      url: 'https://localhost:44342',
      rootNamespace: 'DsPermissionManagement',
    },
    DsPermissionManagement: {
      url: 'https://localhost:44307',
      rootNamespace: 'DsPermissionManagement',
    },
  },
} as Environment;
