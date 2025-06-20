import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NastavitveComponent } from './nastavitve.component';

describe('NastavitveComponent', () => {
  let component: NastavitveComponent;
  let fixture: ComponentFixture<NastavitveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NastavitveComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NastavitveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
