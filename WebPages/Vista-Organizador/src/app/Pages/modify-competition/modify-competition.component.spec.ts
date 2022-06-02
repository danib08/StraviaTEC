import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyCompetitionComponent } from './modify-competition.component';

describe('ModifyCompetitionComponent', () => {
  let component: ModifyCompetitionComponent;
  let fixture: ComponentFixture<ModifyCompetitionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModifyCompetitionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModifyCompetitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
