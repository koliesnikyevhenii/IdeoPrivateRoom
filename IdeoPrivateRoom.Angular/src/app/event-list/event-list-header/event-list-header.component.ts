import { Component, computed, inject, output } from '@angular/core';
import { NgbDropdownModule, NgbModal, NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { AddEventComponent } from '../add-event/add-event.component';
import { EventFiltersComponent } from '../event-filters/event-filters.component';
import { EventListService } from '../event-list.service';
import { ViewMode } from '../event-list.models';

@Component({
  selector: 'app-event-list-header',
  standalone: true,
  imports: [NgbDropdownModule],
  templateUrl: './event-list-header.component.html',
  styleUrl: './event-list-header.component.scss'
})
export class EventListHeaderComponent {
  private modalService = inject(NgbModal);
  private offcanvasService = inject(NgbOffcanvas)
  private eventListService = inject(EventListService)

  activeFilters = this.eventListService.readonlyEventFilters;

  viewMode = computed(() => ViewMode[this.eventListService.readonlyViewMode()])

  onAddEvent() {
    this.modalService.open(AddEventComponent)
  }

  onOpenFiltersMenu() {
    this.offcanvasService.open(EventFiltersComponent, { backdrop: 'static' })
  }

  onClearFilters() {
    this.eventListService.clearFilters()
  }

  switchCardsMode() {
    this.eventListService.changeViewMode(ViewMode.Cards)
  }

  switchTableMode() {
    this.eventListService.changeViewMode(ViewMode.Table)
  }
}
