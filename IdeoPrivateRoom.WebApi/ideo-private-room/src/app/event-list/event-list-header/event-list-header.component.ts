import { Component, inject } from '@angular/core';
import { NgbModal, NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { AddEventComponent } from '../add-event/add-event.component';
import { EventFiltersComponent } from '../event-filters/event-filters.component';

@Component({
  selector: 'app-event-list-header',
  standalone: true,
  imports: [],
  templateUrl: './event-list-header.component.html',
  styleUrl: './event-list-header.component.scss'
})
export class EventListHeaderComponent {
  private modalService = inject(NgbModal);
  private offcanvasService = inject(NgbOffcanvas)

  onAddEvent() {
    this.modalService.open(AddEventComponent)
  }

  openFiltersMenu() {
    this.offcanvasService.open(EventFiltersComponent, { backdrop: 'static' })
  }
}
