import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StaffFilter } from 'src/app/models/staff-filter/staff-filter';
import { Staff } from 'src/app/models/staff/staff';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StaffService {
  baseURL = environment.apiUrl;
  constructor(
    private http: HttpClient
  ) { }

  postStaff(staff: Staff) {
    return this.http.post(`${this.baseURL}/staff`, staff);
  }

  putStaff(id: number, staff: Staff) {
    return this.http.put(`${this.baseURL}/staff/${id}`, staff);
  }

  getStaffByCriteria(filter: StaffFilter) {
    return this.http.post(`${this.baseURL}/staff/by-criteria`, filter);
  }

  getStaffById(id: number) {
    return this.http.get(`${this.baseURL}/staff/${id}`);
  }
  deleteStaffById(id: number) {
    return this.http.delete(`${this.baseURL}/staff/${id}`);
  }
}
