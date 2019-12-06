import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  constructor(private accountService: AccountService, private route: Router) {


  }

  Username = this.accountService.CurrentUsername.value;
  UserId = this.accountService.CurrentUserId.value;
  pictureUrl = localStorage.getItem('PictureUrl');
  loading = false
  

  ngOnInit() {
   
  }

  SignOut() {
    window.location.reload();
    this.accountService.Logout();
    
  }
 

}
