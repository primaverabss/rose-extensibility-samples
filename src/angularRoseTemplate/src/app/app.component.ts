import { Component, OnInit } from '@angular/core';
import { LoginService } from './services/login.services';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'rd-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title:string = 'Angular Rose Demo Application';
  private loggedIn: boolean = false;

  constructor(
    private loginService: LoginService,
    translate: TranslateService)
    {
      // this language will be used as a fallback when a translation isn't found in the current language
      translate.setDefaultLang('pt');
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
        this.loggedIn = false;
      }
  }

  private login() {
    this.loginService.login();
    this.loggedIn = true;
  }
}
