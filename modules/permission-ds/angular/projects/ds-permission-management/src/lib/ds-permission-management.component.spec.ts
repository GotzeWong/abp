import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DsPermissionManagementComponent } from './ds-permission-management.component';

describe('DsPermissionManagementComponent', () => {
  let component: DsPermissionManagementComponent;
  let fixture: ComponentFixture<DsPermissionManagementComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DsPermissionManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DsPermissionManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
