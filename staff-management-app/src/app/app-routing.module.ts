import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditStaffComponent } from './components/edit-staff/edit-staff.component';
import { AddStaffComponent } from './components/add-staff/add-staff.component';
import { StaffListComponent } from './components/staff-list/staff-list.component';
import { Page404Component } from './components/page404/page404/page404.component';

const routes: Routes = [
  { path: '', component: StaffListComponent },
  { path: 'add', component: AddStaffComponent },
  { path: 'edit/:id', component: EditStaffComponent },
  { path: '**', component: Page404Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
