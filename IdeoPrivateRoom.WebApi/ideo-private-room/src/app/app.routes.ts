import { Routes } from '@angular/router';
import { eventListRoutes } from './event-list/event-list.routes';
import { CalendarComponent } from './calendar/calendar.component';
import {LoginComponent} from  './login/login.component';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    ...eventListRoutes,
    {
        path: '',
        redirectTo: 'calendar',
        pathMatch: 'full',
      //  canActivate: [MsalGuard]
    },
    {
        path: 'calendar',
        component: CalendarComponent,
        title: 'Calendar'
         //  canActivate: [MsalGuard]
    },
    {
        path: 'login',
        component: LoginComponent,
        title: 'Login'
         //  canActivate: [MsalGuard]
    }
];
