import { ApiUser } from '../api-models/user.model';
import { User } from './user.models';

export function mapUser(user: ApiUser): User {
  return <User>{
    id: user.id,
    name: user.name,
    icon: user.icon,
  };
}
