import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZaposleniListComponent } from './zaposleni-list.component';

describe('ZaposleniListComponent', () => {
  let component: ZaposleniListComponent;
  let fixture: ComponentFixture<ZaposleniListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ZaposleniListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ZaposleniListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
