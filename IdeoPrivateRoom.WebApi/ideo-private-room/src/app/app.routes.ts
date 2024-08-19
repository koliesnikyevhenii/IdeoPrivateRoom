import { Routes } from '@angular/router';
import { eventListRoutes } from './event-list/event-list.routes';
import { CalendarComponent } from './calendar/calendar.component';

export const routes: Routes = [
    ...eventListRoutes,
    {
        path: '',
        redirectTo: 'calendar',
        pathMatch: 'full'
    },
    {
        path: 'calendar',
        component: CalendarComponent,
        title: 'Calendar'
    }
];
