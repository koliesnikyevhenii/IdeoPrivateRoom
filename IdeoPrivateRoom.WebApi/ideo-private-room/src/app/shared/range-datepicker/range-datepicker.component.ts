import { Component } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import {
  NgbCalendar,
  NgbDate,
  NgbDateParserFormatter,
  NgbDatepickerModule,
} from '@ng-bootstrap/ng-bootstrap';
import { RangeDatepickerModel } from './range-datepicker.model';

@Component({
  selector: 'app-range-datepicker',
  standalone: true,
  imports: [NgbDatepickerModule],
  templateUrl: './range-datepicker.component.html',
  styleUrl: './range-datepicker.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi:true,
      useExisting: RangeDatepickerComponent
    }
  ]
})
export class RangeDatepickerComponent implements ControlValueAccessor {
  hoveredDate: NgbDate | null = null;

  fromDate: NgbDate | null;
  toDate: NgbDate | null;

  constructor(
    private calendar: NgbCalendar,
    public formatter: NgbDateParserFormatter
  ) {
    this.fromDate = calendar.getToday();
    this.toDate = calendar.getNext(calendar.getToday(), 'd', 10);
  }

  onDateSelection(date: NgbDate) {
    this.markAsTouched();

    if (!this.disabled) {
      if (!this.fromDate && !this.toDate) {
        this.fromDate = date;
      } else if (
        this.fromDate &&
        !this.toDate &&
        date &&
        date.after(this.fromDate)
      ) {
        this.toDate = date;
      } else {
        this.toDate = null;
        this.fromDate = date;
      }

      this.onChange({
        fromDate: this.fromDate,
        toDate: this.toDate,
      });
    }
  }

  isHovered(date: NgbDate) {
    return (
      this.fromDate &&
      !this.toDate &&
      this.hoveredDate &&
      date.after(this.fromDate) &&
      date.before(this.hoveredDate)
    );
  }

  isInside(date: NgbDate) {
    return this.toDate && date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return (
      date.equals(this.fromDate) ||
      (this.toDate && date.equals(this.toDate)) ||
      this.isInside(date) ||
      this.isHovered(date)
    );
  }

  validateInput(currentValue: NgbDate | null, input: string): NgbDate | null {
    const parsed = this.formatter.parse(input);
    return parsed && this.calendar.isValid(NgbDate.from(parsed))
      ? NgbDate.from(parsed)
      : currentValue;
  }

  // internal properties
  touched = false;
  disabled = false;

  onChange = (model: RangeDatepickerModel) => {};
  onTouched = () => {};

  writeValue(model: RangeDatepickerModel): void {
    this.fromDate = model.fromDate;
    this.toDate = model.toDate;
  }

  registerOnChange(onChange: any): void {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: any): void {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }
}
