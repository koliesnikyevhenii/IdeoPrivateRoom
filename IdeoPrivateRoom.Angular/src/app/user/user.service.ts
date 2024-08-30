import { inject, Injectable, signal } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';

import { User } from './user.models';
import { environment } from '../../environments/environment';
import { catchError, map, Observable, tap, throwError, from, switchMap } from 'rxjs';
import { ApiUser } from '../api-models/user.model';
import { mapUser } from './user.mapping';
import { MsalService } from '@azure/msal-angular';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  private usersUrl = `${environment.apiUrl}/authcheck`;
  private http = inject(HttpClient);
  private msalService = inject(MsalService);

  private users = signal<User[]>([]);

  allUsers = this.users.asReadonly();

  loadUsers() {
    return this.fetchUsersWithAuth(
      'Something gone wrong trying to load users...'
    ).pipe(tap((users) => this.users.set(users)));
  }

  private fetchUsers(errorMessage: string): Observable<User[]> {
    return this.http.get<ApiUser[]>(this.usersUrl).pipe(
      map((users) => users.map(mapUser)),
      catchError(() => throwError(() => new Error(errorMessage)))
    );
  }

  private fetchUsersWithAuth(errorMessage: string) : Observable<User[]>  {
    return from(this.msalService.instance.acquireTokenSilent({
      scopes: environment.apiConfig.backscopes
    })).pipe(
      switchMap(response => {
        const headers = new HttpHeaders().set('Authorization', `Bearer ${response.accessToken}`);
        return this.http.get<ApiUser[]>(this.usersUrl, { headers }).pipe(
          map(users => users.map(mapUser)),
          catchError(() => throwError(() => new Error(errorMessage)))
        );
      }),
      catchError(error => {
        console.error('Token acquisition failed', error);
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
