import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectionDefOverviewComponent } from './selection-def-overview.component';

describe('SelectionDefOverviewComponent', () => {
  let component: SelectionDefOverviewComponent;
  let fixture: ComponentFixture<SelectionDefOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectionDefOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectionDefOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
