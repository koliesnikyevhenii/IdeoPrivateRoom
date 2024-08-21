import { Component, computed, input, signal } from '@angular/core';
import { EventModel, EventStatus } from '../event-list.models';
import { DatePipe } from '@angular/common';
import { NgbAccordionModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-event-list-table-row',
  standalone: true,
  imports: [DatePipe, NgbAccordionModule],
  templateUrl: './event-list-table-row.component.html',
  styleUrl: './event-list-table-row.component.scss',
})
export class EventListTableRowComponent {
  card = input.required<EventModel>();

  currentDate = computed(() => Date.now());
  approvalStatus = computed(() => EventStatus[this.card().status]);

  getApprovalStatus(status: EventStatus) {
    switch (status) {
      case EventStatus.Pending:
        return {
          class: 'status-pending',
          name: EventStatus[EventStatus.Pending],
        };
      case EventStatus.Declined:
        return {
          class: 'status-declined',
          name: EventStatus[EventStatus.Declined],
        };
      default:
        return {
          class: 'status-confirmed',
          name: EventStatus[EventStatus.Confirmed],
        };
    }
  }
}
