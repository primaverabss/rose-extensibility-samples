import { HomeComponent } from './../home/home.component';
import { CustomerComponent } from './../customer/customer.component';
import { CustomerDetailComponent } from '../customer/customerdetail/customerdetail.component';
import { CustomerPendingDocsComponent } from '../customer/customerpendingdocs/customerpendingdocs.component';
import { Routes } from '@angular/router';

export const secureRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'customers', component: CustomerComponent },
    { path: 'customersdetail/:partykey', component: CustomerDetailComponent },
    { path: 'pendingdocs/:partykey', component: CustomerPendingDocsComponent }
]
