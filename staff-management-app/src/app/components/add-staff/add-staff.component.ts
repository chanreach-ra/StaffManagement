import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StaffService } from 'src/app/services/staff/staff.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-add-staff',
  templateUrl: './add-staff.component.html',
  styleUrls: ['./add-staff.component.scss']
})
export class AddStaffComponent implements OnInit {
  staffForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private staff: StaffService,
    private router: Router,
    private datePipe: DatePipe
  ) {
    this.staffForm = this.fb.group({
      staffID: ['', Validators.required],
      fullName: ['', Validators.required],
      gender: [0, Validators.required],
      birthday: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    const date = this.staffForm.get('birthday')?.value as Date;
    this.staffForm.get('birthday')?.patchValue(this.datePipe.transform(date, 'yyyy-MM-dd', 'UTC+7'))

    console.log(this.staffForm);

    this.staff.postStaff(this.staffForm.value)
      .subscribe(() => {
        setTimeout(() => {
          Swal.fire({
            animation: false,
            backdrop: false,
            allowOutsideClick: false,
            icon: 'success',
            title: 'SUCCESS',
            text: 'Staff saved successfully!',
            confirmButtonText: 'GO TO LIST',
            showCloseButton: true
          })
            .then((result) => {
              if (result.isConfirmed) {
                this.router.navigate(['']);
              }
            });
        }, 100);
      })
  }

}
