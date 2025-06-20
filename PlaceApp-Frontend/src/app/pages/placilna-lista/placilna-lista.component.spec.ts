import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlacilnaListaComponent } from './placilna-lista.component';

describe('PlacilnaListaComponent', () => {
  let component: PlacilnaListaComponent;
  let fixture: ComponentFixture<PlacilnaListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PlacilnaListaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlacilnaListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
