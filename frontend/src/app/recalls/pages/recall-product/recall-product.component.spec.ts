import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecallProductComponent } from './recall-product.component';

describe('RecallProductComponent', () => {
  let component: RecallProductComponent;
  let fixture: ComponentFixture<RecallProductComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecallProductComponent]
    });
    fixture = TestBed.createComponent(RecallProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
