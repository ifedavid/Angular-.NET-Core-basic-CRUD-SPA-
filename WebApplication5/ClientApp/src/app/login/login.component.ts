import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'angularx-social-login';
import { FacebookLoginProvider, GoogleLoginProvider } from 'angularx-social-login';







@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  userData: any [] = [];
  public loading = false;
  // tslint:disable-next-line: max-line-length
  constructor(private fb: FormBuilder, private accountService: AccountService, private route: Router, private Url: ActivatedRoute, private authService: AuthService ) { }


   returnUrl: string;
   resultMessage: string;
   invalidLogin: boolean;

  ngOnInit() {
    this.returnUrl = this.Url.snapshot.queryParams['/returnUrl'] || ['/home'] ;
  }



  signInWithGoogle(platform: string): void {
    platform = GoogleLoginProvider.PROVIDER_ID;
    this.loading = true;
    this.authService.signIn(platform).then(
      (response) => {
        // Get all user details
        console.log(platform + ' logged in user data is= ' , response);

        // Take the details we need and store in an array
        this.userData.push({
          UserId: response.id,
          Provider: response.provider,
          FirstName: response.firstName,
          LastName: response.lastName,
          EmailAddress: response.email,
          PictureUrl: response.photoUrl,
          OauthToken: response.authToken
        });

        // Take the array and send to our accountservice.login method
        this.accountService.Login(this.userData[0]).subscribe(
          result => {
          console.log('success', result);
          this.route.navigate(['/home']);
          this.loading = false;
        },
        error => {
          this.resultMessage = 'There was an error with our database. Sorry!';
          console.log(error);
          this.loading = false;
        }
        );
      },
      error => {
        console.log(error);
        this.resultMessage = error;
        this.loading = false;
      }
    );
  }

  signInWithFacebook(platform: string): void {
    this.loading = true;
    platform = FacebookLoginProvider.PROVIDER_ID;
    this.authService.signIn(platform).then(
      (response) => {
        console.log(platform + ' logged in user data is= ' , response);

         // Take the details we need and store in an array
        this.userData.push({
          UserId: response.id,
          Provider: response.provider,
          FirstName: response.firstName,
          LastName: response.lastName,
          EmailAddress: response.email,
          PictureUrl: response.photoUrl,
          OauthToken: response.authToken
        });

         // Take the array and send to our accountservice.login method
        this.accountService.Login(this.userData[0]).subscribe(
          result => {
          console.log('success', result);
          this.loading = false;
          this.route.navigate(['/home']);
        },
        error => {
          this.loading = false;
          this.resultMessage = 'There was an error from our database. Sorry!';
          console.log(error);
        }
        );

      },
      error => {
        console.log(error);
        this.loading = false;
        this.resultMessage = error;
      }
    );
  }

  signOut(): void {
    this.loading = true;
    this.authService.signOut();
    this.accountService.Logout();
    console.log('User has signed our');
    this.resultMessage = 'User has signed out';
  }

  }
