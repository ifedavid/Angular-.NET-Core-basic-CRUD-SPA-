import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { RouterModule } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  //communicate with web api
  constructor(private http: HttpClient, private router: RouterModule) { }

  private baseUrlLogin = 'api/controller/loginCustomer';

  public LoginStatus = new BehaviorSubject<boolean>(this.setLoginStatus());
  private UserName = new BehaviorSubject<string>(localStorage.getItem('username'));
  private Role = new BehaviorSubject<string>(localStorage.getItem('userRole'));

  login(username: string, password: string){

    return this.http.post<any>(this.baseUrlLogin, {username, password}).pipe(
      map(result => {
 
        if (result && result.username){


          this.LoginStatus.next(true);
          localStorage.setItem('loginStatus', '1');
          localStorage.setItem('username', result.username);
          localStorage.setItem('userRole', result.userRole);
        }
        return result;
       })


    );
  }

  logout(){
    localStorage.setItem('loginStatus', '0');
    localStorage.removeItem('username');
    localStorage.removeItem('userRole');
    this.LoginStatus.next(false);
    this.router.navigate(['/login']);
    console.log('The user was successfully logged out');

  }
  setLoginStatus(): boolean
  {
    return false;
  }


}
