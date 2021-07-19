import { ModuleWithProviders, NgModule } from '@angular/core';
import { DS_PERMISSION_MANAGEMENT_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DsPermissionManagementConfigModule {
  static forRoot(): ModuleWithProviders<DsPermissionManagementConfigModule> {
    return {
      ngModule: DsPermissionManagementConfigModule,
      providers: [DS_PERMISSION_MANAGEMENT_ROUTE_PROVIDERS],
    };
  }
}
