import { Customer } from './../../models/customer';
import { Component, OnInit } from "@angular/core";
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { CoreService } from './../../services/core.service';

@Component({
  selector: 'rd-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  private customerOdata: string ='/odata?$select=PartyKey,Name,Country&$orderby=PartyKey asc';
  private moduleUri: string = "salescore/customerparties";

  public customers: Customer[] = [];

  constructor(
    private coreService: CoreService,
  ) {}

  ngOnInit(): void {
      this.getCustomers().subscribe({
        next: odata => this.customers = odata});
  }

  getCustomers(): Observable<any> {

    let apiEndpoint = this.moduleUri + this.customerOdata;

    return this.coreService.get<any>(apiEndpoint)
        .pipe(
          map(odata => odata.items)
        );
  }

}

