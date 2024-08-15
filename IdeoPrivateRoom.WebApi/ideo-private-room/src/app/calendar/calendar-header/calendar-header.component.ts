import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CalendarModule, CalendarView } from 'angular-calendar';

@Component({
  selector: 'app-calendar-header',
  standalone: true,
  imports: [CalendarModule],
  templateUrl: './calendar-header.component.html',
  styleUrl: './calendar-header.component.scss'
})
export class CalendarHeaderComponent {
  @Input() view: CalendarView = CalendarView.Month;

  @Input() viewDate: Date = new Date();

  @Input() locale: string = 'en';

  @Output() viewChange = new EventEmitter<CalendarView>();

  @Output() viewDateChange = new EventEmitter<Date>();

  CalendarView = CalendarView;
}
