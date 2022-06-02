import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptRegistrationComponent } from './accept-registration.component';

describe('AcceptRegistrationComponent', () => {
  let component: AcceptRegistrationComponent;
  let fixture: ComponentFixture<AcceptRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AcceptRegistrationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
