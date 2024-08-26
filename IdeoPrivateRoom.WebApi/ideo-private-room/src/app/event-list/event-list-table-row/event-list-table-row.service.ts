import { Injectable } from '@angular/core';
import { EventStatus } from '../event-list.models';

@Injectable({
  providedIn: 'root',
})
export class EventListTableRowService {
  getApprovalStatus(status: EventStatus) {
    switch (status) {
      case EventStatus.Pending:
        return {
          badgeClass: 'text-bg-warning',
          class: 'status-pending',
          name: EventStatus[EventStatus.Pending],
        };
      case EventStatus.Declined:
        return {
          badgeClass: 'text-bg-danger',
          class: 'status-declined',
          name: EventStatus[EventStatus.Declined],
        };
      default:
        return {
          badgeClass: 'text-bg-success',
          class: 'status-confirmed',
          name: EventStatus[EventStatus.Approved],
        };
    }
  }
}
