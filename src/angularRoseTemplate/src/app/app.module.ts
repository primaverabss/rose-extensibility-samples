import { catchError } from 'rxjs/operators';
import { NgModule, Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { BlockUIModule } from 'ng-block-ui';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './../app/components/home/home.component';
import { SubscriptionComponent } from './../app/components/subscription/subscription.component';
import { PublicComponent } from './../app/components/public/public.component';
import { SecureComponent } from './../app/components/secure/secure.component';
import { CustomerComponent } from './../app/components/customer/customer.component';
import { CustomerDetailComponent } from './components/customer/customerdetail/customerdetail.component';
import { CustomerPendingDocsComponent } from './components/customer/customerpendingdocs/customerpendingdocs.component';
import { EnvServiceProvider } from './env.service.provider';

import { OAuthModule } from 'angular-oauth2-oidc';
import { LoginService } from './services/login.services';
import { CoreService } from './services/core.service';

import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClientModule, HttpClient } from '@angular/common/http';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    PublicComponent,
    SecureComponent,
    SubscriptionComponent,
    CustomerComponent,
    CustomerDetailComponent,
    CustomerPendingDocsComponent,
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
