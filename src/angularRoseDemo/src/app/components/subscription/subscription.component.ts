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
      this.writeAccountDataToStorage();
      this.router.navigate(['/home']);
  }

  private writeAccountDataToStorage()
  {
    let accountCode:string = 'account_code';
    let subscriptionCode:string = 'subscription_code';

    localStorage.removeItem(accountCode);
    localStorage.setItem(accountCode, this.account);

    localStorage.removeItem(subscriptionCode);
    localStorage.setItem(subscriptionCode, this.subscription);
  }

  private accountDataExistOnStorage():boolean
  {
    var accountValue = localStorage.getItem('account_code');
    var subscriptionValue = localStorage.getItem('subscription_code');

    this.account = accountValue!;
    this.subscription = subscriptionValue!;

    if (accountValue != null && subscriptionValue != null )
    {
      return true;
    }
    else{
      return false;
    }

  }
}
