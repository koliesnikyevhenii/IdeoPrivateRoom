import { Component, inject } from '@angular/core';
import { CalendarHeaderComponent } from './calendar-header/calendar-header.component';
import { CalendarEventTimesChangedEvent, CalendarModule, CalendarMonthViewDay, CalendarView } from 'angular-calendar';
import { Subject } from 'rxjs';
import { CommonModule } from '@angular/common';
import { collapseAnimation } from 'angular-calendar';
import { CalendarService } from './calendar.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { AddEventComponent } from './add-event/add-event.component';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [CalendarHeaderComponent, CalendarModule, CommonModule, AddEventComponent],
  templateUrl: './calendar.component.html',
  animations: [collapseAnimation],
  styleUrl: './calendar.component.scss'
})
export class CalendarComponent {
  private calendarService = inject(CalendarService)
  private modalService = inject(NgbModal);
  
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

  onDayClick(eventDay: {day: CalendarMonthViewDay}) {
    this.modalService.open(AddEventComponent)
  }
}
