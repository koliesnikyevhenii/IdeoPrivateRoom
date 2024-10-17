import { BehaviorSubject, catchError, map, tap, throwError } from 'rxjs';
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ApiEvent, ApiPaginatedResponse } from '../api-models/event.model';
import { EventStatus, FetchEventsParams, ViewMode } from './event-list.models';
import { mapEvent } from './event-list.mapping';
import { EventFilters } from './event-filters/event-filters.models';

@Injectable({
  providedIn: 'root',
})
export class EventListService {
  private http = inject(HttpClient);
  private eventsUrl = `${environment.apiUrl}/vacations`;
  private eventFilters = signal<EventFilters | null>(null);
  page = signal<number>(1);

  readonlyEventFilters = this.eventFilters.asReadonly();

  private currentViewMode = signal<ViewMode>(ViewMode.Table);

  readonlyViewMode = this.currentViewMode.asReadonly();

  refetchTrigger$ = new BehaviorSubject<void>(undefined);

  changeViewMode(mode: ViewMode) {
    this.currentViewMode.set(mode);
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
        this.refetchTrigger$.next();
      }),
      catchError(() => {
        console.log('failure');
        return throwError(() => new Error('Failed to add a new event.'));
      })
    );
  }

  deleteEvent(eventId: string) {
    return this.http.delete(`${this.eventsUrl}/${eventId}`).pipe(
      tap(() => {
        this.refetchTrigger$.next();
      }),
      catchError(() => {
        return throwError(() => new Error('Failed to delete an event.'));
      })
    );
  }

  updateEventStatus(userId: string, vacationId: string, status: string) {
    const body = {
      userId: userId,
      vacationId: vacationId,
      status: status,
    };

    return this.http.post(`${this.eventsUrl}/status`, body).pipe(
      tap(() => {
        this.refetchTrigger$.next();
      }),
      catchError(() => {
        return throwError(
          () =>
            new Error(
              `Failed to update event status. EventID:${vacationId}; UserID:${userId}.`
            )
        );
      })
    );
  }

  mapColorToEventStatus(status?: number): {
    primary: string;
    secondary: string;
  } {
    const statusColors: Record<number, { primary: string; secondary: string }> =
      {
        [EventStatus.Approved]: { primary: '#28a745', secondary: '#28a745' },
        [EventStatus.Declined]: { primary: '#dc3545', secondary: '#dc3545' },
      };

    return (
      statusColors[status ?? -1] || { primary: '#ffc107', secondary: '#ffc107' }
    );
  }

  setEventFilters(filters: EventFilters | null) {
    this.page.set(1);
    this.eventFilters.set(filters);
  }

  clearFilters() {
    this.eventFilters.set(null);
  }

  fetchEvents(params: FetchEventsParams) {
    const { page, pageSize, statuses, userIds, startDate, endDate } = params;
    let queryParams = new HttpParams();

    if (page != null) {
      queryParams = queryParams.set('page', page);
    }

    if (pageSize != null) {
      queryParams = queryParams.set('pageSize', pageSize);
    }

    if (statuses && statuses?.length > 0) {
      queryParams = queryParams.set('statuses', statuses.join(','));
    }

    if (userIds && userIds?.length > 0) {
      queryParams = queryParams.set('userIds', userIds.join(','));
    }

    if (startDate != null) {
      queryParams = queryParams.set('startDate', startDate.toISOString());
    }

    if (endDate != null) {
      queryParams = queryParams.set('endDate', endDate.toISOString());
    }

    return this.http
      .get<ApiPaginatedResponse<ApiEvent>>(this.eventsUrl, {
        params: queryParams,
      })
      .pipe(
        map((response) => {
          return {
            ...response,
            data: response.data.map(mapEvent),
          };
        }),
        catchError((error) => {
          console.error(error);
          return throwError(() => {
            return new Error('Something went wrong during fetching events...');
          });
        })
      );
  }
}
