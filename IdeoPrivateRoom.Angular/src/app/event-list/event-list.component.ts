import {
  ChangeDetectionStrategy,
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { toObservable, toSignal } from '@angular/core/rxjs-interop';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from './event-list-header/event-list-header.component';
import { EventFiltersService } from './event-filters/event-filters.service';
import { EventListTableRowComponent } from './event-list-table-row/event-list-table-row.component';
import { EventListService } from './event-list.service';
import { EventModel, EventStatus, ViewMode } from './event-list.models';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, finalize, switchMap, tap } from 'rxjs';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [
    EventCardComponent,
    EventListHeaderComponent,
    EventListTableRowComponent,
    CommonModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss',
})
export class EventListComponent implements OnInit {
  private eventFiltersService = inject(EventFiltersService);
  private eventListService = inject(EventListService);
  private destroyRef = inject(DestroyRef);

  viewMode = computed(() => ViewMode[this.eventListService.readonlyViewMode()]);
  error = signal<string>('');
  isFetching = signal<boolean>(false);

  filters = this.eventFiltersService.readonlyEventFilters;
  //cards = computed(() => this.eventFiltersService.loadedEvents());

  cards$ = combineLatest([
    toObservable(this.filters),
    this.eventListService.refetchTrigger$,
  ]).pipe(
    tap(() => this.isFetching.set(true)),
    switchMap(([filter]) => {
      return this.eventListService.fetchEventsNew({
        userIds: filter?.employee?.map((i) => i.id) ?? [],
        statuses:
          filter?.status?.map((i) =>
            EventStatus[i as keyof typeof EventStatus].toString()
          ) ?? [],
        startDate: this.getDate(filter?.dates?.fromDate),
        endDate: this.getDate(filter?.dates?.toDate),
      }).pipe(
        finalize(() => this.isFetching.set(false))
      );
    })
  );

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

  private getDate(date: NgbDate | null | undefined) {
    return date ? new Date(date.year, date.month - 1, date.day) : null;
  }
}
