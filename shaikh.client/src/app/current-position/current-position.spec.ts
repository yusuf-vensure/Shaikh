import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CurrentPosition } from './current-position';

describe('CurrentPosition', () => {
  let component: CurrentPosition;
  let fixture: ComponentFixture<CurrentPosition>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CurrentPosition],
    }).compileComponents();

    fixture = TestBed.createComponent(CurrentPosition);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
