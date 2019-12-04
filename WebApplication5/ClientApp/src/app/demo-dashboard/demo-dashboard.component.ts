import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-demo-dashboard',
  templateUrl: './demo-dashboard.component.html',
  styleUrls: ['./demo-dashboard.component.css']
})
export class DemoDashboardComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


  title = 'Top spending this week';
  type = 'ColumnChart';
  is3d = true;
  data = [
    ['2013', 40.0],
    ['2014', 56.8],
    ['2015', 42.8],
    ['2016', 38.5],
    ['2017', 30.2],
    ['2018', 46.7]
  ];
  columnNames = ['Year', 'Sales (millions)'];
  options = {
    is3D: true
  };
  
}
