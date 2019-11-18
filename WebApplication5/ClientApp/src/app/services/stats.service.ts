import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { Router } from '@angular/router';


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
export class StatsService {

  constructor(private http: HttpClient) { }

  currentUser = localStorage.getItem('UserId');

  private DailySpendingStatsUrl = 'api/Stats';



  GetDailySpendingStats() {
    return this.http.get<any>(this.DailySpendingStatsUrl, httpOptions).pipe(
    map( result => {
      console.log(result);
      return result;
    })
    );
  }

  NextWeek(weekNumber) {
    weekNumber = weekNumber + 1;
    return this.http.get<any>('api/Stats/GetWeek/' + weekNumber, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }

  PreviousWeek(weekNumber) {
    weekNumber = weekNumber - 1;
    return this.http.get<any>('api/Stats/GetWeek/' + weekNumber, httpOptions).pipe(
      map(result => {
        console.log(result);
        return result;
      })
    );
  }



}
