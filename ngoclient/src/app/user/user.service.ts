import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
   
import { Observable,throwError } from 'rxjs';
import { AppUserDetail } from './user.models';
import { catchError } from 'rxjs/operators';
const baseUrl = 'https://localhost:5001/api/Login/';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  getAll(): Observable<AppUserDetail[]> {
    return this.http.get<AppUserDetail[]>(baseUrl) .pipe(
      catchError(this.errorHandler)
    );
  }

  get(data:AppUserDetail): Observable<any> {
    return this.http.post(baseUrl+'GetUser',data) .pipe(
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
    return this.http.post(`${baseUrl}`, JSON.stringify(AppUserDetail)) .pipe(
      catchError(this.errorHandler)
    );
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