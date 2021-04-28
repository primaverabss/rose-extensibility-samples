import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { EnvServiceProvider } from './env.service.provider';

import { OAuthModule } from 'angular-oauth2-oidc';
import { LoginService } from './services/login.services';

import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    OAuthModule.forRoot()
  ],
  providers: [
    LoginService,
    EnvServiceProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
