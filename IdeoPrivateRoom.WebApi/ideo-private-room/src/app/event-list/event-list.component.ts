import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from './event-list-header/event-list-header.component';
import { EventListService } from './event-list.service';
@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [EventCardComponent, EventListHeaderComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss',
})
export class EventListComponent implements OnInit {
  private eventListService = inject(EventListService);
  private destroyRef = inject(DestroyRef);

  cards = this.eventListService.loadedEvents;
  isFetching = signal<boolean>(false);
  error = signal<string>('');

  ngOnInit() {
    this.isFetching.set(true);
    const sub = this.eventListService.loadEvents().subscribe({
      complete: () => {
        this.isFetching.set(false);
      },
      error: (error: Error) => {
        this.error.set(error.message);
      },
    });

    this.destroyRef.onDestroy(() => {
      sub.unsubscribe();
    });
  }
}
