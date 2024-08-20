import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ApiEvent } from '../api-models/event';
import { EventApproval, EventModel, EventStatus } from './event-list.models';

@Injectable({
  providedIn: 'root',
})
export class EventListService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);

  private events = signal<EventModel[]>([]);

  loadedEvents = this.events.asReadonly();

  loadEvents() {
    return this.fetchEvents(
      'Something gone wrong trying to load events...'
    ).pipe(
      tap({
        next: (events) => this.events.set(events),
      })
    );
  }

  addEvent(
    userId: string,
    start: Date,
    end: Date,
    comment: string | undefined
  ) {
    // const sameDayEvent = isEqual(start, end);
    // this.events.update((events) => [
    //   ...events,
    // {
    //   id: crypto.randomUUID(),
    //   userId: userId,
    //   title: title,
    //   start: start,
    //   end: sameDayEvent ? undefined : end,
    //   status: EventStatus.Pending,
    // },
    // ]);
  }

  //   deleteEvent(id: string) {
  //     this.events.update((items) => items.filter((item) => item.id !== id));
  //   }

  //   updateEvent(id: string, title: string, start: Date, end: Date) {
  //     this.events.update((items) =>
  //       items.map((item) =>
  //         item.id == id ? { ...item, title: title, start: start, end: end } : item
  //       )
  //     );
  //   }

  //   getEvent(id: string): EventModel | undefined {
  //     return this.events().find((event) => event.id === id);
  //   }

  mapColorToEventStatus(status: number | undefined) {
    switch (status) {
      case EventStatus.Confirmed:
        return {
          primary: '#28a745',
          secondary: '#28a745',
        };
      case EventStatus.Declined:
        return {
          primary: '#dc3545',
          secondary: '#dc3545',
        };
      default:
        return {
          primary: '#ffc107',
          secondary: '#ffc107',
        };
    }
  }

  private fetchEvents(errorMessage: string): Observable<EventModel[]> {
    return this.http.get<ApiEvent[]>(this.baseUrl + '/vocations').pipe(
      map((resData) =>
        resData.map((event) => {
          return <EventModel>{
            id: event.id,
            userId: event.user.id,
            userName: event.user.name,
            userIcon: event.user.icon,
            status: this.getEventStatusByValue(event.status),
            start: new Date(event.start),
            end: event.end ? new Date(event.end) : undefined,
            userApprovalResponses: event.userApprovalResponses.map(
              (approval) =>
                <EventApproval>{
                  id: approval.id,
                  userId: approval.user.id,
                  userName: approval.user.name,
                  userIcon: approval.user.icon,
                  approvalStatus: this.getEventStatusByValue(
                    approval.approvalStatus
                  ),
                }
            ),
          };
        })
      ),
      catchError((error) => {
        console.log(error);
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  private getEventStatusByValue(
    statusCode: number
  ): keyof typeof EventStatus | undefined {
    return Object.keys(EventStatus).find(
      (key) => EventStatus[key as keyof typeof EventStatus] === statusCode
    ) as keyof typeof EventStatus | undefined;
  }
}
