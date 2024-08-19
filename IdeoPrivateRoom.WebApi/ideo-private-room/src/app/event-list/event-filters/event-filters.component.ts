import { Component, inject } from '@angular/core';
import { NgbActiveOffcanvas } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-event-filters',
  standalone: true,
  imports: [],
  templateUrl: './event-filters.component.html',
  styleUrl: './event-filters.component.scss'
})
export class EventFiltersComponent {
  private offCanvasService = inject(NgbActiveOffcanvas)

  close() {
    this.offCanvasService.close()
  }
}
