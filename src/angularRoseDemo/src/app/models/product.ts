export class ProductConfig {

  production: string ='';
  // The identity Server https://demo.identityserver.com/
  issuer: string ='';

  // Login Url of the Identity Provider https://demo.identityserver.com/connect/authorize',
  loginurl: string='' ;

  // Login Url of the Identity Provider  https://demo.identityserver.com/connect/endsession,
  logouturl: string='' ;

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: string='' ;

  // set the scope for the permissions the client should request
  scope: string='' ;

  // The application url
  redirectUri : string ='';

  // The ROSE url
  appUri: string ='';

  constructor() {
  }

}
