import { Component, OnInit } from '@angular/core';
import { AppUserDetail } from './user.models';
import {UserService} from './user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpEventType, HttpResponse,HttpClient, HttpRequest } from '@angular/common/http';
import { FileUploadService } from '../file-upload.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  appuser: AppUserDetail = {
  

  };
  submitted = false;
  submittedname='Save';
  baseApiUrl = environment.baseUrl+'Login/Upload'
  progress?: number;  
  message?: string;  
   
  constructor(private userservice: UserService,private route: ActivatedRoute
    ,public router: Router ,private http: HttpClient ) { }

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
   
  // OnClick of button Upload
 
  upload(files:any) {  
    if (files.length === 0)  
      return;  
  
    const formData = new FormData();  
  
    for (let file of files)  
      formData.append(file.name, file);  
  
    const uploadReq = new HttpRequest('POST', this.baseApiUrl, formData, {  
      reportProgress: true,  
    });  
   
    this.http.request(uploadReq).subscribe(event => {  
       
    });  
  }  
}
