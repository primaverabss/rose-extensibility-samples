import { SubscriptionComponent } from './../subscription/subscription.component'
import { Routes, RouterModule } from '@angular/router';

export const publicRoutes: Routes = [
    { path: '', component: SubscriptionComponent, pathMatch: 'full' }
];
