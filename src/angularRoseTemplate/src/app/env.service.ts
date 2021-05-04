export class EnvService {

  // The values that are defined here are the default values that can
  // be overridden by env.js
  public issuer = 'https://identity.primaverabss.com;'
  public loginurl = 'https://identity.primaverabss.com/connect/authorize';
  public logouturl = 'https://identity.primaverabss.com/connect/endsession';
  public clientId = 'ANGULARAPP';
  public scope = 'rose-api';
  public redirect_uri = 'http://localhost:4200/index.html';
  public app_uri = 'https://app.rose.primaverabss.com';

  constructor() {
  }

}
