import {
  Component,
  computed,
  DestroyRef,
  inject,
  OnInit,
  signal,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../../user/user.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import {
  NgbActiveOffcanvas,
  NgbDatepickerModule,
} from '@ng-bootstrap/ng-bootstrap';
import { RangeDatepickerComponent } from '../../../shared/range-datepicker/range-datepicker.component';
import { RangeDatepickerModel } from '../../../shared/range-datepicker/range-datepicker.model';
import { EventStatus } from '../../event-list.models';
import { EventFiltersService } from '../event-filters.service';

@Component({
  selector: 'app-event-filters-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgMultiSelectDropDownModule,
    NgbDatepickerModule,
    RangeDatepickerComponent,
  ],
  templateUrl: './event-filters-form.component.html',
  styleUrl: './event-filters-form.component.scss',
})
export class EventFiltersFormComponent implements OnInit {
  private userService = inject(UserService);
  private offCanvasService = inject(NgbActiveOffcanvas);
  private eventFiltersService = inject(EventFiltersService);
  private destroyRef = inject(DestroyRef);

  isFetching = signal<boolean>(false);
  error = signal<string>('');

  employeeDropdownSettings = {
    singleSelection: false,
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    idField: 'id',
    textField: 'name',
    itemsShowLimit: 3,
    allowRemoteDataSearch: true,
    allowSearchFilter: true,
    defaultOpen: false,
  };

  statusDropdownSettings = {
    singleSelection: false,
    selectAllText: 'Select All',
    unSelectAllText: 'Unselect All',
    itemsShowLimit: 3,
    allowRemoteDataSearch: false,
    allowSearchFilter: false,
    defaultOpen: false,
  };

  employees = computed(() => this.userService.allUsers());
  // Outputs a list of status keys without values
  statuses = Object.keys(EventStatus).filter((key) => isNaN(Number(key)));
  activeFilters = this.eventFiltersService.readonlyEventFilters;

  form = new FormGroup({
    employee: new FormControl<{ id: string; name: string }[] | undefined>(
      this.activeFilters()?.employee ?? []
    ),
    status: new FormControl<string[]>(this.activeFilters()?.status ?? []),
    dates: new FormControl<RangeDatepickerModel>(
      this.activeFilters()?.dates ?? {
        fromDate: null,
        toDate: null,
      }
    ),
  });

  onSubmit() {
    if (!this.form.valid) {
      return;
    }

    const employee = this.form.controls.employee.value;
    const status = this.form.controls.status.value;
    const dates = this.form.controls.dates.value;

    this.eventFiltersService.setEventFilters({
      employee: employee,
      status: status,
      dates: dates,
    });

    this.offCanvasService.close();
  }

  onClose() {
    this.offCanvasService.close();
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
