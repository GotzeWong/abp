import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DsPermissionManagementService {
  apiName = 'DsPermissionManagement';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DsPermissionManagement/sample' },
      { apiName: this.apiName }
    );
  }
}
