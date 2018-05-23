import { Component, OnInit } from '@angular/core';
import { UserAuthService } from '../services/user.auth.service';
import { TestSerService } from '../services/test-ser.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private  userService: UserAuthService) { }

  ngOnInit() {
  }

  testConnection(){
      //this.userService.testConnect();

       //this.userService.testConnect().subscribe(res=>console.log(res));
       this.userService.testConnect().subscribe(res=>console.log(`test: ${res.Value}`));
  }

  testConnection2(){
      //this.userService.testConnect();

       //this.userService.testConnect().subscribe(res=>console.log(res));
       this.userService.testConnectView().subscribe(res=>console.log(`test pro: ${res.Value}`));
  }

   testConnection3(){
      //this.userService.testConnect();

       //this.userService.testConnect().subscribe(res=>console.log(res));
       this.userService.testConnectApi().subscribe(res=>console.log(`test pro: ${res.Value}`));
  }

}
