import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BlockUIModule } from 'ng-block-ui';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './../app/components/home/home.component'
import { SubscriptionComponent } from './../app/components/subscription/subscription.component';

import { EnvServiceProvider } from './env.service.provider';

import { OAuthModule } from 'angular-oauth2-oidc';
import { LoginService } from './services/login.services';
import { CoreService } from './services/core.service';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { HttpClientModule, HttpClient } from '@angular/common/http';

import { FormsModule } from '@angular/forms';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    SubscriptionComponent,
    HomeComponent
  ],
  imports: [
    BlockUIModule.forRoot(),
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    OAuthModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
      }
  })
  ],
  providers: [
    LoginService,
    CoreService,
    EnvServiceProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
