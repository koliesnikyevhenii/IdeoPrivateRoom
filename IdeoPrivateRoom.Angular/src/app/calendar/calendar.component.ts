import {
  Component,
  inject,
  signal,
} from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import {
  CalendarEvent,
  CalendarEventTimesChangedEvent,
  CalendarModule,
  CalendarView,
} from 'angular-calendar';
import { finalize, map, switchMap, tap } from 'rxjs';
import { CommonModule } from '@angular/common';
import { collapseAnimation } from 'angular-calendar';
import { EventListService } from '../event-list/event-list.service';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CalendarHeaderComponent, CalendarModule, CommonModule],
  templateUrl: './calendar.component.html',
  animations: [collapseAnimation],
  styleUrl: './calendar.component.scss',
})
export class CalendarComponent {
  private eventListService = inject(EventListService);

  view: CalendarView = CalendarView.Month;

  viewDate = new Date();

  refresh = this.eventListService.refetchTrigger$;
  isFetching = signal<boolean>(false);
  error = signal<string>('');

  events$ = this.eventListService.refetchTrigger$.pipe(
    tap(() => this.isFetching.set(true)),
    switchMap(() => {
      return this.eventListService.fetchEvents({}).pipe(
        map((pagedEvents) => {
          return pagedEvents.data.map(
            (event) =>
              <CalendarEvent>{
                id: event.id,
                title: event.userName,
                start: event.fromDate,
                end: event.toDate,
                color: this.eventListService.mapColorToEventStatus(
                  event.status
                ),
              }
          );
        })
      ).pipe(finalize(() => this.isFetching.set(false)));
    })
  );

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    event.start = newStart;
    event.end = newEnd;
    this.refresh.next();
  }
}
