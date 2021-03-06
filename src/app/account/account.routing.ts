import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RegistrationFormComponent } from './registration-form/registration-form.component';
import {  LoginComponent } from './login/login.component';
import { FacebookLoginComponent } from "./facebook-login/facebook-login.component";


export const routing: ModuleWithProviders = RouterModule.forChild([
  { path: 'register', component: RegistrationFormComponent},
  { path: 'login', component: LoginComponent},
   { path: 'facebook-login', component: FacebookLoginComponent}
]);
