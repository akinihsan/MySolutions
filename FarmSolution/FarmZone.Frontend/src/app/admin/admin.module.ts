import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminLayoutComponent } from './layout/admin-layout/admin-layout.component';
import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MenuComponent } from './layout/menu/menu.component';
import { TopBarComponent } from './layout/top-bar/top-bar.component';
import { FooterComponent } from './layout/footer/footer.component';
import { BrowserModule } from '@angular/platform-browser';
import { AuthGuardService } from '../helpers/auth-guard.service';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AnimalListComponent } from './farm/animal-list/animal-list.component';
import { AnimalEditComponent } from './farm/animal-edit/animal-edit.component'; 


@NgModule({
  declarations: [
    AdminLayoutComponent, MenuComponent, TopBarComponent, FooterComponent, AnimalListComponent, AnimalEditComponent,
  ],
  imports: [
    BrowserModule, CommonModule, AdminRoutingModule, SharedModule
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [AuthGuardService],
  entryComponents: [
    AdminHomeComponent
  ]
})
export class AdminModule { }
