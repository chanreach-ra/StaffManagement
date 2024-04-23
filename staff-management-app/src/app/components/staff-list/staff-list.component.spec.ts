import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffListComponent } from './staff-list.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';

describe('StaffListComponent', () => {
  let component: StaffListComponent;
  let fixture: ComponentFixture<StaffListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffListComponent],
      imports: [
        ReactiveFormsModule,
        FormsModule,
        HttpClientModule,
        MatSelectModule
      ],
      providers:[
        DatePipe
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(StaffListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
