import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { EventStatus } from '../../../calendar/calendar.models';
import {
  endsBeforeStart,
  validDate,
} from '../../event-validators/add-event-form.validators';
import { UserService } from '../../../user/user.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbActiveOffcanvas } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-event-filters-form',
  standalone: true,
  imports: [ReactiveFormsModule, NgMultiSelectDropDownModule],
  templateUrl: './event-filters-form.component.html',
  styleUrl: './event-filters-form.component.scss',
})
export class EventFiltersFormComponent {
  private userService = inject(UserService);
  private offCanvasService = inject(NgbActiveOffcanvas);

  employeeDropdownSettings = {
    singleSelection: false,
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
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

  employees = this.userService.allUsers().map((m) => m.name);
  // Outputs a list of status keys without values
  statuses = Object.keys(EventStatus).filter(key => isNaN(Number(key)))

  form = new FormGroup({
    employee: new FormControl<string[]>([]),
    status: new FormControl<EventStatus[]>([]),
    // dates: new FormGroup(
    //   {
    //     start: new FormControl<Date>(new Date(), {
    //       validators: [validDate()],
    //     }),
    //     end: new FormControl<Date>(new Date(), {
    //       validators: [validDate()],
    //     }),
    //   },
    //   {
    //     validators: [
    //       Validators.required,
    //       endsBeforeStart('startDate', 'endDate'),
    //     ],
    //   }
    // ),
  });

  onSubmit() {
    console.log(this.form);
  }

  onClose() {
    this.offCanvasService.close();
  }
}
