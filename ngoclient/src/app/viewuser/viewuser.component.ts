import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppUserDetail } from '../user/user.models';
import { UserService } from '../user/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-viewuser',
  templateUrl: './viewuser.component.html',
  styleUrls: ['./viewuser.component.css']
})
export class ViewuserComponent implements OnInit {
 viewappuser?:AppUserDetail[];
 appuser: AppUserDetail={
   username:"",
   userId:0
 };
 
  constructor(private userservice: UserService,public router: Router ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  deleteuser():void {
    this.userservice.delete(this.appuser).subscribe(response=>{
      
  
    },
    error => {
      console.log(error);
    });
    this.router.navigate(['ViewUser']);
  };
  getUsers():void {
    console.log(this.appuser);
    this.userservice.get(this.appuser).subscribe(response=>{
      
      console.log(response.data)
    this.viewappuser=response.data;
   
    },
    error => {
      console.log(error);
    });
  }
}


