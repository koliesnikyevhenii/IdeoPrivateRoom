import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { UserService } from '../../../user/user.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NgbActiveOffcanvas, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { RangeDatepickerComponent } from "../../../shared/range-datepicker/range-datepicker.component";
import { RangeDatepickerModel } from '../../../shared/range-datepicker/range-datepicker.model';
import { EventStatus } from '../../event-list.models';

@Component({
  selector: 'app-event-filters-form',
  standalone: true,
  imports: [ReactiveFormsModule, NgMultiSelectDropDownModule, NgbDatepickerModule, RangeDatepickerComponent],
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
    dates: new FormControl<RangeDatepickerModel>({
      fromDate: null,
      toDate: null
    })
  });

  onSubmit() {
    console.log(this.form);
  }

  onClose() {
    this.offCanvasService.close();
  }
}
