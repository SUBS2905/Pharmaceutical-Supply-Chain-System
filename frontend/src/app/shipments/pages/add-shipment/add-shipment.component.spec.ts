import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddShipmentComponent } from './add-shipment.component';

describe('AddShipmentComponent', () => {
  let component: AddShipmentComponent;
  let fixture: ComponentFixture<AddShipmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddShipmentComponent]
    });
    fixture = TestBed.createComponent(AddShipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
