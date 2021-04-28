import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";

import { Router, ActivatedRoute } from '@angular/router';
import { AuthServiceService } from '../auth-service.service';
import { AppUserDetail } from '../user/user.models';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  appuser: AppUserDetail={};
  message: string="";
  showerror:boolean=false;
  constructor(
    private AuthService:AuthServiceService,
    private route: ActivatedRoute,
    private router: Router,
    private spinner: NgxSpinnerService
    ) {}
  ngOnInit() {
    this.initForm();
  }
  initForm(){
    
  }

  loginProcess(){
    this.spinner.show;
    
      this.AuthService.get(this.appuser).subscribe(result=>{
      if(result){
      this.spinner.hide;
      }
        if (result.status_Code==0){
          localStorage.setItem("userid",result.data.userId.toString())
          localStorage.setItem("role",result.data.role.toString())
         if(result.data.role=="admin"){
          
          this.router.navigate(['ViewUser'])
         }
        else
          this.router.navigate(['EditUser/'+result.id])
        }else{
          this.spinner.hide;
          this.message=result.exception;
          this.appuser=new AppUserDetail();
        }
      })
      this.spinner.hide;
   
  }
}
