import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ngo';
  users: any;
  constructor(private http: HttpClient,private spinner: NgxSpinnerService){}

  ngOnInit(){
  
  }
  
}
