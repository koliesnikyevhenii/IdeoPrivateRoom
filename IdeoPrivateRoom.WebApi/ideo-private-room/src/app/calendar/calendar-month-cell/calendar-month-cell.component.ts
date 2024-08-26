import { Component, computed, input, ViewEncapsulation } from '@angular/core';
import { MonthViewDay } from 'calendar-utils';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { isSameDay } from 'date-fns';
import { CalendarUserEvent } from '../calendar.models';

@Component({
  selector: 'app-calendar-month-cell',
  standalone: true,
  imports: [NgbTooltipModule],
  templateUrl: './calendar-month-cell.component.html',
  styleUrl: './calendar-month-cell.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class CalendarMonthCellComponent {
  viewDay = input.required<MonthViewDay>();
  events = computed(() => this.viewDay().events as CalendarUserEvent[]);

  firstDisplayEvent = computed(() => {
    return this.events().at(-1) as CalendarUserEvent;
  });

  moreEvents = computed(() => this.events().slice(1, this.events().length - 1));

  isEventStartDay = computed(() =>
    isSameDay(this.firstDisplayEvent().start, this.viewDay().date)
  );

  roundStatus = computed(() => {
    if (this.isEventStartDay()) {
      return 'rounded-start-2';
    } else if (
      this.firstDisplayEvent().end &&
      isSameDay(this.firstDisplayEvent().end as Date, this.viewDay().date) &&
      !this.isEventStartDay()
    ) {
      return 'rounded-end-2';
    } else {
      return '';
    }
  });
}
