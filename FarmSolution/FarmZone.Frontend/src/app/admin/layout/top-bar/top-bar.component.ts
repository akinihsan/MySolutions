import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthHelper } from 'src/app/helpers/auth.helper';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss']
})
export class TopBarComponent implements OnInit {
 
  username:string ='';
  constructor(private authHelper: AuthHelper) {
    
    this.username = "John Doe"

  }

  ngOnInit(): void {

  }
 
}
