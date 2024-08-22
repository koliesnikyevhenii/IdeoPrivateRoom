import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { MonthViewDay } from 'calendar-utils';
import { CalendarEvent } from 'angular-calendar';
import { isAfter, isSameDay, isBefore } from 'date-fns';
import { CalendarService } from '../calendar.service';

@Component({
  selector: 'app-calendar-month-cell',
  standalone: true,
  imports: [],
  templateUrl: './calendar-month-cell.component.html',
  styleUrl: './calendar-month-cell.component.scss',
})
export class CalendarMonthCellComponent implements OnInit {
  private calendarService = inject(CalendarService);

  viewDay = input.required<MonthViewDay>();
  events = input.required<CalendarEvent[]>();

  currentSlotsStatus = signal<Array<CalendarEvent | null>>([]);

  ngOnInit() {
    this.events().forEach((event) => {
      if(event.end && isSameDay(event.end, this.viewDay().date)) {
        this.calendarService.deleteEventFromSlot(event.id);
      }
      
      if(isSameDay(event.start, this.viewDay().date)) {
        this.calendarService.addEventToSlot(event);
      }
    })

    console.log(this.viewDay().date, this.calendarService.readonlySlots().map(m => m.event))

    this.currentSlotsStatus.set(JSON.parse(
      JSON.stringify(this.calendarService.readonlySlots())
    ) as Array<CalendarEvent | null>);
  }

  // displayEvents = computed(() => {
  //   return this.events().map((event) => {
  //     let styleClass = '';
  //     if (isSameDay(this.viewDay().date, event.start)) {
  //       styleClass = 'rounded-left';
  //     } else if (!event.end) {
  //       styleClass = 'rounded-both';
  //     } else if (isSameDay(this.viewDay().date, event.end)) {
  //       styleClass = 'rounded-right';
  //     }

  //     return { event, styleClass };
  //   });
  // });
}
