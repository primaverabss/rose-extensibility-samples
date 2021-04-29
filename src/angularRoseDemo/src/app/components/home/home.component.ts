import { Component } from "@angular/core";
import { Router } from '@angular/router';

@Component({
  selector: 'rd-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent{

  constructor(
    private router: Router
  ) { }

  goToApplication(): void
  {

  }
}
