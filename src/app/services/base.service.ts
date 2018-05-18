import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';


export class BaseService {

  constructor() { }


  protected handleOperationError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      let modelStateErrors = '';
      const serverError = error.json();

      if (!serverError.type) {
        for (let key in serverError) {
          if (serverError[key]) {
            modelStateErrors += (serverError[key] + '\n');
          }
        }
      }

      if (modelStateErrors) {
        console.log(`model state errors-> ${modelStateErrors} `);
      }



      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  protected unsafeHandleOperationError<T>(operation = 'operation') {
    return (error: any): Observable<T> => {

      console.log(`${operation} failed: ${error.message}`);
      let modelStateErrors = '';

      if (error.error) {
        for (let key in error.error) {
          if (error.error[key]) {
            modelStateErrors += (key + ': ' + error.error[key] + '\n');
          }
        }
      }

      if (modelStateErrors) {
       return throwError(modelStateErrors);
      }

      if (error.message) {
        return throwError(error.message);
      }
      //return Observable.throw('Server Error');
      return throwError('Server Error');
    };
  }
}
