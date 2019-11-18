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


  currentUser = localStorage.getItem('UserId');


  private getCategoryUrl = 'api/Category/';
  private updateCategoryUrl = 'api/Category/Update';
  private deleteCategoryUrl = 'api/Category/Delete';

  private saveSpendingUrl = 'api/DailySpendings';
  private getSpendingUrl = 'api/DailySpendings/' + this.currentUser;
  private updateSpendingUrl = 'api/DailySpendings';



  constructor(private http: HttpClient) { }





  getCategories(currentDate) {
    return this.http.get<any>(this.getCategoryUrl + currentDate, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }

  SaveCategories(CategoryData, currentDate) {
    return this.http.post<any>(this.getCategoryUrl + currentDate, CategoryData, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    )
  }

  UpdateCategories(CategoryData) {
    return this.http.put<any>(this.updateCategoryUrl, CategoryData, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }

  DeleteCategories(CategoryData) {
    return this.http.post<any>(this.deleteCategoryUrl, CategoryData, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }

  GetSpending() {
    return this.http.get<any>(this.getSpendingUrl, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }


  CreateSpending(SpendingData) {
    return this.http.post<any>(this.saveSpendingUrl, SpendingData, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }

  UpdateSpending(UpdateArray) {
    return this.http.put<any>(this.updateSpendingUrl, UpdateArray, httpOptions).pipe(
      map (result => {
        console.log(result);
        return result;
      })
    );
  }

  DeleteSpending(dateId) {
    return this.http.delete<any>('api/DailySpendings/DeleteDailySpendings/' + dateId, httpOptions).pipe(
      map  (result => {
        console.log(result);
        return result;
      })
    );
  }
}



