import {
  Component,
  computed,
  inject,
  input,
  OnInit,
  signal,
} from '@angular/core';
import { MonthViewDay } from 'calendar-utils';
import { CalendarEvent } from 'angular-calendar';
import { isAfter, isSameDay, isBefore, addDays } from 'date-fns';
import { CalendarService } from '../calendar.service';
import { CalendarUserEvent, SlotModel } from '../calendar.models';

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
  events = input.required<CalendarUserEvent[]>();

  currentSlotsStatus = signal<SlotModel[]>([]);

  ngOnInit() {
    this.events().forEach((event) => {
      if (event.end && isSameDay(addDays(event.end, 1), this.viewDay().date)) {
        this.calendarService.deleteEventFromSlot(event.id);
      }

      if (isSameDay(event.start, this.viewDay().date)) {
        this.calendarService.addEventToSlot(event);
      }
    });

    this.currentSlotsStatus.set(
      this.calendarService.slots.filter((slot) => slot.event != null)
    );

    this.displayEvents = this.currentSlotsStatus()
    .map((slot) => {
      let styleClass = '';

      // this condition will not happen, but should exist to pass null check
      if (!slot.event) {
        return { styleClass, order: slot.order };
      }

      if (
        slot.event?.end &&
        isSameDay(this.viewDay().date, slot.event.start) &&
        isSameDay(this.viewDay().date, slot.event.end)
      ) {
        styleClass = 'rounded-both';
      } else if (isSameDay(this.viewDay().date, slot.event.start)) {
        styleClass = 'rounded-left';
      } else if (
        slot.event?.end &&
        isSameDay(this.viewDay().date, slot.event.end)
      ) {
        styleClass = 'rounded-right';
      }

      return { styleClass, order: slot.order };
    })
    .sort((a, b) => a.order - b.order)
    .reverse(); // Sort by order to maintain vertical positioning;
  }

  displayEvents: { styleClass: string, order: number }[] = []
}
