import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DsPermissionManagementComponent } from './components/ds-permission-management.component';
import { DsPermissionManagementRoutingModule } from './ds-permission-management-routing.module';

@NgModule({
  declarations: [DsPermissionManagementComponent],
  imports: [CoreModule, ThemeSharedModule, DsPermissionManagementRoutingModule],
  exports: [DsPermissionManagementComponent],
})
export class DsPermissionManagementModule {
  static forChild(): ModuleWithProviders<DsPermissionManagementModule> {
    return {
      ngModule: DsPermissionManagementModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DsPermissionManagementModule> {
    return new LazyModuleFactory(DsPermissionManagementModule.forChild());
  }
}
