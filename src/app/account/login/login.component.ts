import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserAuthService } from "../../services/user.auth.service";
import { Credentials } from "../../models/credentials";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {


  private subscription: Subscription;

  brandNew: boolean;
  errors: string;
  isRequesting: boolean;
  submitted = false;
  credentials: Credentials = { email: '', password: '' };

  constructor(private userService: UserAuthService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {

    // subscribe to router event
    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => {
        this.brandNew = param['brandNew'];
        this.credentials.email = param['email'];
      });

    this.credentials.email = 'admin@gmail.com';
    this.credentials.password = 'P@ssw0rd';

  }

  ngOnDestroy() {
    // prevent memory leak by unsubscribing
    this.subscription.unsubscribe();
  }

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    //if (valid) {
      //this.userService.login(value.email, value.password)
        this.userService.login('admin@gmail.com', 'P@ssw0rd')
        .subscribe(
        result => {
          this.isRequesting = false;
          if (result) {
            this.router.navigate(['/home']);
          }
        },
        error => {
          this.isRequesting = false;
          this.errors = error;
        });
   // }
  }

  



}
