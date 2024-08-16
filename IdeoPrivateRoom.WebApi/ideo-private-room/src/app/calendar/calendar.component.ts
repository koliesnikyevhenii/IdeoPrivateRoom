import { Component, inject } from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import { CalendarEventTimesChangedEvent, CalendarModule, CalendarView } from 'angular-calendar';
import { Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { collapseAnimation } from 'angular-calendar';
import { CalendarService } from './calendar.service';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CalendarHeaderComponent, CalendarModule, CommonModule],
  templateUrl: './calendar.component.html',
  animations: [collapseAnimation],
  styleUrl: './calendar.component.scss'
})
export class CalendarComponent {
  private calendarService = inject(CalendarService)
  
  events = this.calendarService.allEvents()
  view: CalendarView = CalendarView.Month;

  viewDate = new Date();

  refresh = new Subject<void>();

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
