import { TestBed } from '@angular/core/testing';

import { SelectionDefinitionService } from './selection-definition.service';

describe('SelectionDefinitionService', () => {
  let service: SelectionDefinitionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SelectionDefinitionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
