import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import {CustomerDetail} from '../../../models/customer-detail'
import { CoreService } from '../../../services/core.service';

@Component({
  selector: 'rd-customer-detail',
  templateUrl: './customerdetail..component.html',
  styleUrls: ['./customerdetail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  private moduleUri: string = '';
  public isNew: boolean = false;
  public partyKey: string = "";

  customerDetail: CustomerDetail = new CustomerDetail();

  constructor(
    private coreService: CoreService,
    private router: Router,
    private activatedroute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // The the paramter from route.
    // Use the Snapshot option, if you only need the initial value.
    this.partyKey = this.activatedroute.snapshot.paramMap.get("partykey")!;

    this.moduleUri = `/businessCore/parties/${this.partyKey}`;

    this.getCustomerDetail().subscribe({
      next: response => this.customerDetail = response});
  }

  getCustomerDetail(): Observable<CustomerDetail> {

    let apiEndpoint = this.moduleUri;

    return this.coreService.get<CustomerDetail>(apiEndpoint)
      .pipe(
        map((response: CustomerDetail) => response)
      );
  }

  cancel() {
    this.router.navigate(['/customers']);
  }

  saveCustomer() {

  }
}
