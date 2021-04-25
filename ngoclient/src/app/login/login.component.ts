import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  constructor(
    private AuthService:AuthServiceService,
    private route: ActivatedRoute,
    private router: Router,
    ) {}
  ngOnInit() {
    this.initForm();
  }
  initForm(){
    
  }

  loginProcess(){
    
      this.AuthService.get(this.appuser).subscribe(result=>{
      
       
        if (result.status_Code==0){
         
         if(result.data.role="admin")
          this.router.navigate(['ViewUser'])
else
this.router.navigate(['EditUser/'+result.id])
        }else{
          alert(result.error);
          this.appuser=new AppUserDetail();
        }
      })
   
  }
}
