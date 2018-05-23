import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { BaseService } from './base.service';
import { Observable, BehaviorSubject, of } from 'rxjs';

import { UserRegistration } from '../models/user.registration';
import { catchError, map, tap } from 'rxjs/operators';
import { TestData, AuthResponse } from "../models/csmodels";

@Injectable({
  providedIn: 'root'
})
export class UserAuthService extends BaseService {

  private baseUrl = 'http://localhost:5000/api';
  private account_url = '/account';
  private test_url = '/data/publicData';
  private test_urlapi = '/data/userid_api';
  private test_urlview = '/data/userid_view';
  private auth_url = '/auth/login';

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private httpClient: HttpClient) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');


    //  // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
  }


  public testConnect(): Observable<TestData> {
    return this.httpClient.get<TestData>(this.baseUrl + this.test_url, this.getRequestOptions(true))
      .pipe(tap(r => console.log(`success->` + r)), catchError(super.unsafeHandleOperationError<TestData>('testConnect')));
  }

  public testConnectApi(): Observable<TestData> {
    return this.httpClient.get<TestData>(this.baseUrl + this.test_urlapi, this.getRequestOptions(true))
      .pipe(tap(r => console.log(`success->` + r)), catchError(super.unsafeHandleOperationError<TestData>('testConnectApi')));
  }

  public testConnectView(): Observable<TestData> {
    return this.httpClient.get<TestData>(this.baseUrl + this.test_urlview, this.getRequestOptions(true))
      .pipe(tap(r => console.log(`success->` + r)), catchError(super.unsafeHandleOperationError<TestData>('testConnectView')));
  }

  private getRequestOptions(withAuth = false) {

    if (withAuth && this.loggedIn) {
      return {
        headers: new HttpHeaders({
          'Authorization': 'Bearer ' + localStorage.getItem('auth_token')
        })
      };
    }
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };


  }


  //  register(email: string, password: string, firstName: string, lastName: string,location: string): Observable<UserRegistration> {
  //   let body = JSON.stringify({ email, password, firstName, lastName, location });
  //   let headers = new Headers({ 'Content-Type': 'application/json' });
  //   let options = new RequestOptions({ headers: headers });

  //   return this.http.post(this.baseUrl + this.account_UrlPart, body, options)
  //     .map(res => true)
  //     .catch(this.handleError);
  // }
  register(email: string,
    password: string,
    firstName: string,
    lastName: string,
    location: string): Observable<boolean> {
    let body = JSON.stringify({ email, password, firstName, lastName, location });
    let options = this.getRequestOptions();

    return this.httpClient
      .post<boolean>(this.baseUrl + this.account_url, body, options)
      .pipe(map(res => true), catchError(super.unsafeHandleOperationError<boolean>('register')));
  }
  //  login(userName, password) {
  //   let headers = new Headers();
  //   headers.append('Content-Type', 'application/json');

  //   return this.httpClient
  //     .post(
  //     this.baseUrl + '/auth/login',
  //     JSON.stringify({ userName, password }),{ headers }
  //     )
  //     .map(res => res.json())
  //     .map(res => {
  //       localStorage.setItem('auth_token', res.auth_token);
  //       this.loggedIn = true;
  //       this._authNavStatusSource.next(true);
  //       return true;
  //     })
  //     .catch(this.handleError);

  // }

  login(userName, password): Observable<boolean> {
    let httpOptions = this.getRequestOptions();
    let body = JSON.stringify({ userName, password });
    return this.httpClient
      .post<AuthResponse>(this.baseUrl + this.auth_url, body, httpOptions)
      .pipe(map(res => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      }),
      catchError(super.unsafeHandleOperationError<boolean>('login'))
      );
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  // facebookLogin(accessToken:string) {
  //   let headers = new Headers();
  //   headers.append('Content-Type', 'application/json');
  //   let body = JSON.stringify({ accessToken });  
  //   return this.http
  //     .post(
  //     this.baseUrl + '/externalauth/facebook', body, { headers })
  //     .map(res => res.json())
  //     .map(res => {
  //       localStorage.setItem('auth_token', res.auth_token);
  //       this.loggedIn = true;
  //       this._authNavStatusSource.next(true);
  //       return true;
  //     })
  //     .catch(this.handleError);
  // }
}
