import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvidencaDelaComponent } from './evidenca-dela.component';

describe('EvidencaDelaComponent', () => {
  let component: EvidencaDelaComponent;
  let fixture: ComponentFixture<EvidencaDelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EvidencaDelaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EvidencaDelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
