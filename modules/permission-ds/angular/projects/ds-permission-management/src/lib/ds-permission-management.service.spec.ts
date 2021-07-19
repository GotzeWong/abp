import { TestBed } from '@angular/core/testing';

import { DsPermissionManagementService } from './ds-permission-management.service';

describe('DsPermissionManagementService', () => {
  let service: DsPermissionManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DsPermissionManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
