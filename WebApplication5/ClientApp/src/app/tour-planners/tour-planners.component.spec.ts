import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TourPlannersComponent } from './tour-planners.component';

describe('TourPlannersComponent', () => {
  let component: TourPlannersComponent;
  let fixture: ComponentFixture<TourPlannersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourPlannersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourPlannersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
