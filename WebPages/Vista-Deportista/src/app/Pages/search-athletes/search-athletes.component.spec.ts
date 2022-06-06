import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchAthletesComponent } from './search-athletes.component';

describe('SearchAthletesComponent', () => {
  let component: SearchAthletesComponent;
  let fixture: ComponentFixture<SearchAthletesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchAthletesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchAthletesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
