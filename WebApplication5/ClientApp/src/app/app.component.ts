import { Component, OnInit } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  constructor(private updates: SwUpdate) {
    updates.available.subscribe(result => {
      updates.activateUpdate().then(() => document.location.reload());
    })
  }

  ngOnInit() {


  }
}



