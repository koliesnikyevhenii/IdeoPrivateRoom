import { afterNextRender, ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from "./event-list-header/event-list-header.component";
import { EventCardModel } from './event-card/event-card.models';
import { CalendarService } from '../calendar/calendar.service';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [EventCardComponent, EventListHeaderComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss'
})
export class EventListComponent {
  private calendarService = inject(CalendarService)
  private userService = inject(UserService)
  private destroyRef = inject(DestroyRef)

  cards = signal<EventCardModel[]>([])
  loading = signal<boolean>(true)

  constructor() {
    afterNextRender(() => {
      const sub = this.calendarService.allEvents.subscribe({
        next: (events) => {
          const users = this.userService.allUsers()
          const constructedArray = events.map<EventCardModel>((event => {
            const user = users.find(f => f.id === event.userId)
            return {
              id: event.id,
              name: user?.name,
              title: event.title,
              icon: user?.icon,
              startDate: event.start,
              endDate: event.end,
              status: event.status
            }
          }))
      
          this.cards.set([...constructedArray])
          this.loading.set(false)
        }
      })
  
      this.destroyRef.onDestroy(() => {
        sub.unsubscribe()
      })
    })
  }
}
