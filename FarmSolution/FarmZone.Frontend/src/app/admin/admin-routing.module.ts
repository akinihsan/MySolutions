import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from '../helpers/auth-guard.service';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminLayoutComponent } from './layout/admin-layout/admin-layout.component';
import { AnimalEditComponent } from './farm/animal-edit/animal-edit.component';
 import { AnimalListComponent } from './farm/animal-list/animal-list.component';



const routes: Routes = [
  {
    path: "",
    component: AdminLayoutComponent,
    children: [
      {
        path: "admin/home",
        component: AdminHomeComponent , canActivate: [AuthGuardService]
      }, 
      {
        path: "admin/farm/animals",  canActivate: [AuthGuardService],
        component: AnimalListComponent
      },
      {
        path: "admin/farm/animal-edit",canActivate: [AuthGuardService],
        component: AnimalEditComponent
      }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
