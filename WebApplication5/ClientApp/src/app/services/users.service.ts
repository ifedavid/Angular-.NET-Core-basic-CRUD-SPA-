import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


const authToken = localStorage.getItem('token');


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'bearer ' + authToken
  })
};



@Injectable({
  providedIn: 'root'
})

export class UsersService {

  private getCategoryUrl = 'api/Category';
  constructor(private http: HttpClient) { }





  getCategories() {
    return this.http.get<any>(this.getCategoryUrl, httpOptions).pipe(
      map(result => {
           console.log(result);
      })
    );
  }

  SaveCategories(CategoryData) {
    return this.http.post<any>(this.getCategoryUrl, CategoryData, httpOptions).pipe(
      map(result => {
        console.log(result);
      })
    )
  }
}



