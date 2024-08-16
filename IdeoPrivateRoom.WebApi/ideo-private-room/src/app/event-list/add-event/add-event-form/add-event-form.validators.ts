import { AbstractControl } from '@angular/forms';
import { isBefore } from 'date-fns'

export function validDate(dateControl: string) {
  return (control: AbstractControl) => {
    const controlValue = control.value
    const newDate = new Date()
    controlValue.setHours(0, 0, 0, 0)
    newDate.setHours(0, 0, 0, 0)

    if (
      controlValue === null ||
      isBefore(controlValue, newDate)
    ) {
      return { dateBeforeToday: true };
    }

    return null;
  };
}

export function endsBeforeStart(startControl: string, endControl: string) {
  return (control: AbstractControl) => {
    const startControlValue = control.get(startControl)?.value as Date;
    const endControlValue = control.get(endControl)?.value as Date;
    startControlValue.setHours(0, 0, 0, 0)
    endControlValue.setHours(0, 0, 0, 0)
    
    if (
      isBefore(endControlValue, startControlValue)
    ) {
      return { endsBeforeStart: true };
    }

    return null;
  };
}
