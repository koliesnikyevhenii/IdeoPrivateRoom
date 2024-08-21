import { inject, Injectable, signal } from '@angular/core';
import { EventListService } from '../event-list.service';
import { EventFilters } from './event-filters.models';
import { EventModel, EventStatus } from '../event-list.models';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EventFiltersService {
  private eventsListService = inject(EventListService);
  private eventFilters = signal<EventFilters | null>(null);
  private filteredEvents = signal<EventModel[]>([]);

  loadedEvents = this.filteredEvents.asReadonly();

  readonlyEventFilters = this.eventFilters.asReadonly();

  setEventFilters(filters: EventFilters | null) {
    this.eventFilters.set(filters);
    this.loadFilteredEvents().subscribe();
  }

  loadFilteredEvents(): Observable<void> {
    return this.eventsListService
      .fetchEvents('Something gone wrong trying to load filtered events...')
      .pipe(
        map((events) => this.filteredEvents.set(this.filterEvents(events)))
      );
  }

  clearFilters() {
    this.eventFilters.set(null);
    this.loadFilteredEvents().subscribe();
  }

  private filterEvents(events: EventModel[]): EventModel[] {
    return events.filter((event) => {
      const employee = this.eventFilters()?.employee;
      const status = this.eventFilters()?.status;
      const dates = this.eventFilters()?.dates;

      // Check employee filter
      if (employee && employee.length > 0 && !employee.map(m => m.id).includes(event.userId)) {
        return false;
      }

      // Check status filter
      if (
        status &&
        status.length > 0 &&
        !status.includes(EventStatus[event.status])
      ) {
        return false;
      }

      // Check date filter
      if (dates) {
        const { fromDate, toDate } = dates;
        const eventStart = new Date(event.fromDate);
        const eventEnd = event.toDate ? new Date(event.toDate) : eventStart;

        const from = fromDate
          ? new Date(fromDate.year, fromDate.month - 1, fromDate.day)
          : null;
        const to = toDate
          ? new Date(toDate.year, toDate.month - 1, toDate.day)
          : null;

        if (from && to) {
          if (eventEnd < from || eventStart > to) {
            return false;
          }
        } else if (from) {
          if (eventEnd < from && eventStart < from) {
            return false;
          }
        } else if (to) {
          if (eventStart > to && eventEnd > to) {
            return false;
          }
        }
      }

      return true;
    });
  }
}
