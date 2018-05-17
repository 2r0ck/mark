import { TestBed, inject } from '@angular/core/testing';

import { TestSerService } from './test-ser.service';

describe('TestSerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TestSerService]
    });
  });

  it('should be created', inject([TestSerService], (service: TestSerService) => {
    expect(service).toBeTruthy();
  }));
});
