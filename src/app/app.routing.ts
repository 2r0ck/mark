import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';

const appRoutes: Routes = [
  { path: 'home',  component: HomeComponent },
  { path: '',  redirectTo: '/home', pathMatch: 'full' },   // root address
  { path: '**',  component: NotfoundComponent }  // notfound address
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
