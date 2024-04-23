import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import saveAs from 'file-saver';
import { StaffFilter } from 'src/app/models/staff-filter/staff-filter';
import { Staff } from 'src/app/models/staff/staff';
import { StaffService } from 'src/app/services/staff/staff.service';
import Swal from "sweetalert2";
import * as XLSX from 'xlsx';
import { jsPDF } from 'jspdf';

@Component({
  selector: 'app-staff-list',
  templateUrl: './staff-list.component.html',
  styleUrls: ['./staff-list.component.scss']
})
export class StaffListComponent {
  searchForm: FormGroup;
  displayedColumns: string[] = ['staffId', 'fullName', 'gender', 'birthday', 'createddate', 'actions'];
  staffList: Staff[] = [];

  constructor(
    private staff: StaffService,
    private fb: FormBuilder,
    private datePipe: DatePipe
  ) {
    this.searchForm = this.fb.group({
      staffID: [''],
      gender: [0],
      fromDate: [null],
      toDate: [null]
    });
    this.getStaffs(this.searchForm.value);
  }

  search(): void {
    const st = this.searchForm.value as StaffFilter;
    const dates = this.range.value;
    st.fromDate = this.datePipe.transform(dates.start, 'yyyy-MM-dd', 'UTC+7');
    st.toDate = this.datePipe.transform(dates.end, 'yyyy-MM-dd', 'UTC+7');
    console.log(st);

    this.getStaffs(st)
  }
  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  exportFile() {

  }

  getStaffs(filter: StaffFilter) {
    this.staff.getStaffByCriteria(filter)
      .subscribe((res: any) => {
        let result = res.data as Array<Staff>;
        this.staffList = result;
      })
  }

  deleteStaff(id: number) {
    this.staff.deleteStaffById(id)
      .subscribe(() => {
        setTimeout(() => {
          Swal.fire({
            animation: false,
            backdrop: false,
            allowOutsideClick: false,
            showCloseButton: true,
            icon: 'success',
            title: 'SUCCESS',
            text: 'Staff deleted successfully!'
          })
            .then(() => {
              this.getStaffs(this.searchForm.value);
            });
        }, 100);
      })
  }

  exportToPDF(): void {
    const doc = new jsPDF();
    // Get page dimensions (assuming default portrait settings)
    const pageWidth = doc.internal.pageSize.getWidth();

    // Calculate center position
    const text = "Staff Data";
    const textWidth = doc.getTextDimensions(text).w;  // Get text width
    const centerX = (pageWidth - textWidth) / 2;

    // Add the centered text
    doc.text(text, centerX, 10);
    // Table data - Adjust to your actual data
    let data = [
      { StaffID: "", FullName: "", Gender: "", Birthday: "" }
    ];

    data = [];
    this.staffList.forEach(element => {
      data.push({
        StaffID: element.staffID,
        FullName: element.fullName,
        Gender: element.genders,
        Birthday: this.datePipe.transform(element.birthday, 'yyyy-MMM-dd', 'UTC+7')!
      });
    })

    const config = {
      autoSize: true,
      margins: 20,
      fontSize: 12,
      headerBackgroundColor: '#e0e0e0',
      headerTextColor: '#000000'
    };
    const headers = ["StaffID", "FullName", "Gender", "Birthday"];
    doc.table(10, 20, data, headers, config);
    doc.save("staff_data.pdf");
  }

  exportList: any[] = [];
  exportToExcel(): void {
    this.exportList = []
    this.staffList.forEach(element => {
      this.exportList.push({
        StaffID: element.staffID,
        FullName: element.fullName,
        Gender: element.genders,
        BirthDay: this.datePipe.transform(element.birthday, 'yyyy-MMM-dd', 'UTC+7'),
      })
    });
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.exportList);
    const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const excelBlob: Blob = new Blob([excelBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8' });
    saveAs(excelBlob, 'staff-list.xlsx');
  }

}
