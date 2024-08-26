import { CalendarEvent } from 'angular-calendar';
import { EventStatus } from '../event-list/event-list.models';

export interface CalendarUserEvent extends CalendarEvent {
  userName: string;
  status: EventStatus;
}

export interface CalendarSlots {
  firstEvent: CalendarUserEvent | null;
  moreEvents: CalendarUserEvent[];
}
