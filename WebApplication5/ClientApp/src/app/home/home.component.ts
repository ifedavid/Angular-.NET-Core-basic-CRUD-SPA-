import { Component, OnInit, ApplicationRef } from '@angular/core';
import { AccountService } from '../services/account.service';
import { SwUpdate } from '@angular/service-worker';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private accountService: AccountService) {

   
  }

  ngOnInit() {
  }

  UserId = this.accountService.CurrentUserId.value;

  

}
