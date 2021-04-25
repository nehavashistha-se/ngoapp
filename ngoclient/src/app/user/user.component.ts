import { Component, OnInit } from '@angular/core';
import { AppUserDetail } from './user.models';
import {UserService} from './user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  appuser: AppUserDetail = {
  

  };
  submitted = false;
  submittedname='Save'
  constructor(private userservice: UserService,private route: ActivatedRoute,public router: Router  ) { }

  ngOnInit(): void {
    this.newUser()
    this.appuser.userId=Number(this.route.snapshot.paramMap.get('id'));
    console.log(this.appuser)
    this.appuser.username="";
    if(this.appuser.userId>0)
    {this.getuserdetail()
      this.submittedname = 'Edit'
    }
  }
  getuserdetail():void {
    this.userservice.get(this.appuser).subscribe(response=>{
      this.submitted=true;
      console.log(response)
    this.appuser=response.data[0];
   
    },
    error => {
      console.log(error);
    });
  }

  
  saveUser(): void {
    
console.log(this.appuser);
    this.userservice.create(this.appuser)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
        },
        error => {
          console.log(error);
        });
  }

  newUser(): void {
    this.submitted = false;
    this.appuser =new AppUserDetail();
  }

}
