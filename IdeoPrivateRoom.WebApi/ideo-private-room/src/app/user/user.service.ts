import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from './user.models';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  private users = signal<User[]>([
    {
      id: '1',
      icon: '',
      name: 'John',
    },
    {
      id: '2',
      icon: '',
      name: 'Mike',
    },
    {
      id: '3',
      icon: '',
      name: 'Steve',
    },
  ]);

  allUsers = this.users.asReadonly();

  allUser(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/users');
  }

  getUserById(userId: string): Observable<User> {
    return this.http.get<User>(this.baseUrl + '/users/' + userId);
  }

  getUser(userId: string) {
    return this.users().find((f) => f.id === userId);
  }
}
