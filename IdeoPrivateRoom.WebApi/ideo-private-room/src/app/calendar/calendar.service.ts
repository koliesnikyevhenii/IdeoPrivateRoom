import { Injectable, signal } from '@angular/core';
import { colors, EventModel, EventStatus } from './calendar.models';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  private events = signal<EventModel[]>([
    {
      title: 'Draggable event',
      color: colors['yellow'],
      start: new Date(),
      id: '1',
      userId: '1',
      status: EventStatus.Pending,
    },
    {
      title: 'A non draggable event',
      color: colors['blue'],
      start: new Date(),
      id: '2',
      userId: '2',
      status: EventStatus.Pending,
    },
  ]);

  allEvents = this.events.asReadonly();

  addEvent(userId: string, title: string, start: Date, end: Date) {
    this.events.update((events) => [
      ...events,
      {
        id: crypto.randomUUID(),
        userId: userId,
        title: title,
        start: start,
        end: end,
        status: EventStatus.Pending,
      },
    ]);
  }

  deleteEvent(id: string) {
    this.events.update((items) => items.filter((item) => item.id !== id));
  }

  updateEvent(id: string, title: string, start: Date, end: Date) {
    this.events.update((items) =>
      items.map((item) =>
        item.id == id ? { ...item, title: title, start: start, end: end } : item
      )
    );
  }

  getEvent(id: string): EventModel | undefined {
    return this.events().find((event) => event.id === id);
  }
}
