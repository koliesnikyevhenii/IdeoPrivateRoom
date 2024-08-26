import {
  Component,
  computed,
  inject,
  input,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { MonthViewDay } from 'calendar-utils';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { isSameDay } from 'date-fns';
import { CalendarUserEvent } from '../calendar.models';
import { CalendarService } from '../calendar.service';
import { EventStatus } from '../../event-list/event-list.models';

@Component({
  selector: 'app-calendar-month-cell',
  standalone: true,
  imports: [NgbTooltipModule],
  templateUrl: './calendar-month-cell.component.html',
  styleUrl: './calendar-month-cell.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class CalendarMonthCellComponent implements OnInit {
  calendarService = inject(CalendarService);

  viewDay = input.required<MonthViewDay>();
  events = computed(() => this.viewDay().events as CalendarUserEvent[]);

  firstDisplayEvent: CalendarUserEvent | null = null;

  moreEvents: CalendarUserEvent[] = [];

  isFirstEventStartDay = computed(
    () =>
      this.firstDisplayEvent &&
      isSameDay(this.firstDisplayEvent!.start, this.viewDay().date)
  );

  definePaletteFromStatus(event: CalendarUserEvent) {
    const statusThemes: Record<number, string[]> = 
    {
      [EventStatus.Approved]: ['border-success', 'bg-success-subtle'],
      [EventStatus.Declined]: ['border-danger', 'bg-danger-subtle'],
    };

    return statusThemes[event.status ?? -1] || ['border-warning', 'bg-warning-subtle']
  }

  // mapColorToEventStatus(status?: number): {
  //   primary: string;
  //   secondary: string;
  // } {
  //   const statusColors: Record<number, { primary: string; secondary: string }> =
  //     {
  //       [EventStatus.Approved]: { primary: '#198754', secondary: '#5fb78e8f' },
  //       [EventStatus.Declined]: { primary: '#ad2121', secondary: '#FAE3E3' },
  //     };

  //   return (
  //     statusColors[status ?? -1] || { primary: '#e3bc08', secondary: '#FDF1BA' }
  //   );
  // }

  ngOnInit() {
    for (const event of this.events()) {
      if (isSameDay(event.start, this.viewDay().date)) {
        this.calendarService.addEventToSlot(event);
      }
    }

    this.firstDisplayEvent = this.calendarService.calendarSlots.firstEvent;
    this.moreEvents = this.calendarService.calendarSlots.moreEvents;

    for (const event of this.events()) {
      if (event.end && isSameDay(event.end, this.viewDay().date)) {
        this.calendarService.removeEventFromSlot(event);
      }
    }
  }
}
