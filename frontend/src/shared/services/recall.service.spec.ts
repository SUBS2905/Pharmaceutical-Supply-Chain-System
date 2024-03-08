import { TestBed } from '@angular/core/testing';

import { RecallService } from './recall.service';

describe('RecallService', () => {
  let service: RecallService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecallService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
