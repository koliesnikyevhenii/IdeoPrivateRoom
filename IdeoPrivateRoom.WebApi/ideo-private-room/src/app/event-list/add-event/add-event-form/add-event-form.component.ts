import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { endsBeforeStart, validDate } from './add-event-form.validators';
import { User } from '../../event-list.models';
import {
  NgbActiveModal,
  NgbDateAdapter,
  NgbDateNativeAdapter,
  NgbDatepickerModule,
} from '@ng-bootstrap/ng-bootstrap';
import { error } from 'console';
import { DatePipe } from '@angular/common';

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

  employees: User[] = [
    {
      id: '1',
      name: 'John',
      role: 'developer',
    },
    {
      id: '2',
      name: 'Mike',
      role: 'designer',
    },
    {
      id: '3',
      name: 'Steve',
      role: 'tester',
    },
  ];

  form = new FormGroup({
    employee: new FormControl('', {
      validators: [Validators.required],
    }),
    title: new FormControl('', {
      validators: [Validators.required],
    }),
    dates: new FormGroup(
      {
        startDate: new FormControl(new Date(), {
          validators: [validDate('startDate')],
        }),
        endDate: new FormControl(new Date(), {
          validators: [validDate('endDate')],
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

  get invalidTitle() {
    const titleField = this.form.controls.title;
    return titleField.touched && titleField.invalid;
  }

  get invalidEmployee() {
    const employeeField = this.form.controls.employee;
    return employeeField.touched && employeeField.invalid;
  }

  get invalidStartDate() {
    const startDate = this.form.controls.dates.get('startDate');
    return startDate?.errors && startDate.errors['dateBeforeToday'] == true;
  }

  get invalidEndDate() {
    const endDate = this.form.controls.dates.get('endDate');
    return endDate?.errors && endDate.errors['dateBeforeToday'] == true;
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
      console.log(this.form);
      return;
    }

    console.log(this.form);
  }

  onCancel() {
    this.modalService.close();
  }
}
