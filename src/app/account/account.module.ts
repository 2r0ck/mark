import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginComponent } from './login/login.component';
import { routing } from './account.routing';
import { FormsModule } from '@angular/forms';
import { SpinnerComponent } from "../spinner/spinner.component";
import { FacebookLoginComponent } from './facebook-login/facebook-login.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    routing    
  ],
  declarations: [RegistrationFormComponent, LoginComponent, SpinnerComponent, FacebookLoginComponent]
})
export class AccountModule { }
