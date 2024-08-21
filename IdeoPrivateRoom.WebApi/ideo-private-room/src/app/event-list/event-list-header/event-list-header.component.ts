import { Component, inject, output } from '@angular/core';
import { NgbModal, NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { AddEventComponent } from '../add-event/add-event.component';
import { EventFiltersComponent } from '../event-filters/event-filters.component';
import { EventFiltersService } from '../event-filters/event-filters.service';

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
  private eventFiltersService = inject(EventFiltersService)

  activeFilters = this.eventFiltersService.readonlyEventFilters;
  
  switchView = output();

  onAddEvent() {
    this.modalService.open(AddEventComponent)
  }

  onOpenFiltersMenu() {
    this.offcanvasService.open(EventFiltersComponent, { backdrop: 'static' })
  }

  onClearFilters() {
    this.eventFiltersService.clearFilters()
  }

  onSwitchView() {
    this.switchView.emit()
  }
}
