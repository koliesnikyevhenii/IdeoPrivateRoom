import {
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  endsBeforeStart,
  validDate,
} from '../../event-validators/add-event-form.validators';
import {
  NgbActiveModal,
  NgbDateAdapter,
  NgbDateNativeAdapter,
  NgbDatepickerModule,
} from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { UserService } from '../../../user/user.service';
import { EventListService } from '../../event-list.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { EventFiltersService } from '../../event-filters/event-filters.service';

@Component({
  selector: 'app-add-event-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgbDatepickerModule,
    DatePipe,
    NgMultiSelectDropDownModule,
  ],
  providers: [{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }],
  templateUrl: './add-event-form.component.html',
  styleUrl: './add-event-form.component.scss',
})
export class AddEventFormComponent implements OnInit {
  private modalService = inject(NgbActiveModal);
  private userService = inject(UserService);
  private eventListService = inject(EventListService);
  private eventFiltersService = inject(EventFiltersService);
  private destroyRef = inject(DestroyRef);

  isFetching = signal<boolean>(false);
  error = signal<string>('');

  employees = computed(() => this.userService.allUsers());

  form = new FormGroup({
    employee: new FormControl<{ id: string; name: string }[] | undefined>(
      undefined,
      {
        validators: [Validators.required],
      }
    ),
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

  employeeDropdownSettings = {
    singleSelection: true,
    idField: 'id',
    textField: 'name',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: 3,
    allowRemoteDataSearch: true,
    allowSearchFilter: true,
    defaultOpen: false,
  };

  get invalidEmployee() {
    const employeeField = this.form.controls.employee;
    return employeeField.touched && employeeField.invalid;
  }

  get invalidStartDate() {
    const startDate = this.form.controls.dates.get('startDate');
    return (
      (startDate?.touched && startDate?.errors) ||
      this.form.controls.dates.errors?.['startDateNotADate']
    );
  }

  get invalidEndDate() {
    const endDate = this.form.controls.dates.get('endDate');
    return (
      (endDate?.touched && endDate?.errors) ||
      this.form.controls.dates.errors?.['endDateNotADate']
    );
  }

  get endDateBeforeStartDate() {
    const dates = this.form.controls.dates;
    return (
      (dates.get('endDate')?.touched || dates.get('startDate')?.touched) &&
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

    const userData = this.form.controls.employee.value;
    const comment = this.form.controls.comment.value;
    const startDate = this.form.controls.dates.controls.startDate.value;
    const endDate = this.form.controls.dates.controls.endDate.value;

    if (userData?.[0] && startDate && endDate) {
      const sub = this.eventListService
        .createEvent(userData[0].id, startDate, endDate, comment)
        .subscribe({
          complete: () =>
            this.eventFiltersService.loadFilteredEvents().subscribe(),
          error: (error: Error) => {
            this.error.set(error.message);
          },
        });

      this.destroyRef.onDestroy(() => {
        sub.unsubscribe();
      });

      this.modalService.close();
    }
  }

  onCancel() {
    this.modalService.close();
  }

  ngOnInit() {
    this.isFetching.set(true);
    const sub = this.userService.loadUsers().subscribe({
      complete: () => {
        this.isFetching.set(false);
      },
      error: (error: Error) => {
        this.error.set(error.message);
      },
    });

    this.destroyRef.onDestroy(() => {
      sub.unsubscribe();
    });
  }
}
