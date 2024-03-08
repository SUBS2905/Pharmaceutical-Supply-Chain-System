import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecallsComponent } from './recalls.component';

describe('RecallsComponent', () => {
  let component: RecallsComponent;
  let fixture: ComponentFixture<RecallsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecallsComponent]
    });
    fixture = TestBed.createComponent(RecallsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
