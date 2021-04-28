import { Component, OnInit } from '@angular/core';
import { AppUserDetail } from './user.models';
import {UserService} from './user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  
  success:boolean=false;
  appuser: AppUserDetail = {
  

  };
  baseurl=environment.baseUrl;
  submitted = false;
  submittedname='Save';
  baseApiUrl = environment.baseUrl+'Login/Upload'
  progress?: number;  
  message: string="";  
   
  constructor(private userservice: UserService,
    private route: ActivatedRoute,
    public router: Router ,
    private http: HttpClient,
    private spinner:NgxSpinnerService ) {   if(localStorage.getItem("userid")=="" || localStorage.getItem("userid")=="0")
    {
    this.router.navigate(['']);
      
    }}

  ngOnInit(): void {
    
    this.appuser.userId=Number(this.route.snapshot.paramMap.get('id'));
    this.appuser.username="";
    if(this.appuser.userId>0 && (localStorage.getItem("role")=="admin" || Number(localStorage.getItem("userid"))==this.appuser.userId))
    {this.getuserdetail()
      this.submittedname = 'Update'
    }
   
  }
  getuserdetail():void {
    this.spinner.show
    this.userservice.get(this.appuser).subscribe(response=>{
      if(response){
      this.spinner.hide
      
      }
      
    this.appuser=response.data[0];
 
    console.log(this.appuser);
    },
    error => {
      if(error){
        this.spinner.hide
        
        }
      this.message=error;
    });
  }

  
  saveUser(): void {
    console.log(this.appuser.dob)
    this.spinner.show
    
console.log(this.appuser);
    this.userservice.create(this.appuser)
      .subscribe(
        response => {
          this.spinner.hide
          if(response){
          this.message="Successfully Saved"
          this.success=true;
          }

        },
        error => {
          this.message="Error Occured"
          this.success=false;
          if(error){
            this.spinner.hide
            
            }
        });
  }

  newUser(): void {
    this.submitted = false;
    this.appuser =new AppUserDetail();
  }
   
  // OnClick of button Upload
 
  upload(files:any,type:string) {  
    if (files.length === 0)  
      return;  
  
    const formData = new FormData();  
       formData.append(files[0].name, files[0]);
    
      
    
    const uploadReq = new HttpRequest('POST', this.baseApiUrl, formData, {  
      
      reportProgress: true,  
    });  
   
    this.http.request(uploadReq).subscribe(event => {  
      this.spinner.show
       if(event)
       {
 
        this.spinner.hide
        if(type="image")
      this.appuser.image=files[0].name;
      else
      this.appuser.biodata=files[0].name;
        

       }
    });  
  }  
}
