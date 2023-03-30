/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CourceService } from './cource.service';

describe('Service: Cource', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CourceService]
    });
  });

  it('should ...', inject([CourceService], (service: CourceService) => {
    expect(service).toBeTruthy();
  }));
});
