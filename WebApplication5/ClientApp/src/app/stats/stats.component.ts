import { Component, OnInit } from '@angular/core';
import {FormControl, FormBuilder, Validators} from '@angular/forms';
import {StatsService} from '../services/stats.service';
import {UsersService} from '../services/users.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {



  DailySpendings: any[] = [];
  loading = false;

  title = 'Population (in millions)';
   type = 'ColumnChart';
   data = [

   ];
   columnNames = ['Year', 'Asia', 'canada'];
   options = { colors: ['#e0440e', '#e6693e'] };
  dynamicResize = true;

  constructor(private fb: FormBuilder, private statsService: StatsService, private userService: UsersService) {
 
  }

  SelectGroup = this.fb.group({

    Selector: ['', Validators.required]
  });




  ngOnInit() {
  this.GetDailySpending();
    }



  GetDailySpending() {
    this.loading = true;

    let todayDate =  new Date().toDateString();

    this.statsService.GetDailySpendingStats().subscribe(
      result => {
        console.log(result.dailySpendings);
        this.DailySpendings = result.dailySpendings;

        this.title = 'Spendings for week ' + result.dailySpendings[0].weekNumber;
        this.type = 'ColumnChart';


        this.data = [];
        for (let category in result.dailySpendings) {
          this.data.push( [result.dailySpendings[category].dateString, result.dailySpendings[category].totalAmount] );
        }

        console.log(this.data);

        this.columnNames = ['Date', 'Total Amount'];
        this.options = { colors: ['#e0440e', '#e6693e'] };
        this.loading = false;
      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );
  }

  NextWeek() {
    this.loading = true;
    let weekNumber = this.DailySpendings[0].weekNumber;

    this.statsService.NextWeek(weekNumber).subscribe(
     result => {
      console.log(result);
      this.DailySpendings = result;

      this.title = 'Spendings for week ' + result[0].weekNumber;

      this.data = [];
      for (let category in result) {
        this.data.push( [result[category].dateString, result[category].totalAmount] );
      }

      console.log(this.data);
      this.loading = false;
     },
     error => {
       console.log(error);
       this.loading = false;
     }
   );
  }

  PreviousWeek() {
    this.loading = false;
    let weekNumber = this.DailySpendings[0].weekNumber;

    this.statsService.PreviousWeek(weekNumber).subscribe(
      result => {
      console.log(result);
      this.DailySpendings = result;

      this.data = [];
      this.title = 'Spendings for week ' + result[0].weekNumber;

        for (let category in result) {
          this.data.push( [result[category].dateString, result[category].totalAmount] );
      }

      console.log(this.data);
      this.loading = false;
    },
    error => {
      console.log(error);
      this.loading = false;
    }
    );
  }

  getDailySpending() {
    if (this.SelectGroup.valid == false) {

      console.error ('please select a date');
    } else {

      this.data = [];
      console.log(this.SelectGroup.value.Selector);

      const StatDate =  this.SelectGroup.value.Selector;

      console.log(StatDate);

      this.userService.getCategories(StatDate).subscribe(
      result => {
        console.log(result.value);

        this.title = 'Population (in millions)';
        this.type = 'ColumnChart';


        for (let category in result.value) {

          this.data.push([result.value[category].name, result.value[category].amount],);
        }



        this.columnNames = ['Expense', 'Amount'];
        this.options = { colors: ['#e0440e', '#e6693e'] };
      },
      error => {
        console.log(error);
      }
    );
    }
  }

  onSelect(arg) {
    console.log(arg);
  }

}
