import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CalendarComponent } from './calendar/calendar.component';
import { HeaderComponent } from './header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CalendarComponent, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ideo-private-room';
}
