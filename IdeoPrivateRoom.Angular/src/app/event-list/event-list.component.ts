import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
  signal,
} from '@angular/core';
import { toObservable } from '@angular/core/rxjs-interop';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from './event-list-header/event-list-header.component';
import { EventListTableRowComponent } from './event-list-table-row/event-list-table-row.component';
import { EventListService } from './event-list.service';
import { EventStatus, ViewMode } from './event-list.models';
import { NgbDate, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { catchError, combineLatest, finalize, of, startWith, switchMap, tap } from 'rxjs';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [
    EventCardComponent,
    EventListHeaderComponent,
    EventListTableRowComponent,
    CommonModule,
    NgbPaginationModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss',
})
export class EventListComponent {
  private eventListService = inject(EventListService);

  viewMode = computed(() => ViewMode[this.eventListService.readonlyViewMode()]);
  error = signal<string>('');
  isFetching = signal<boolean>(false);
  isStatusUpdating = signal<boolean>(false);

  filters = this.eventListService.readonlyEventFilters;
  page = this.eventListService.page;

  paginatedEvents$ = combineLatest([
    toObservable(this.page),
    toObservable(this.filters).pipe(tap(() => this.isFetching.set(true))),
    this.eventListService.refetchTrigger$
  ]).pipe(
    switchMap(([page, filter]) => {
      return this.eventListService
        .fetchEvents({
          page: page,
          userIds: filter?.employee?.map((i) => i.id) ?? [],
          statuses:
            filter?.status?.map((i) =>
              EventStatus[i as keyof typeof EventStatus].toString()
            ) ?? [],
          startDate: this.getDate(filter?.dates?.fromDate),
          endDate: this.getDate(filter?.dates?.toDate),
        })
        .pipe(
          finalize(() => {
            this.isFetching.set(false);
          })
        );
    })
  );

  private getDate(date: NgbDate | null | undefined) {
    return date ? new Date(date.year, date.month - 1, date.day) : null;
  }
}
