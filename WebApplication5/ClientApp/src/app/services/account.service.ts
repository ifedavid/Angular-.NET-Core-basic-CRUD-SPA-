import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // communicate with web api
  constructor(private http: HttpClient, private router: Router) { }

  // properties needed
  private baseUrlLogin = 'api/Account/Login';
  private loginStatus = new BehaviorSubject<boolean>(this.getLoginStatus());
  private username = new BehaviorSubject<string>(localStorage.getItem('username'));
  private userRole = new BehaviorSubject<string>(localStorage.getItem('userRole'));
  private UserId = new BehaviorSubject<string>(localStorage.getItem('UserId'));



  getLoginStatus(): boolean {
return  false;
}

get CurrentUserId() {
  return this.UserId;
}

get IsLoggedIn() {
  return this.loginStatus;
}
get CurrentUsername() {
  return this.username;
}
get CurrentUserRole() {
  return this.userRole;
}

  // register method
  Login(userData) {
    return this.http.post<any>(this.baseUrlLogin, userData).pipe(
     map(result => {

      if (result && result.message != null) {
            console.log(result);
            this.loginStatus.next(true);
            localStorage.setItem('username', result.username);
            localStorage.setItem('userRole', result.userRole);
            localStorage.setItem('loginStatus', '1');
            localStorage.setItem('UserId', result.id);
            localStorage.setItem('token', result.token);
        localStorage.setItem('tokenExpiration', result.expiration);
        localStorage.setItem('PictureUrl', result.pictureUrl);
            console.log('We sent a message to our Controller API. It works');
      }
      return result;
     })
    );
  }




   Logout() {
     this.loginStatus.next(false);
     localStorage.setItem('loginStatus', '0');
     localStorage.removeItem('token');
     localStorage.removeItem('tokenExpiration');
     localStorage.removeItem('username');
     localStorage.removeItem('UserId');
     localStorage.removeItem('userRole');
     this.router.navigate(['/login']);
     console.log('User Logged out successfully');
   }


  AuthorizeUser() {
  let userId = this.CurrentUserId;
   let loginStatus = this.loginStatus;
   

    if (userId == null) {
      this.router.navigate(['/home']);
    }

  }

}
