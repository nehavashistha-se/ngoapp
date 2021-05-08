import { Component, OnInit } from '@angular/core';
import { AppUserDetail } from './user.models';
import {UserService} from './user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from "ngx-spinner";
import{ GlobalConstants } from '../GlobalParameters/global-constant';

import * as CryptoJS from 'crypto-js';
import {  
  saveAs as importedSaveAs  
} from "file-saver";  
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
  role?: any;
  userid: number;
   
  constructor(private userservice: UserService,
    private route: ActivatedRoute,
    public router: Router ,
    private http: HttpClient,
    private spinner:NgxSpinnerService 
    ) {   
      let lrole:any = sessionStorage.getItem("role");//GlobalConstants.role;
      this.role=CryptoJS.AES.decrypt( lrole,GlobalConstants.encryptionpassword ).toString(CryptoJS.enc.Utf8);  //GlobalConstants.role;
      let luserid:any = sessionStorage.getItem("userid");//GlobalConstants.role;
      this.userid =Number(CryptoJS.AES.decrypt( luserid ,GlobalConstants.encryptionpassword ).toString(CryptoJS.enc.Utf8));  //GlobalConstants.role;
     
      //console.log(this.userid)
    if( this.role==null||sessionStorage.getItem("userid")==""  || sessionStorage.getItem("userid")==null)
    {
    this.router.navigate(['']);
      
    }
  
  }

  ngOnInit(): void {
   
    GlobalConstants.role=this.role;
    this.appuser.userId=Number(this.route.snapshot.paramMap.get('id'));
    //console.log(this.appuser.userId)
    this.appuser.username="";
    if(this.appuser.userId>0)
    {
     
      if((GlobalConstants.role=="admin" || (GlobalConstants.role!="admin" && this.userid==this.appuser.userId)))
      {
      this.getuserdetail()
      this.submittedname = 'Update'
    }
    else
    {

      this.router.navigate(['EditUser', GlobalConstants.userid ]).then(u=>{
        window.location.reload();
       });
    }
  }
   
  }
  getuserdetail():void {
    this.spinner.show
    var appdata={
      _ObjappUser:this.appuser,
       numberOfObjectsPerPage:1,
       pageNumber:0
 
     }
    this.userservice.get(appdata).subscribe(response=>{
      if(response){
      this.spinner.hide
      if(response.data[0]!=null)
      this.appuser=response.data[0];
      }
      else
      this.newUser();
   
 
    //console.log(this.appuser);
    },
    error => {
      if(error){
        this.spinner.hide
        
        }
      this.message=error;
    });
  }

  
  saveUser(): void {
    //console.log(this.appuser.dob)
    this.spinner.show
    
//console.log(this.appuser);
    this.userservice.create(this.appuser)
      .subscribe(
        response => {
          this.spinner.hide
          if(response){ 
            //console.log(response)
          this.message=response.exception;
          if (response.status_Code==0)
          this.success=true;
          else
          this.success=false;
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
// OnClick of button Download

downloadFile(filename?:string){
if(filename) 
  this.userservice.downloadFile(filename).subscribe(response=>{ 
    importedSaveAs(response, filename)  
  }); 

}
  // OnClick of button Upload
 
  upload(files:any,type:string) {  
    if (files.length === 0)  
      return;  
  
    const formData = new FormData();  
       formData.append(files[0].name, files[0]);
    
      
    
    const uploadReq = new HttpRequest('POST', this.baseApiUrl, formData, {  
      headers:new HttpHeaders().set('Authorization',`Bearer ${this.userservice.getToken()}`),
      reportProgress: true, 
    });  
   
    this.http.request(uploadReq).subscribe(event => {  
      this.spinner.show
       if(event)
       {
 
        this.spinner.hide
        if(type=="image")
      this.appuser.image=files[0].name;
      else if(type=="publications")
      this.appuser.publications=files[0].name;
      else
      this.appuser.biodata=files[0].name;
        

       }
    });  
  }  
}
