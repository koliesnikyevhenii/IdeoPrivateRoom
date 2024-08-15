import { Component } from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import { CalendarEvent, CalendarEventTimesChangedEvent, CalendarModule, CalendarView } from 'angular-calendar';
import { colors } from './calendar.models';
import { Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { collapseAnimation } from 'angular-calendar';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CalendarHeaderComponent, CalendarModule, CommonModule],
  templateUrl: './calendar.component.html',
  animations: [collapseAnimation],
  styleUrl: './calendar.component.scss'
})
export class CalendarComponent {
  view: CalendarView = CalendarView.Month;

  viewDate = new Date();

  events: CalendarEvent[] = [
    {
      title: 'Draggable event',
      color: colors['yellow'],
      start: new Date(),
      draggable: true,
    },
    {
      title: 'A non draggable event',
      color: colors['blue'],
      start: new Date(),
    },
  ];

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
