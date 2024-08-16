import { ChangeDetectionStrategy, Component } from '@angular/core';
import { EventCardComponent } from './event-card/event-card.component';
import { EventListHeaderComponent } from "./event-list-header/event-list-header.component";

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [EventCardComponent, EventListHeaderComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss'
})
export class EventListComponent {

}
