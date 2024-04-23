import { TestBed } from '@angular/core/testing';

import { StaffService } from './staff.service';
import { HttpClientModule } from '@angular/common/http';

describe('StaffService', () => {
  let service: StaffService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
    });
    service = TestBed.inject(StaffService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
