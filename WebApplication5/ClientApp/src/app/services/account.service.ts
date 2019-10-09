import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  //communicate with web api
  constructor(private http: HttpClient, private router: Router) { }

  //properties needed
  private baseUrlRegister = 'api/Account/Register';
  private baseUrlLogin = 'api/Account/Login';
  private loginStatus = new BehaviorSubject<boolean>(this.getLoginStatus());
  private username = new BehaviorSubject<string>(localStorage.getItem('Username'));
  private userRole = new BehaviorSubject<string>(localStorage.getItem('userRole'));
  private supp : string;


  getLoginStatus(): boolean {
return  false;
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

  //register method
  Login(userData) {
    return this.http.post<any>(this.baseUrlLogin, userData).pipe(
     map(result => {

      if (result && result.username) {
            this.loginStatus.next(true);
            localStorage.setItem('loginStatus', '1');
            localStorage.setItem('username', result.username);
            localStorage.setItem('userRole', result.userRole);
      }
      return result;
     })
    );
  }


   Register(userData) {
     return this.http.post<any>(this.baseUrlRegister, userData).pipe(
      map(result =>  {
        return result;
      })

     );
   }

   Logout() {
     this.loginStatus.next(false);
     localStorage.setItem('loginStatus', '0');
     localStorage.removeItem('username');
     localStorage.removeItem('userRole');
     this.router.navigate(['/login']);
     console.log('User Logged out successfully');
   }





}
