import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ApiEvent } from '../api-models/event.model';
import { EventModel, EventStatus, ViewMode } from './event-list.models';
import { mapEvent } from './event-list.mapping';

@Injectable({
  providedIn: 'root',
})
export class EventListService {
  private http = inject(HttpClient);
  private eventsUrl = `${environment.apiUrl}/vocations`;

  private events = signal<EventModel[]>([]);

  loadedEvents = this.events.asReadonly();

  private currentViewMode = signal<ViewMode>(ViewMode.Table);

  readonlyViewMode = this.currentViewMode.asReadonly();

  changeViewMode(mode: ViewMode) {
    this.currentViewMode.set(mode)
  }

  loadEvents() {
    return this.fetchEvents(
      'Something gone wrong trying to load events...'
    ).pipe(tap((events) => this.events.set(events)));
  }

  createEvent(
    userId: string,
    startDate: Date,
    endDate: Date,
    comment?: string | null
  ) {
    const eventPayload = {
      userId: userId,
      startDate: startDate.toISOString(),
      endDate: endDate.toISOString(),
    };

    return this.http.post(this.eventsUrl, eventPayload).pipe(
      tap(() => {
        this.refreshEvents('Failed to fetch events after adding one.');
      }),
      catchError(() => {
        console.log('failure');
        return throwError(() => new Error('Failed to add a new event.'));
      })
    );
  }

  deleteEvent(eventId: string) {
    const prevEvents = this.events();

    if (prevEvents.some((event) => event.id === eventId)) {
      this.events.set(prevEvents.filter((event) => event.id !== eventId));
    }

    return this.http.delete(`${this.eventsUrl}/${eventId}`).pipe(
      catchError(() => {
        this.events.set(prevEvents);
        return throwError(() => new Error('Failed to delete an event.'));
      })
    );
  }

  fetchEvents(errorMessage: string): Observable<EventModel[]> {
    return this.http.get<ApiEvent[]>(this.eventsUrl).pipe(
      map((events) => {
        return events.map(mapEvent);
      }),
      catchError((error) => {
        console.error(error);
        return throwError(() => {
          return new Error(errorMessage);
        });
      })
    );
  }

  private refreshEvents(errorMessage: string): void {
    this.fetchEvents(errorMessage).subscribe({
      next: (events) => this.events.set(events),
      error: (err) => console.error(err),
    });
  }
}
