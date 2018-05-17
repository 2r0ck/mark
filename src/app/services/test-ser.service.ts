import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Http } from '@angular/http';


import { Observable, BehaviorSubject, of } from 'rxjs';

import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TestSerService {

  private baseUrl = 'http://localhost:5000/api';
  private account_UrlPart = '/account';
  private test_Url = '/data/publicData';

  constructor(private httpClient: HttpClient, private http: Http) { }


  do() {
    console.log("test do!");
    let url = this.baseUrl + this.test_Url;
    //this.httpClient.

    let res = this.httpClient.get<any>(url).subscribe(res => {
          console.log(res);
      });

    let res2 = this.http.get(url)
      .subscribe(res => {
          console.log(res);
      });
    //.pipe(tap(r => console.log(`success->` + r)), catchError(this.handleOperationError<any>('test')));
  }

  protected handleOperationError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
