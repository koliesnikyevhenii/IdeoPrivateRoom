import { Component, computed, input, OnInit, signal } from '@angular/core';
import { EventCardModel } from './event-card.models';
import { DatePipe } from '@angular/common';
import { EventStatus } from '../../calendar/calendar.models';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './event-card.component.html',
  styleUrl: './event-card.component.scss',
})
export class EventCardComponent implements OnInit {
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
}
