import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppUserDetail } from '../user/user.models';
import { UserService } from '../user/user.service';
import { Router } from '@angular/router';
import { GlobalConstants } from '../GlobalParameters/global-constant';

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
  totalrecord: any=0;
  numberOfObjectsPerPage:number=Number(GlobalConstants.numberOfObjectsPerPage);
  constructor(private userservice: UserService,public router: Router ) { 
      if(localStorage.getItem("userid")=="" || localStorage.getItem("userid")=="0" || localStorage.getItem("userid")==null)
  {
  this.router.navigate(['']);
    
  }}

  ngOnInit(): void {
    this.getUsers(0);
  }

  deleteuser(userid:any):void {
    this.appuser.userId=userid;
    this.userservice.delete(this.appuser).subscribe(response=>{
      
    },
    error => {
      //console.log(error);
    });
    this.router.navigate(['ViewUser']).then(u=>{
      window.location.reload();
     } );
  };
  counter(i: number) {
    
    let indx=Number((this.totalrecord>this.numberOfObjectsPerPage?Math.ceil(this.totalrecord/this.numberOfObjectsPerPage):1));
  
    return new Array(indx);
}
  getUsers(pagenumber:number):void {
    //console.log(this.appuser);
    var appdata={
     _ObjappUser:this.appuser,
      numberOfObjectsPerPage:this.numberOfObjectsPerPage,
      pageNumber:pagenumber

    }
    //console.log(appdata)
    this.userservice.get(appdata).subscribe(response=>{
      
      //console.log(response.data)
    this.viewappuser=response.data;
   this.totalrecord=response.id;
    },
    error => {
      //console.log(error);
    });
  }
}


