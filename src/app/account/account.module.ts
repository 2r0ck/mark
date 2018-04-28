import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginComponent } from './login/login.component';
import { routing } from './account.routing';

@NgModule({
  imports: [
    CommonModule,
    routing
  ],
  declarations: [RegistrationFormComponent, LoginComponent]
})
export class AccountModule { }
