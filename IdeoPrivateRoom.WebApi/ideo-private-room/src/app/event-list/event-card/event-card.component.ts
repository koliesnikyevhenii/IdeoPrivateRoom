import { Component, computed, DestroyRef, inject, input, OnInit, signal, TemplateRef } from '@angular/core';
import { EventCardModel } from './event-card.models';
import { DatePipe } from '@angular/common';
import { EventStatus } from '../../calendar/calendar.models';
import { CalendarService } from '../../calendar/calendar.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '../../shared/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './event-card.component.html',
  styleUrl: './event-card.component.scss',
})
export class EventCardComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private calendarService = inject(CalendarService);
  private modalService = inject(NgbModal)

  card = input.required<EventCardModel>();
  cardStatus = signal<{class: string, name: string} | undefined>(undefined);

  ngOnInit(): void {
    switch (this.card().status) {
      case EventStatus.Pending:
        this.cardStatus.set({
          class: 'pending-bg',
          name: EventStatus[EventStatus.Pending]
        })
        break;
      case EventStatus.Declined:
        this.cardStatus.set({
          class: 'rejected-bg',
          name: EventStatus[EventStatus.Declined]
        })
        break;
      default:
        this.cardStatus.set({
          class: 'approved-bg',
          name: EventStatus[EventStatus.Confirmed]
        })
        break;
    }
  }

  removeCard() {
    const modal = this.modalService.open(ConfirmModalComponent);
    modal.componentInstance.confirmationTitle = "Confirm Deletion"
    modal.componentInstance.confirmationText = "Are you sure you want to delete this vacation request?"

    const sub = (modal.componentInstance).confirmationStatus.subscribe({
      next: (result: any) => {
        if(result === true) {
          this.calendarService.deleteEvent(this.card().id);
        }
      }
    })

    this.destroyRef.onDestroy(() => {
      sub.unsubscribe()
    })
  }
}
