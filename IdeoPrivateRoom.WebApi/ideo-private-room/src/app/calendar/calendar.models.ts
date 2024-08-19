import { CalendarEvent } from "angular-calendar";
import { RangeDatepickerModel } from "../shared/range-datepicker/range-datepicker.model";

export const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};

export enum EventStatus {
  Pending,
  Confirmed,
  Declined
}

export interface EventModel extends CalendarEvent {
  id: string,
  userId: string,
  status: EventStatus
}

export interface EventFilters {
  employee: string | string[],
  status: EventStatus | EventStatus[],
  dates: RangeDatepickerModel,
}
