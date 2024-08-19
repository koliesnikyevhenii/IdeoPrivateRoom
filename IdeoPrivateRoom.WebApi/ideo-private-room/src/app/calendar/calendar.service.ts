import { Observable } from 'rxjs';
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toObservable } from '@angular/core/rxjs-interop';
import { isEqual } from 'date-fns';

import { colors, EventModel, EventStatus } from './calendar.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);


  private events = signal<EventModel[]>([
    {
      title: 'Draggable event',
      color: colors['yellow'],
      start: new Date(),
      end: new Date(),
      id: '1',
      userId: '1',
      status: EventStatus.Pending,
    },
    {
      title: 'A non draggable event',
      color: colors['blue'],
      start: new Date(),
      end: new Date(),
      id: '2',
      userId: '2',
      status: EventStatus.Confirmed,
    },
  ]);

  allEvents = toObservable(this.events)

  fetchAllEvents(): Observable<EventModel[]>{
    return this.http.get<EventModel[]>(this.baseUrl + '/vocations');
  }

  addEvent(userId: string, title: string, start: Date, end: Date) {
    const sameDayEvent = isEqual(start, end)
    this.events.update((events) => [
      ...events,
      {
        id: crypto.randomUUID(),
        userId: userId,
        title: title,
        start: start,
        end: sameDayEvent ? undefined : end,
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
