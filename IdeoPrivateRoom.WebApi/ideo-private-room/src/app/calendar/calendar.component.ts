import {
  ChangeDetectionStrategy,
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
  ViewEncapsulation,
} from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import {
  CalendarEventTimesChangedEvent,
  CalendarModule,
  CalendarView,
} from 'angular-calendar';
import { Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { collapseAnimation } from 'angular-calendar';
import { EventListService } from '../event-list/event-list.service';
import { CalendarMonthCellComponent } from './calendar-month-cell/calendar-month-cell.component';
import { CalendarUserEvent } from './calendar.models';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CalendarHeaderComponent, CalendarModule, CommonModule, CalendarMonthCellComponent],
  templateUrl: './calendar.component.html',
  animations: [collapseAnimation],
  styleUrl: './calendar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None
})
export class CalendarComponent implements OnInit {
  private eventListService = inject(EventListService);
  private destroyRef = inject(DestroyRef);

  events = computed(() => {
    return this.eventListService.loadedEvents().map((event) => <CalendarUserEvent> {
      id: event.id,
      title: event.userName,
      start: event.fromDate,
      end: event.toDate,
      userName: event.userName,
      status: event.status,
      allDay: true
    });
  });

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

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
