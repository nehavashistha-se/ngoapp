import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { ViewuserComponent } from './viewuser/viewuser.component';

const routes: Routes = [{path:'', component:LoginComponent},
{path:'Adduser', component:UserComponent},
{path:'EditUser/:id', component:UserComponent},
{path:'ViewUser', component:ViewuserComponent},
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
