import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddEventComponent } from '../add-event/add-event.component';

@Component({
  selector: 'app-event-list-header',
  standalone: true,
  imports: [],
  templateUrl: './event-list-header.component.html',
  styleUrl: './event-list-header.component.scss'
})
export class EventListHeaderComponent {
  private modalService = inject(NgbModal);

  onAddEvent() {
    this.modalService.open(AddEventComponent)
  }
}
