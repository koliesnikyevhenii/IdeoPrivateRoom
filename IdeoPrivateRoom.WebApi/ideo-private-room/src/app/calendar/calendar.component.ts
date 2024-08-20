import {
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import {
  CalendarEvent,
  CalendarEventTimesChangedEvent,
  CalendarModule,
  CalendarView,
} from 'angular-calendar';
import { Subject } from 'rxjs';
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
export class CalendarComponent implements OnInit {
  private eventListService = inject(EventListService);
  private destroyRef = inject(DestroyRef);

  events = computed(() => {
    return this.eventListService.loadedEvents().map((event) => <CalendarEvent> {
      id: event.id,
      title: event.userName,
      start: event.fromDate,
      end: event.toDate,
      color: this.eventListService.mapColorToEventStatus(event.status)
    });
  });

  view: CalendarView = CalendarView.Month;

  viewDate = new Date();

  refresh = new Subject<void>();
  isFetching = signal<boolean>(false);
  error = signal<string>('');

  ngOnInit() {
    this.isFetching.set(true);
    const sub = this.eventListService.loadEvents().subscribe({
      complete: () => {
        this.isFetching.set(false);
      },
      error: (error: Error) => {
        console.error(error.message)
        this.error.set(error.message);
      },
    });

    this.destroyRef.onDestroy(() => {
      sub.unsubscribe();
    });
  }

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
