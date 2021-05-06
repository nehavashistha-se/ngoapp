import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
 
import * as CryptoJS from 'crypto-js';  
import { Router, ActivatedRoute } from '@angular/router';
import { AuthServiceService } from '../auth-service.service';
import { AppUserDetail } from '../user/user.models';
import { GlobalConstants } from '../GlobalParameters/global-constant';


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
      console.log(result.data)

        if (result.status_Code==0){
          sessionStorage.setItem("userid", CryptoJS.AES.encrypt(result.data.userId.toString(), GlobalConstants.encryptionpassword).toString())
          sessionStorage.setItem("role",CryptoJS.AES.encrypt(result.data.role.toString(), GlobalConstants.encryptionpassword).toString())          // GlobalConstants.role=result.data.role.toString();
          // GlobalConstants.userid=result.data.userid;
         if(result.data.role=="admin"){
          this.router.navigate(['ViewUser'])
         }
        else
        {
         //console.log(result.data.userId)
          this.router.navigate(['EditUser',result.data.userId])
        }}else{
          this.spinner.hide;
         //console.log(result)
          this.message=result.exception;
          this.appuser=new AppUserDetail();
        }
      }
      },
      err=>{
        this.spinner.hide;
        console.log(err)
         this.message="Please try again!";
         this.appuser=new AppUserDetail();

      })
      this.spinner.hide;
   
  }
  register(){
    sessionStorage.setItem("userid", CryptoJS.AES.encrypt("-1", GlobalConstants.encryptionpassword).toString())
    sessionStorage.setItem("role",CryptoJS.AES.encrypt("User", GlobalConstants.encryptionpassword).toString())          // GlobalConstants.role=result.data.role.toString();

    this.router.navigate(['Adduser'])
  }
}
