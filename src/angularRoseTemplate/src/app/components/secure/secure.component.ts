import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.services';

@Component({
  selector: 'rd-secure',
  templateUrl: './secure.component.html',
  styleUrls: ['./secure.component.css']
})
export class SecureComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  ngOnInit() {
  }

  public logout() {
    this.loginService.logout();
  }
}
