import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Router, ActivatedRoute } from '@angular/router';




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private fb: FormBuilder, private accountService: AccountService, private route: Router, private Url: ActivatedRoute) { }

   loginForm: FormGroup;
   returnUrl: string;
   resultMessage: string;
   invalidLogin: boolean;
  

  ngOnInit() {
    this.loginForm = this.fb.group( {

      EmailAddress: ['', Validators.required],
      password: ['', Validators.required]

    });

    this.returnUrl = this.Url.snapshot.queryParams['/returnUrl'] || ['/home'] ;
  }

  onSubmit() {
    console.log(this.loginForm);
    this.accountService.Login(this.loginForm.value).
    subscribe(
      result => {
        this.invalidLogin = false;
        console.log('success', result);
        let username = result.username;
        let userRole = result.userRole;
        console.log(username + userRole);
        this.route.navigate(['/']);
      },
      error => {
        this.invalidLogin = true;

        this.resultMessage = 'Couldn\'t authenticate user. Please check your credentials';
        console.log(error);
    }
    );

  }

}
