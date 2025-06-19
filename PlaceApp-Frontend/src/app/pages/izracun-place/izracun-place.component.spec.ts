import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IzracunPlaceComponent } from './izracun-place.component';

describe('IzracunPlaceComponent', () => {
  let component: IzracunPlaceComponent;
  let fixture: ComponentFixture<IzracunPlaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IzracunPlaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IzracunPlaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
