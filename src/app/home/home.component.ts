import { Component, OnInit } from '@angular/core';
import { UserAuthService } from '../services/user.auth.service';
import { TestSerService } from "../services/test-ser.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private  userService: TestSerService) { }

  ngOnInit() {
  }

  testConnection(){
      //this.userService.testConnect();

       this.userService.do();
  }

}
