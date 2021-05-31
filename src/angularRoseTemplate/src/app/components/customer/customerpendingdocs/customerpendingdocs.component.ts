import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CoreService } from '../../../services/core.service';
import { CustomerPendingDocs } from '../../../models/customer-pending-docs';

@Component({
  selector: 'rd-customerpendingdocs',
  templateUrl: './customerpendingdocs.component.html',
  styleUrls: ['./customerpendingdocs.component.css']
})
export class CustomerPendingDocsComponent implements OnInit {

  private documentFiler: string = '';
  private moduleUri: string = '';
  private companyKey: string ='';
  public partyKey: string = "";

  // List of pending doc's.
  public pendingdocs: CustomerPendingDocs[] = [];

  constructor(
    private coreService: CoreService,
    private activatedroute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // The the paramter from route.
    // Use the Snapshot option, if you only need the initial value.
    this.partyKey = this.activatedroute.snapshot.paramMap.get("partykey")!;
    this.companyKey = localStorage.getItem('company_code')!;

    this.documentFiler =`?party=${this.partyKey}&company=${this.companyKey}&documentDate=2021-05-27&currency=EUR&documentExchangeRate=1&documentType=REC`;
    this.moduleUri = "accountsreceivable/processOpenItems/1/10";

    this.getDocuments().subscribe({
      next: response => this.pendingdocs = response});
  }

  getDocuments(): Observable<any> {

    let apiEndpoint = this.moduleUri + this.documentFiler;

    return this.coreService.get<any>(apiEndpoint)
      .pipe(
        map(response => response)
      );
  }

}
