import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem'; 
import { AuthHelper } from 'src/app/helpers/auth.helper';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit { 

  items: MenuItem[] = [];
  constructor(private authHelper: AuthHelper) { }

  ngOnInit(): void { 
      this.items = [ 
      {
        label: 'FARM MANAGEMENT', styleClass: 'layout-root-menuitem',
        items: [
          { label: 'Animals', icon: 'fa fa-database', url: 'admin/farm/animals' },
          { label: 'Barns', icon: 'fa-solid fa-building', url: '#' },
          { label: 'Vets', icon: 'fa-solid fa-boxes-stacked', url: '#' },
          { label: 'Logout', icon: 'fa-solid fa-arrow-right-from-bracket', command: cmd => this.logout() }
        ]
      }]; 
      
  }
  logout(): void {
    this.authHelper.logout();
  }
}
