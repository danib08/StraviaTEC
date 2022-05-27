import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyChallengeComponent } from './modify-challenge.component';

describe('ModifyChallengeComponent', () => {
  let component: ModifyChallengeComponent;
  let fixture: ComponentFixture<ModifyChallengeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModifyChallengeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModifyChallengeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
