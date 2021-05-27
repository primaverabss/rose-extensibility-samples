import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';

@Component({
  selector: 'rd-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.css']
})
export class SubscriptionComponent implements OnInit {

  private _account: string ='';
  private _subscription: string ='';
  private _company: string ='';

  get account(): string{
      return this._account;
  }

  set account(value: string){
    this._account = value;
  }

  set subscription(value: string){
    this._subscription = value;
  }

  get subscription(): string{
    return this._subscription;
  }

  set company(value: string){
    this._company = value;
  }

  get company(): string{
    return this._company;
  }

  constructor(
    private router: Router
  ) { }

  ngOnInit(){

    if (this.accountDataExistOnStorage()){
      this.router.navigate(['/home']);
    }
  }

  goToApplication(): void
  {
    if (this.account != null && this.subscription != null && this.company != null)
    {
      this.writeAccountDataToStorage();
      this.router.navigate(['/home']);
    }
  }

  private writeAccountDataToStorage()
  {
    let accountCode:string = 'account_code';
    let subscriptionCode:string = 'subscription_code';
    let company:string = 'company_code';

    localStorage.removeItem(accountCode);
    localStorage.setItem(accountCode, this.account);

    localStorage.removeItem(subscriptionCode);
    localStorage.setItem(subscriptionCode, this.subscription);

    localStorage.removeItem(company);
    localStorage.setItem(company, this.company);
  }

  private accountDataExistOnStorage():boolean
  {
    this.account = localStorage.getItem('account_code')!;
    this.subscription = localStorage.getItem('subscription_code')!;
    this.company = localStorage.getItem('company_code')!;

    if (this.account != null || this.subscription != null )
    {
      return true;
    }
    else{
      return false;
    }

  }
}
