import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DailySpendingsComponent } from './daily-spendings.component';

describe('DailySpendingsComponent', () => {
  let component: DailySpendingsComponent;
  let fixture: ComponentFixture<DailySpendingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DailySpendingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DailySpendingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
