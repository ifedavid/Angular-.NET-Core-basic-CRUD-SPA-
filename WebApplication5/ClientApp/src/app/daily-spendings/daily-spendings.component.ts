import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UsersService } from '../services/users.service';
import { Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-daily-spendings',
  templateUrl: './daily-spendings.component.html',
  styleUrls: ['./daily-spendings.component.css']
})



export class DailySpendingsComponent implements OnInit {


  loading = false;
  errorMessage: string;

  SpendingData: any[] = [];
  UserId: any = localStorage.getItem('UserId');
  minDate: Date = new Date();
  maxDate: Date = new Date(2051, 11);
  constructor( private fb: FormBuilder, private userService: UsersService, private route: Router ) { }

  DatePicker = this.fb.group({
    Date: ['', Validators.required]
  });



  ngOnInit() {
    this.GetSpendings();
  }

  GetSpendings() {

    this.loading = true;

    this.userService.GetSpending().subscribe(
      result => {
        this.SpendingData = result;
        for (let item in result) {
          console.log(result[item]);
        }
        this.loading = false;

      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );
  }

  Start() {

    if ( this.DatePicker.valid == true ) {

      this.loading = true;
      console.log(this.DatePicker.value.Date.toDateString());

      this.errorMessage = '';
      let DailySpendingsData: any[] = [];

      DailySpendingsData.push({
     date: this.DatePicker.value.Date.toDateString(),
     UserId: this.UserId
    });

      console.log(DailySpendingsData[0]);
      this.userService.CreateSpending(DailySpendingsData[0]).subscribe(
      result => {
        this.loading = false;
        console.log(result);

        this.GetSpendings();
      },

      error => {
        this.loading = false;
        this.errorMessage = error.error.message;
        console.log(error);
      }
    );

    }

    else {
    console.log('field is required');
    }

  }

  Update(i) {
    this.loading = true;
    localStorage.setItem('TodayDate', this.SpendingData[i].dateId);


    let updateArray = [];
    updateArray.push({
      id: localStorage.getItem('TodayDate'),

    });


    this.userService.UpdateSpending(updateArray[0]).subscribe(
      result => {
      console.log(result);
      },
      error => {
        console.log(error);
      }
    );
    this.route.navigate(['/categories']);
  }

  Delete(i) {
    this.loading = true;
    let dateId = this.SpendingData[i].dateId;

    this.userService.DeleteSpending(dateId).subscribe(
      result => {
        console.log(result);
        this.GetSpendings();
        this.loading = false;
      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );

  }

}
