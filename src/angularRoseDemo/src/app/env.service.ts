export class EnvService {

  // The values that are defined here are the default values that can
  // be overridden by env.js
  public issuer = 'https://stg-identity.primaverabss.com;'
  public loginurl = 'https://stg-identity.primaverabss.com/connect/authorize';
  public logouturl = 'https://stg-identity.primaverabss.com/connect/endsession';
  public clientId = 'rose-excel-client-app';
  public scope = 'rose-api';
  public redirect_uri = 'https://localhost:4200/';
  public app_uri = 'http://app.rose.primaverabss.com';

  constructor() {
  }

}
