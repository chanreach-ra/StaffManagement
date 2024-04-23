import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StaffService } from 'src/app/services/staff/staff.service';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';
import { Staff } from 'src/app/models/staff/staff';

@Component({
  selector: 'app-edit-staff',
  templateUrl: './edit-staff.component.html',
  styleUrls: ['./edit-staff.component.scss']
})
export class EditStaffComponent implements OnInit {
  id?: any;
  staffs?: Staff;
  staffForm?: FormGroup;
  constructor(
    private fb: FormBuilder,
    private staff: StaffService,
    private router: Router,
    private datePipe: DatePipe,
    private route: ActivatedRoute
  ) {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      this.id = id
      console.log('Id: ', id); // Output the id parameter to the console or use it as needed
    });
    this.staff.getStaffById(this.id)
      .subscribe((res: any) => {
        let result = res.data as Staff;
        this.staffs = result;

        this.staffForm = this.fb.group({
          staffID: [result.staffID, Validators.required],
          fullName: [result.fullName, Validators.required],
          gender: [result.gender, Validators.required],
          birthday: [result.birthday, Validators.required]
        });
      })
  }

  ngOnInit(): void {

  }

  onSubmit(): void {
    const date = this.staffForm?.get('birthday')?.value as Date;
    this.staffForm?.get('birthday')?.patchValue(this.datePipe.transform(date, 'yyyy-MM-dd', 'UTC+7'))

    console.log(this.staffForm);

    this.staff.putStaff(this.id, this.staffForm?.value)
      .subscribe(() => {
        setTimeout(() => {
          Swal.fire({
            animation: false,
            backdrop: false,
            allowOutsideClick: false,
            icon: 'success',
            title: 'SUCCESS',
            text: 'Staff edited successfully!',
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
