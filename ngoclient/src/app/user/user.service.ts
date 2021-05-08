import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable,throwError } from 'rxjs';
import { AppUserDetail } from './user.models';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {JwtHelperService} from '@auth0/angular-jwt';
import { Router } from '@angular/router';
const baseUrl = environment.baseUrl +'Login/';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  jwtHelper: any=new JwtHelperService();
 

  constructor(private http: HttpClient,public router: Router) { 
 if(this.tokenNotExpired())
 {
  sessionStorage.clear();
  this.router.navigate([''])
 }
  }
  getToken() {
    const token = sessionStorage.getItem('token');
    return token;
  }
  tokenNotExpired() {
    
    return this.getToken() != null && !this.jwtHelper.isTokenExpired(this.getToken());
  }
  httpOptions = {
    headers: new HttpHeaders({      
      'Content-Type': 'application/json',
      'Authorization':`Bearer ${this.getToken()}`
    })
  }
  getAll(): Observable<AppUserDetail[]> {
    return this.http.get<AppUserDetail[]>(baseUrl) .pipe(
      catchError(this.errorHandler)
    );
  }

  get(data:any): Observable<any> {
    return this.http.post(baseUrl+'GetUser',data,this.httpOptions) .pipe(
      catchError(this.errorHandler)
    );
  }

  create(data: any): Observable<any> {
    return this.http.post(baseUrl+'SaveUserDetail',JSON.stringify(data),this.httpOptions) .pipe(
      catchError(this.errorHandler)
    );
  }
  delete(data: any): Observable<any> {
    return this.http.post(baseUrl+'DeleteUser',JSON.stringify(data),this.httpOptions) .pipe(
      catchError(this.errorHandler)
    );
  }
  findByName(Name: any): Observable<any> {
    return this.http.post(`${baseUrl}`, JSON.stringify(AppUserDetail),this.httpOptions) .pipe(
      catchError(this.errorHandler)
    );
  }
  
  uploadFile(file: File): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', `${baseUrl}upload`, formData, {
      headers:new HttpHeaders().set('Authorization',`Bearer ${this.getToken()}`),
      reportProgress: true,
      responseType: 'json',
      
    });
    return this.http.request(request);
  }

  public downloadFile(docFile: string): Observable < Blob > {  
    return this.http.get(baseUrl + 'DownloadFile?filename=' + docFile, {  
      headers:new HttpHeaders({             
        'Authorization':`Bearer ${this.getToken()}`
      }),
        responseType: 'blob'  
    });  
}  
  errorHandler(error: { error: { message: string; }; status: any; message: any; }) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
 }
  
}