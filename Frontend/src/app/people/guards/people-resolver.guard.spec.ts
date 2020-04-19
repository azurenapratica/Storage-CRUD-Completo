import { TestBed, async, inject } from '@angular/core/testing';

import { PeopleResolverGuard } from './people-resolver.guard';

describe('PeopleResolverGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PeopleResolverGuard]
    });
  });

  it('should ...', inject([PeopleResolverGuard], (guard: PeopleResolverGuard) => {
    expect(guard).toBeTruthy();
  }));
});
