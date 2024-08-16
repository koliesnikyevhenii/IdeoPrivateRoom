import { Injectable, signal } from '@angular/core';
import { User } from './user.models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private users = signal<User[]>([
    {
      id: '1',
      icon: '',
      name: 'John'
    },
    {
      id: '2',
      icon: '',
      name: 'Mike'
    },
    {
      id: '3',
      icon: '',
      name: 'Steve'
    },
  ])

  allUsers = this.users.asReadonly()

  getUser(userId: string) {
    return this.users().find(f => f.id === userId)
  }
}
