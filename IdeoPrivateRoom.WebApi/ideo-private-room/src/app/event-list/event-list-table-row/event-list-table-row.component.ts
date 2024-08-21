import { Component, computed, DestroyRef, inject, input, signal } from '@angular/core';
import { EventModel, EventStatus } from '../event-list.models';
import { DatePipe } from '@angular/common';
import { NgbAccordionModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EventListService } from '../event-list.service';
import { ConfirmModalComponent } from '../../shared/confirm-modal/confirm-modal.component';
import { EventFiltersService } from '../event-filters/event-filters.service';

@Component({
  selector: 'app-event-list-table-row',
  standalone: true,
  imports: [DatePipe, NgbAccordionModule],
  templateUrl: './event-list-table-row.component.html',
  styleUrl: './event-list-table-row.component.scss',
})
export class EventListTableRowComponent {
  private destroyRef = inject(DestroyRef);
  private eventListService = inject(EventListService);
  private eventFiltersService = inject(EventFiltersService);
  private modalService = inject(NgbModal);

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

  onDelete(event: MouseEvent) {
    event.stopPropagation()
    const modal = this.modalService.open(ConfirmModalComponent);
    modal.componentInstance.confirmationTitle = 'Confirm Deletion';
    modal.componentInstance.confirmationText =
      'Are you sure you want to delete this vacation request?';

    const sub = modal.componentInstance.confirmationStatus.subscribe({
      next: (result: any) => {
        if (result === true) {
          this.eventListService.deleteEvent(this.card().id).subscribe({
            complete: () =>
              this.eventFiltersService.loadFilteredEvents().subscribe(),
            error: (error: Error) => {
              console.log(error.message);
            },
          });
        }
      },
    });

    this.destroyRef.onDestroy(() => {
      sub.unsubscribe();
    });
  }
}
