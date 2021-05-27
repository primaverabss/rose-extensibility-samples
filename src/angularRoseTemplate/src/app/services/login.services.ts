import { productConfig } from '../product.config';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpClientModule } from '@angular/common/http';

@Injectable()
export class LoginService {

    constructor(
      private httpClientModule: HttpClientModule,
      private oAuthService: OAuthService) {
    }

    public init() {

        // oauth config
        this.oAuthService.loginUrl = productConfig.loginurl;
        this.oAuthService.redirectUri = productConfig.redirectUri;
        this.oAuthService.clientId = productConfig.clientId;
        this.oAuthService.scope = productConfig.scope;
        this.oAuthService.issuer = productConfig.issuer;
        this.oAuthService.logoutUrl = productConfig.logouturl;
        this.oAuthService.logoutUrl = productConfig.logouturl+ "?id_token_hint={{id_token}}&post_logout_redirect_uri=" + productConfig.redirectUri;

        this.oAuthService.setStorage(sessionStorage);
        this.oAuthService.oidc = true;

        this.oAuthService.tryLogin({});
    }

    public loggedIn(): boolean {
        let hasIdToken = this.oAuthService.hasValidIdToken();
        let hasAccessToken = this.oAuthService.hasValidAccessToken();

        return (hasIdToken && hasAccessToken);
    }

    public login() {

        // validate access - are tokens valid?
        let hasIdToken = this.oAuthService.hasValidIdToken();
        let hasAccessToken = this.oAuthService.hasValidAccessToken();

        if (!hasIdToken || !hasAccessToken) {
            this.oAuthService.initImplicitFlow();
        }
    }

    public logout() {
        let hasIdToken = this.oAuthService.hasValidIdToken();
        let hasAccessToken = this.oAuthService.hasValidAccessToken();

        if (hasIdToken || hasAccessToken) {
            this.oAuthService.logOut();
        }
    }
}
