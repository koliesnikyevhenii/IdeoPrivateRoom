import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from './user.models';
import { environment } from '../../environments/environment';
import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { ApiUser } from '../api-models/user.model';
import { mapUser } from './user.mapping';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private usersUrl = `${environment.apiUrl}/users`;
  private http = inject(HttpClient);

  private users = signal<User[]>([]);

  allUsers = this.users.asReadonly();

  loadUsers() {
    return this.fetchUsers(
      'Something gone wrong trying to load users...'
    ).pipe(tap((users) => this.users.set(users)));
  }

  private fetchUsers(errorMessage: string): Observable<User[]> {
    return this.http.get<ApiUser[]>(this.usersUrl).pipe(
      map((users) => users.map(mapUser)),
      catchError(() => throwError(() => new Error(errorMessage)))
    );
  }

  // TODO: add user interaction functionality
  // private refreshUsers(errorMessage: string): void {
  //   this.fetchUsers(errorMessage).subscribe({
  //     next: (users) => this.users.set(users),
  //     error: (err) => console.error(err),
  //   });
  // }
}
