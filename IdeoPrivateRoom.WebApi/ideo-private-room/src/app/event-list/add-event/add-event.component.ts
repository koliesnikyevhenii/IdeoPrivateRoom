import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AddEventFormComponent } from "./add-event-form/add-event-form.component";

@Component({
  selector: 'app-add-event',
  standalone: true,
  imports: [AddEventFormComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './add-event.component.html',
  styleUrl: './add-event.component.scss'
})
export class AddEventComponent {
  activeModal = inject(NgbActiveModal);

  onClose() {
    this.activeModal.close()
  }
}
