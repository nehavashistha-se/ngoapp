import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { GlobalConstants } from '../GlobalParameters/global-constant';

import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  userid!: number;
 
  constructor(public router: Router) { 
  

  }

  ngOnInit(): void {
    let luserid:any = sessionStorage.getItem("userid");//GlobalConstants.role;
    this.userid =Number(CryptoJS.AES.decrypt( luserid ,GlobalConstants.encryptionpassword ).toString(CryptoJS.enc.Utf8));  //GlobalConstants.role;
   
  }
LogOut():void{

  sessionStorage.clear();
  this.router.navigate([''])

}
}
