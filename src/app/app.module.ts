import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { BusyComponent } from './busy/busy.component';
import { routing } from './app.routing';
import { AccountModule } from './account/account.module';
import { NotfoundComponent } from './notfound/notfound.component';
import { FormsModule } from '@angular/forms';
 



@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    BusyComponent,
    NotfoundComponent
  
  ],
  imports: [
    BrowserModule,
    routing,
    AccountModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
