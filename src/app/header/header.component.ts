
import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserAuthService } from "../services/user.auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  status: boolean;
  subscription: Subscription;

  constructor(private userService: UserAuthService) {
  }

  logout() {
    this.userService.logout();
  }

  ngOnInit() {
    this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);
  }

  ngOnDestroy() {
    // prevent memory leak when component is destroyed
    this.subscription.unsubscribe();
  }
}
