import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";





@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  // tslint:disable-next-line: max-line-length
  constructor(private fb: FormBuilder, private accountService: AccountService, private route: Router, private Url: ActivatedRoute, private authService: AuthService ) { }

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

  /*signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }*/

  signInWithGoogle(platform: string): void {
    platform = GoogleLoginProvider.PROVIDER_ID;
    this.authService.signIn(platform).then(
      (response) => {
        console.log(platform + ' logged in user data is= ' , response);
      }
    );
  }

  signInWithFacebook(){
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(
      (response) => {
        console.log(FacebookLoginProvider.PROVIDER_ID + ' logged in user data is= ' , response);
      }
    );
  }

  }
