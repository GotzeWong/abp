import { Component, OnInit } from '@angular/core';
import { DsPermissionManagementService } from '../services/ds-permission-management.service';

@Component({
  selector: 'lib-ds-permission-management',
  template: ` <p>ds-permission-management works!</p> `,
  styles: [],
})
export class DsPermissionManagementComponent implements OnInit {
  constructor(private service: DsPermissionManagementService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
