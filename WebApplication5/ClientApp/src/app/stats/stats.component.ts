import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {


  title = 'Population (in millions)';
   type = 'ColumnChart';
   data = [
      ["2012", 900, 390],
      ["2013", 1000, 450],
      ["2014", 1170, 3000],
      ["2015", 1250, 2000],
      ["2016", 1530, 200]
   ];
   columnNames = ['Year', 'Asia', 'canada'];
   options = { colors: ['#e0440e', '#e6693e', '#ec8f6e', '#f3b49f', '#f6c7b6'] };
   width = 550;
   height = 400;

  constructor() { }

  ngOnInit() {
  }


}
