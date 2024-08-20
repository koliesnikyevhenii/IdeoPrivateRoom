import { afterNextRender, ChangeDetectionStrategy, Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from "./event-list-header/event-list-header.component";
import { EventCardModel } from './event-card/event-card.models';
import { CalendarService } from '../calendar/calendar.service';
import { UserService } from '../user/user.service';
import { forkJoin, map } from 'rxjs';
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
      const sub = this.calendarService.fetchAllEvents().subscribe({
        next: (events) => {
          const constructedArray = events.map<EventCardModel>((event => {
            return {
              id: crypto.randomUUID(),
              name: event.user?.name,
              title: event.title,
              icon: event.user?.icon,
              startDate: event.start,
              endDate: event.end,
              status: event.status,
              userApprovalResponses: event.userApprovalResponses
            };
          }))

              this.cards.set([...constructedArray])
              this.loading.set(true)
        }
      })
      // const sub = this.calendarService.fetchAllEvents().subscribe({
      //   next: (events) => {
      //     const userObservables = events.map((event) =>
      //       this.userService.getUserById(event.userId).pipe(
      //         map((user) => ({
      //           id: event.id,
      //           name: user?.name,
      //           title: event.title,
      //           icon: user?.icon,
      //           startDate: event.start,
      //           endDate: event.end,
      //           status: event.status,
      //         }))
      //       )
      //     );

      //     forkJoin(userObservables).subscribe((constructedArray) => {
      //       this.cards.set([...constructedArray]);
      //     });
      //   },
      // });

      this.destroyRef.onDestroy(() => {
        sub.unsubscribe();
      });
    })
  }
}
