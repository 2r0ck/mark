import { Component, OnInit } from '@angular/core';
import { UserAuthService } from "../../services/user.auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-facebook-login',
  templateUrl: './facebook-login.component.html',
  styleUrls: ['./facebook-login.component.scss']
})
export class FacebookLoginComponent {

  private authWindow: Window;
  failed: boolean;
  error: string;
  errorDescription: string;
  isRequesting: boolean;

  launchFbLogin() {
    //this.authWindow = window.open('https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=1528751870549294&display=popup&redirect_uri=http://localhost:5000/facebook-auth.html&scope=email',null,'width=600,height=400');    
    //todo: что делать со строкой
    let url = 'https://accounts.google.com/o/oauth2/auth?client_id=313078532206-b01t045uahrop8264g97jrnt1j2dmbrt.apps.googleusercontent.com&redirect_uri=http://localhost:5000/facebook-auth.html&scope=profile+email&response_type=code&access_type=offline';
    this.authWindow = window.open(url, null, 'width=600,height=400');

    //https://accounts.google.com/o/oauth2/token?code=&client_id=&client_secret=&redirect_uri=http://localhost:5000/facebook-auth.html&grant_type=authorization_code

    //GmailService.Scope.GmailReadonly

   // https://accounts.google.com/o/oauth2/auth?client_id=313078532206-b01t045uahrop8264g97jrnt1j2dmbrt.apps.googleusercontent.com&redirect_uri=http://localhost:5000/facebook-auth.html&scope=profile&response_type=code&access_type=offline

  }

  constructor(private userService: UserAuthService, private router: Router) {
    if (window.addEventListener) {
      window.addEventListener("message", this.handleMessage.bind(this), false);
    } else {
      (<any>window).attachEvent("onmessage", this.handleMessage.bind(this));
    };
  }

  handleMessage(event: Event) {
    const message = event as MessageEvent;
    // Only trust messages from the below origin.
    //todo: что делать с адресом?
    if (message.origin !== "http://localhost:5000") return;

    this.authWindow.close();

    const result = JSON.parse(message.data);
    if (!result.status) {
      this.failed = true;
      this.error = result.error;
      this.errorDescription = result.errorDescription;
    }
    else {
      this.failed = false;
      this.isRequesting = true;

      this.userService.facebookLogin(result.accessToken)

        .subscribe(
          res => {
            this.isRequesting = false;
            if (res) {
              this.router.navigate(['/home']);
            }
          },
          error => {
            this.isRequesting = false;
            this.failed = true;
            this.error = error;
          });
    }
  }
}
