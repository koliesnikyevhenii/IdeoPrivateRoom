import { Component, computed, inject, input, signal } from '@angular/core';
import { EventModel, EventStatus } from '../../event-list.models';
import { DatePipe } from '@angular/common';
import { EventListTableRowService } from '../event-list-table-row.service';
import { EventListService } from '../../event-list.service';
import { EventFiltersService } from '../../event-filters/event-filters.service';

@Component({
  selector: 'app-event-list-table-row-content',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './event-list-table-row-content.component.html',
  styleUrl: './event-list-table-row-content.component.scss'
})
export class EventListTableRowContentComponent {
  eventListTableRowService = inject(EventListTableRowService);
  eventListService = inject(EventListService);
  eventFiltersService = inject(EventFiltersService);

  card = input.required<EventModel>();

  approvalStatus = computed(() => EventStatus[this.card().status]);

  curentDate = signal<Date>(new Date());

  getStatus(status: EventStatus | undefined) {
    return status ? EventStatus[status] : EventStatus[EventStatus.Pending]
  }
  
  updateStatus(userId: string, status: string): void {
    const eventId = this.card().id
    this.eventListService.updateEventStatus(userId, eventId, status).subscribe(res => {
      this.eventListService.refetchTrigger$.next();
    });
  }
}
