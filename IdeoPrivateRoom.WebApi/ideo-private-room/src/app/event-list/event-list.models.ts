import { CalendarEvent } from "angular-calendar";
import { RangeDatepickerModel } from "../shared/range-datepicker/range-datepicker.model";

export interface User {
  id: string;
  name: string;
  role: string;
}

export enum EventStatus {
  Pending = 0,
  Confirmed = 1,
  Declined = 2,
}

export interface EventModel {
  id: string;
  userId: string,
  userName: string,
  userIcon: string,
  status: EventStatus;
  userApprovalResponses: EventApproval[];
  fromDate: Date,
  toDate: Date | undefined
}

export interface EventApproval {
  id: string;
  userId: string,
  userName: string,
  userIcon: string,
  approvalStatus: EventStatus | undefined;
}
