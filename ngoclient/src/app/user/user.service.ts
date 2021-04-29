import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest, HttpEvent } from '@angular/common/http';
   
import { Observable,throwError } from 'rxjs';
import { AppUserDetail } from './user.models';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
const baseUrl = environment.baseUrl +'Login/';

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

  get(data:any): Observable<any> {
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
  
  uploadFile(file: File): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const request = new HttpRequest('POST', `${baseUrl}upload`, formData, {
      reportProgress: true,
      responseType: 'json'
    });

    return this.http.request(request);
  }

  public downloadFile(docFile: string): Observable < Blob > {  
    return this.http.get(baseUrl + 'DownloadFile?filename=' + docFile, {  
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