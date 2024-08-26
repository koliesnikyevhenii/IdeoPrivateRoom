import { Injectable } from '@angular/core';
import { CalendarSlots, CalendarUserEvent } from './calendar.models';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  calendarSlots: CalendarSlots = {
    firstEvent: null,
    moreEvents: [],
  };

  addEventToSlot(event: CalendarUserEvent) {
    if (!this.calendarSlots.firstEvent) {
      this.calendarSlots = {
        firstEvent: event,
        moreEvents: this.calendarSlots.moreEvents,
      };
    } else {
      this.calendarSlots = {
        firstEvent: this.calendarSlots.firstEvent,
        moreEvents: [...this.calendarSlots.moreEvents, event],
      };
    }
  }

  removeEventFromSlot(event: CalendarUserEvent) {
    if (
      this.calendarSlots.firstEvent &&
      this.calendarSlots.firstEvent.id === event.id
    ) {
      this.calendarSlots = {
        firstEvent: null,
        moreEvents: this.calendarSlots.moreEvents,
      };
    } else if (
      this.calendarSlots.moreEvents.findIndex(
        (innerEvent) => innerEvent.id === event.id
      ) === -1
    ) {
      return;
    } else {
      this.calendarSlots = {
        firstEvent: this.calendarSlots.firstEvent,
        moreEvents: [
          ...this.calendarSlots.moreEvents.filter(
            (innerEvent) => innerEvent.id !== event.id
          ),
        ],
      };
    }
  }
}
