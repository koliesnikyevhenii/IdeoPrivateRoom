import { Component, EventEmitter, inject, Input, input, Output, output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  imports: [],
  templateUrl: './confirm-modal.component.html',
  styleUrl: './confirm-modal.component.scss',
})
export class ConfirmModalComponent {
  modalService = inject(NgbActiveModal)

  // Ng-bootstrap does not support signals yet, thus the need in old-fashioned inputs/outputs
  @Input({required: true}) public confirmationTitle!: string;
  @Input({required: true}) public confirmationText!: string;
  
  @Output() confirmationStatus = new EventEmitter<boolean>();

  confirm() {
    this.confirmationStatus.emit(true);
    this.modalService.close();
  }

  reject() {
    this.confirmationStatus.emit(false);
    this.modalService.close()
  }
}
