import { Component, inject } from '@angular/core';
import { NgbActiveOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { EventFiltersFormComponent } from "./event-filters-form/event-filters-form.component";

@Component({
  selector: 'app-event-filters',
  standalone: true,
  imports: [EventFiltersFormComponent],
  templateUrl: './event-filters.component.html',
  styleUrl: './event-filters.component.scss'
})
export class EventFiltersComponent {
  private offCanvasService = inject(NgbActiveOffcanvas)

  onClose() {
    this.offCanvasService.close()
  }
}
