import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AccountDetailComponent } from './accounts/account-detail/account-detail.component';
import { AccountListComponent } from './accounts/account-list/account-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AccountDetailComponent,
    AccountListComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
