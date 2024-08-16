import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import { CalendarEventTimesChangedEvent, CalendarModule, CalendarView } from 'angular-calendar';
import { Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { collapseAnimation } from 'angular-calendar';
import { CalendarService } from './calendar.service';
import { EventModel } from './calendar.models';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CalendarHeaderComponent, CalendarModule, CommonModule],
  templateUrl: './calendar.component.html',
  animations: [collapseAnimation],
  styleUrl: './calendar.component.scss'
})
export class CalendarComponent implements OnInit {
  private calendarService = inject(CalendarService)
  private destroyRef = inject(DestroyRef)
  
  events = signal<EventModel[]>([]);
  view: CalendarView = CalendarView.Month;

  viewDate = new Date();

  refresh = new Subject<void>();

  ngOnInit(): void {
    const sub = this.calendarService.allEvents.subscribe({
      next: (events) => {
        this.events.set(events)
      }
    })

    this.destroyRef.onDestroy(() => {
      sub.unsubscribe()
    })
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
