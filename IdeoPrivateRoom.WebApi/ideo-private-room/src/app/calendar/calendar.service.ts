import { computed, Injectable, signal } from '@angular/core';
import { CalendarUserEvent, SlotModel } from './calendar.models';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  slots: SlotModel[] = [];
  readonlySlots = computed(() => this.slots);

  addEventToSlot(event: CalendarUserEvent) {
    // Find the index of the first null in the array
    const firstNullIndex = this.slots.findIndex((slot) => slot.event == null);

    if (firstNullIndex !== -1) {
      // Replace the first null with the new object
      this.slots[firstNullIndex].event = event;
    } else {
      // Add the new object to the end of the array
      this.slots.push(<SlotModel>{
        order: this.slots.length == 0 ? 0 : this.slots.length,
        event: event,
      });
    }
    this.cleanSlots();
  }

  deleteEventFromSlot(eventId: string | number | undefined) {
    const deletingIndex = this.slots.findIndex(
      (slot) => slot.event?.id === eventId
    );
    if (deletingIndex !== -1) {
      this.slots[deletingIndex].event = null
    }

    this.cleanSlots();
  }

  private cleanSlots() {
    // Remove empty trailing slots
    while (
      this.slots.length > 0 &&
      !this.slots[this.slots.length - 1].event
    ) {
      this.slots.pop()
    }
  }
}
