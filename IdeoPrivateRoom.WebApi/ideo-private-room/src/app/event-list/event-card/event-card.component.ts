import {
  Component,
  DestroyRef,
  inject,
  input,
  OnInit,
  signal,
} from '@angular/core';
import { DatePipe } from '@angular/common';
import { EventModel, EventStatus } from '../event-list.models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '../../shared/confirm-modal/confirm-modal.component';
import { EventListService } from '../event-list.service';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './event-card.component.html',
  styleUrl: './event-card.component.scss',
})
export class EventCardComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private eventListService = inject(EventListService);
  private modalService = inject(NgbModal);

  card = input.required<EventModel>();
  cardStatus = signal<{ class: string; name: string } | undefined>(undefined);

  ngOnInit(): void {
    switch (this.card().status) {
      case EventStatus.Pending:
        this.cardStatus.set({
          class: 'pending-bg',
          name: EventStatus[EventStatus.Pending],
        });
        break;
      case EventStatus.Declined:
        this.cardStatus.set({
          class: 'rejected-bg',
          name: EventStatus[EventStatus.Declined],
        });
        break;
      default:
        this.cardStatus.set({
          class: 'approved-bg',
          name: EventStatus[EventStatus.Confirmed],
        });
        break;
    }
  }

  removeCard() {
    const modal = this.modalService.open(ConfirmModalComponent);
    modal.componentInstance.confirmationTitle = 'Confirm Deletion';
    modal.componentInstance.confirmationText =
      'Are you sure you want to delete this vacation request?';

    const sub = modal.componentInstance.confirmationStatus.subscribe({
      next: (result: any) => {
        if (result === true) {
          this.eventListService.deleteEvent(this.card().id).subscribe({
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
