import { Component, OnInit } from '@angular/core';
import { LoginService } from './services/login.services';

@Component({
  selector: 'rd-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title:string = 'angularRoseDemo';
  private loggedIn: boolean = false;

  constructor(
    private loginService: LoginService
    ) {
  }

  ngOnInit(){

    this.initLoginService();
  }

  private initLoginService() {
      this.loginService.init();

      if (!this.loginService.loggedIn()) {
        this.loginService.login();
        this.loggedIn = true;
      }
      else {
        this.loggedIn = true;
      }
  }

  private login() {
    this.loginService.login();
    this.loggedIn = true;
  }

  private logout() {
    this.loginService.logout();
    this.loggedIn = false;
  }
}
