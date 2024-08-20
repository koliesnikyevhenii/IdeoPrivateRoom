import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { endsBeforeStart, isADate, validDate } from '../../event-validators/add-event-form.validators';
import {
  NgbActiveModal,
  NgbDateAdapter,
  NgbDateNativeAdapter,
  NgbDatepickerModule,
} from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { UserService } from '../../../user/user.service';
import { EventListService } from '../../event-list.service';

@Component({
  selector: 'app-add-event-form',
  standalone: true,
  imports: [ReactiveFormsModule, NgbDatepickerModule, DatePipe],
  providers: [{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
  templateUrl: './add-event-form.component.html',
  styleUrl: './add-event-form.component.scss',
})
export class AddEventFormComponent {
  modalService = inject(NgbActiveModal);
  userService = inject(UserService)
  eventListService = inject(EventListService)

  employees = this.userService.allUsers();

  form = new FormGroup({
    employee: new FormControl<string>('', {
      validators: [Validators.required],
    }),
    comment: new FormControl<string>(''),
    dates: new FormGroup(
      {
        startDate: new FormControl<Date>(new Date(), {
          validators: [validDate()],
        }),
        endDate: new FormControl<Date>(new Date(), {
          validators: [validDate()],
        }),
      },
      {
        validators: [
          Validators.required,
          endsBeforeStart('startDate', 'endDate'),
        ],
      }
    ),
  });

  get invalidEmployee() {
    const employeeField = this.form.controls.employee;
    return employeeField.touched && employeeField.invalid;
  }

  get invalidStartDate() {
    const startDate = this.form.controls.dates.get('startDate');
    return (startDate?.touched && startDate?.errors) || this.form.controls.dates.errors?.['startDateNotADate']
  }

  get invalidEndDate() {
    const endDate = this.form.controls.dates.get('endDate');
    return endDate?.touched && endDate?.errors || this.form.controls.dates.errors?.['endDateNotADate']
  }

  get endDateBeforeStartDate() {
    const dates = this.form.controls.dates
    return (
      (dates.get('endDate')?.touched ||
      dates.get('startDate')?.touched) &&
      dates?.errors &&
      dates.errors['endsBeforeStart'] == true
    );
  }

  get currentDate() {
    return new Date();
  }

  onSubmit() {
    this.form.markAllAsTouched();

    if (this.form.invalid) {
      return;
    }

    const userId = this.form.controls.employee.value;
    const comment = this.form.controls.comment.value;
    const startDate = this.form.controls.dates.controls.startDate.value;
    const endDate = this.form.controls.dates.controls.endDate.value;

    if(userId && comment && startDate && endDate) {
      this.eventListService.addEvent(userId, startDate, endDate, comment)
      this.modalService.close();
    }
  }

  onCancel() {
    this.modalService.close();
  }
}
