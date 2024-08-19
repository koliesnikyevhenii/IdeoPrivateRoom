import { Routes } from '@angular/router';
import { EventListComponent } from './event-list.component';

export const eventListRoutes: Routes = [
  {
    path: 'event-list', // <your-domain>/users/<uid>/tasks
    component: EventListComponent,
    runGuardsAndResolvers: 'always'
  }
];