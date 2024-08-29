import { Component, computed, inject, input, signal } from '@angular/core';
import { EventModel, EventStatus } from '../../event-list.models';
import { DatePipe } from '@angular/common';
import { EventListTableRowService } from '../event-list-table-row.service';

@Component({
  selector: 'app-event-list-table-row-content',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './event-list-table-row-content.component.html',
  styleUrl: './event-list-table-row-content.component.scss'
})
export class EventListTableRowContentComponent {
  eventListTableRowService = inject(EventListTableRowService);

  card = input.required<EventModel>();

  approvalStatus = computed(() => EventStatus[this.card().status]);

  curentDate = signal<Date>(new Date());

  getStatus(status: EventStatus | undefined) {
    return status ? EventStatus[status] : EventStatus[EventStatus.Pending]
  }
}
