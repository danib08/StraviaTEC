import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AthletesSearchComponent } from './athletes-search.component';

describe('AthletesSearchComponent', () => {
  let component: AthletesSearchComponent;
  let fixture: ComponentFixture<AthletesSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AthletesSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AthletesSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
