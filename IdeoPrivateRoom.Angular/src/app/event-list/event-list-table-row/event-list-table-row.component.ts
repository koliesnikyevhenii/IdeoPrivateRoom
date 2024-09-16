import { Component, computed, DestroyRef, inject, input, output, signal } from '@angular/core';
import { EventModel, EventStatus } from '../event-list.models';
import { DatePipe } from '@angular/common';
import { NgbAccordionModule, NgbModal, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { EventListService } from '../event-list.service';
import { ConfirmModalComponent } from '../../shared/confirm-modal/confirm-modal.component';
import { EventListTableRowContentComponent } from "./event-list-table-row-content/event-list-table-row-content.component";
import { EventListTableRowService } from './event-list-table-row.service';

@Component({
  selector: 'app-event-list-table-row',
  standalone: true,
  imports: [
    DatePipe,
    NgbAccordionModule,
    EventListTableRowContentComponent,
    NgbTooltipModule,
  ],
  templateUrl: './event-list-table-row.component.html',
  styleUrl: './event-list-table-row.component.scss',
})
export class EventListTableRowComponent {
  private destroyRef = inject(DestroyRef);
  private eventListService = inject(EventListService);
  private modalService = inject(NgbModal);

  eventListTableRowService = inject(EventListTableRowService);

  card = input.required<EventModel>();

  currentDate = computed(() => Date.now());
  approvalStatus = computed(() => EventStatus[this.card().status]);

  onDelete(event: MouseEvent) {
    event.stopPropagation();
    const modal = this.modalService.open(ConfirmModalComponent);
    modal.componentInstance.confirmationTitle = 'Confirm Deletion';
    modal.componentInstance.confirmationText =
      'Are you sure you want to delete this vacation request?';

    const sub = modal.componentInstance.confirmationStatus.subscribe({
      next: (result: any) => {
        if (result === true) {
          this.eventListService.deleteEvent(this.card().id).subscribe({
            complete: () =>
              this.eventListService.refetchTrigger$.next(),
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
