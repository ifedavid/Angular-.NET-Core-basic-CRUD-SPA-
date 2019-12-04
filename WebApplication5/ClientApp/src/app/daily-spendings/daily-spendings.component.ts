import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UsersService } from '../services/users.service';
import { AccountService } from '../services/account.service';
import { Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { EventInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGrigPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction'; // for dateClick
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { SwUpdate } from '@angular/service-worker';


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
  minDate: Date = new Date(2018, 11);
  maxDate: Date = new Date(2051, 11);
  constructor(private fb: FormBuilder, private userService: UsersService, private route: Router, private accountService: AccountService, private activatedRoute: ActivatedRoute, private updates: SwUpdate) {

    updates.available.subscribe(result => {
      updates.activateUpdate().then(() => document.location.reload());
    })
  }

  DatePicker = this.fb.group({
    Date: ['', Validators.required]
  });

  @ViewChild('calendar', { static: true }) calendarComponent: FullCalendarComponent; // the #calendar in the template
  calendarPlugins = [dayGridPlugin, timeGrigPlugin, interactionPlugin];
  calendarEvents: EventInput[] = [];

  ngOnInit() {
    this.accountService.AuthorizeUser();
    this.GetSpendings();
  }

  GetSpendings() {

    this.loading = true;

    this.userService.GetSpending().subscribe(
      result => {
        this.SpendingData = result;
        for (let item in result) {
          console.log(result[item]);

          this.calendarEvents = this.calendarEvents.concat({ // add new event data. must create new array
            title: 'Total Amount: '
              + result[item].totalAmount,
            className: 'track-record',
            start: result[item].dateString,
            url: 'categories/' + result[item].dateId,
            backgroundColor: '#64b2cd'
          });

        }

        this.loading = false;

      },
      error => {
        console.log(error);
        this.loading = false;
      }
    );
  }



  handleDateClick(arg) {
   

    if (confirm('Would you like to add a record to ' + arg.dateStr + ' ?')) {

      this.loading = true;


      this.errorMessage = '';

      let DailySpendingsData: any[] = [];

      DailySpendingsData.push({
        date: arg.dateStr,
        UserId: this.UserId
      });

      console.log(DailySpendingsData[0]);
      this.userService.CreateSpending(DailySpendingsData[0]).subscribe(
        result => {

          this.calendarEvents = this.calendarEvents.concat({ // add new event data. must create new array
            title: '',
            date: arg.dateStr,
            className: 'track-record',
            allDay: arg.allDay,
            backgroundColor: '#64b2cd'
          });

            
          this.route.navigate(['/categories/' + result.dateId]);


          console.log(result);


        },

        error => {
          this.loading = false;
          this.errorMessage = error.error.message;
          console.log(error);
        }
      );



      console.log(arg);

    
    }
  }

  Update(i): string {
    this.loading = true;

    let returnMessage: string;

    let updateArray = [];
    updateArray.push({
      id: localStorage.getItem('TodayDate'),

    });


    this.userService.UpdateSpending(updateArray[0]).subscribe(
      result => {
        console.log(result);
        returnMessage = result.message;
               
      },
      error => {
        console.log(error);
        returnMessage = error.error.message;
      }
    );

    return returnMessage;
   
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
