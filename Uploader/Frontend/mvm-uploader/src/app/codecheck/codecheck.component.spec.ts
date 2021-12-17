import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodecheckComponent } from './codecheck.component';

describe('CodecheckComponent', () => {
  let component: CodecheckComponent;
  let fixture: ComponentFixture<CodecheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CodecheckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CodecheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
