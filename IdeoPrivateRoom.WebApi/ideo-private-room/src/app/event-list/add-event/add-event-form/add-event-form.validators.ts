import { AbstractControl } from '@angular/forms';
import { isBefore } from 'date-fns';

export function isADate(dateValue: any) {
  if (typeof dateValue.setHours !== 'function') {
    return false
  }

  return true;
}

export function validDate() {
  return (control: AbstractControl) => {
    const controlValue = control.value;

    if(!isADate(controlValue)) {
      return { notADate: true }
    }

    const newDate = new Date();

    controlValue.setHours(0, 0, 0, 0);
    newDate.setHours(0, 0, 0, 0);

    if (controlValue === null || isBefore(controlValue, newDate)) {
      return { dateBeforeToday: true };
    }

    return null;
  };
}

export function endsBeforeStart(startControl: string, endControl: string) {
  return (control: AbstractControl) => {
    const startControlObject = control.get(startControl);
    const endControlObject = control.get(endControl);
    const startControlValue = startControlObject?.value;
    const endControlValue = endControlObject?.value;

    if(!isADate(startControlValue)) {
      return { startDateNotADate: true }
    }

    if(!isADate(endControlValue)) {
      return { endDateNotADate: true }
    }

    startControlValue.setHours(0, 0, 0, 0);
    endControlValue.setHours(0, 0, 0, 0);

    if (isBefore(endControlValue, startControlValue)) {
      return { endsBeforeStart: true };
    }

    return null;
  };
}
