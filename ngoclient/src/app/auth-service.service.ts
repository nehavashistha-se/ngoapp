import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable,throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppUserDetail } from './user/user.models';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http:HttpClient) {}
  get(data:AppUserDetail): Observable<any> {
    return this.http.post(environment.baseUrl+'Login/Login',data) .pipe(
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
