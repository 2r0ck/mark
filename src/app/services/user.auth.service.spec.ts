import { TestBed, inject } from '@angular/core/testing';

import { UserAuthService } from './user.auth.service';

describe('User.AuthService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserAuthService]
    });
  });

  it('should be created', inject([UserAuthService], (service: UserAuthService) => {
    expect(service).toBeTruthy();
  }));
});
