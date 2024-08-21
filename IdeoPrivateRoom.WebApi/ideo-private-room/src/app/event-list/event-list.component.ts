import {
  ChangeDetectionStrategy,
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from './event-list-header/event-list-header.component';
import { EventFiltersService } from './event-filters/event-filters.service';
import { EventListTableRowComponent } from './event-list-table-row/event-list-table-row.component';
@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [EventCardComponent, EventListHeaderComponent, EventListTableRowComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss',
})
export class EventListComponent implements OnInit {
  private eventFiltersService = inject(EventFiltersService);
  private destroyRef = inject(DestroyRef);

  cards = computed(() => this.eventFiltersService.loadedEvents());
  isFetching = signal<boolean>(false);
  error = signal<string>('');

  isTable = signal<boolean>(false);

  onSwitchView() {
    this.isTable.update((value) => !value)
  }

  ngOnInit() {
    this.isFetching.set(true);
    const sub = this.eventFiltersService.loadFilteredEvents().subscribe({
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
