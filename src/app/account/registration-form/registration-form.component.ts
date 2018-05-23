import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserRegistration } from '../../models/user.registration';
import { UserAuthService } from '../../services/user.auth.service';
 

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss']
})
export class RegistrationFormComponent implements OnInit {
errors: string;  
  isRequesting: boolean;
  submitted: boolean = false;
  constructor(private userService: UserAuthService,private router: Router) { }

  ngOnInit() {
  }

   registerUser({ value, valid }: { value: UserRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors='';
    if(valid)
    {      
        this.userService.register(value.email,value.password,value.firstName,value.lastName,value.location)
                  .subscribe(
                    result  => {
                      this.isRequesting = false;
                      console.log(`register ${result}!`);
                      if(result){
                        this.router.navigate(['/login'], { queryParams: {brandNew: true, email: value.email}});
                    }
                  },
                  error => {
                      this.isRequesting = false;
                      this.errors = error;
                  });
    }
   } 
}
