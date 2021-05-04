import { Injectable } from '@angular/core';
import { HttpHeaders, HttpParams, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { productConfig } from '../product.config';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { TranslateService } from '@ngx-translate/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  // providedIn: 'root', means that the CoreService is visible throughout the application.
  providedIn: 'root'
})
export class CoreService {

  @BlockUI()
  loading!: NgBlockUI;

  private RES_Loading: string = '';

  constructor(
    private http: HttpClient,
    private oAuthService: OAuthService,
    private translateService: TranslateService)
    { this.translateService.get("RES_Loading").subscribe(result => {
      this.RES_Loading = result;})
    }

  public get<T>(
    url: string,
    parameters?: HttpParams): Observable<any> {

    let tenantKey = this.getTenantKey();
    let organizationKey = this.getOrganizationKey();
    let apiUri = `/api/${tenantKey}/${organizationKey}/${url}`;

    return this.getCore<T>(apiUri, parameters);
  }

  public post<T>(
    url: string,
    body: any,
    parameters?: HttpParams): Observable<any> {

    let tenantKey = this.getTenantKey();
    let organizationKey = this.getOrganizationKey();
    let apiUri = `/api/${tenantKey}/${organizationKey}/${url}`;

    return this.postCore<T>(apiUri, body, parameters);
  }

  public put<T>(
    url: string,
    body: number,
    parameters?: HttpParams): Observable<any> {

    let tenantKey = this.getTenantKey();
    let organizationKey = this.getOrganizationKey();
    let apiUri = `/api/${tenantKey}/${organizationKey}/${url}`;

    return this.putCore<T>(apiUri, body, parameters);
  }

  public getFromRoot<T>(
    url: string,
    parameters?: HttpParams): Observable<any> {
      return this.getCore<T>(url, parameters);
  }

  private getCore<T>(
    url: string,
    parameters?: HttpParams): Observable<any> {

    this.loading.start(this.RES_Loading);

    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .append('Authorization', this.oAuthService.getAccessToken())
      .append('Cache-Control', 'no-cache')
      .append('Pragma', 'no-cache')
      .append('Expires', '-1');

    return this.http.get(productConfig.appUri + url, { headers: headers, params: parameters })
        .pipe(
           retry(3), // retry a failed request up to 3 times
           catchError(this.handleError)
        )
  }

  private putCore<T>(
      url: string,
      body: number,
      parameters?: HttpParams): Observable<any> {

      this.loading.start(this.RES_Loading);

      const headers = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .append('Authorization', this.oAuthService.getAccessToken())
        .append('Cache-Control', 'no-cache')
        .append('Pragma', 'no-cache')
        .append('Expires', '-1');

      return this.http.put<T>(productConfig.appUri + url, body, { headers: headers, params: parameters })
  }

  private postCore<T>(
      url: string,
      body: any,
      parameters?: HttpParams): Observable<any> {

      this.loading.start(this.RES_Loading);

      const headers = new HttpHeaders()
        .set('Content-Type', 'application/json')
        .append('Authorization', this.oAuthService.getAccessToken())
        .append('Cache-Control', 'no-cache')
        .append('Pragma', 'no-cache')
        .append('Expires', '-1');

      return this.http.post(productConfig.appUri + url, body, { headers: headers, params: parameters })
  }

  public getTenantKey(): string {
      return localStorage.getItem('account_code')!;
  }

  public getOrganizationKey(): string {
      return localStorage.getItem('subscription_code')!;
  }

  private handleError(error: HttpErrorResponse) {

    if (error.error instanceof ErrorEvent) {
        // A client-side or network error occurred. Handle it accordingly.
        console.error('An error occurred:', error.error.message);
    } else {
        // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong,
        console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
    }

    // return an observable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  };

  public getJSON(jsonPath: string, parameters?: HttpParams) : Observable<any> {
      return this.http.get(jsonPath, { params: parameters })
      .pipe(catchError(this.handleError));
  }
}
