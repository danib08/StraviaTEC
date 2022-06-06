import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetitionPositionsComponent } from './competition-positions.component';

describe('CompetitionPositionsComponent', () => {
  let component: CompetitionPositionsComponent;
  let fixture: ComponentFixture<CompetitionPositionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompetitionPositionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetitionPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
