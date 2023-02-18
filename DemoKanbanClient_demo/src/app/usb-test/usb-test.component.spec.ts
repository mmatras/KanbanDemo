import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsbTestComponent } from './usb-test.component';

describe('UsbTestComponent', () => {
  let component: UsbTestComponent;
  let fixture: ComponentFixture<UsbTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsbTestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsbTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
