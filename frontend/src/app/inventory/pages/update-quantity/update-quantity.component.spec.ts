import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateQuantityComponent } from './update-quantity.component';

describe('UpdateQuantityComponent', () => {
  let component: UpdateQuantityComponent;
  let fixture: ComponentFixture<UpdateQuantityComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateQuantityComponent]
    });
    fixture = TestBed.createComponent(UpdateQuantityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
