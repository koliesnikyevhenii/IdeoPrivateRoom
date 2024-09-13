import { CalendarEvent } from "angular-calendar";
import { RangeDatepickerModel } from "../shared/range-datepicker/range-datepicker.model";

export interface User {
  id: string;
  name: string;
  role: string;
}

export enum EventStatus {
  Pending = 0,
  Approved = 1,
  Declined = 2,
}

export interface EventModel {
  id: string;
  userId: string,
  userName: string,
  userIcon: string,
  status: EventStatus;
  //userApprovalResponses: EventApproval[];
  reviewers: EventReviewer[],
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

export interface EventReviewer {
  id: string,
  name: string,
  icon: string,
  approvalStatus: number;
}

export enum ViewMode {
  Table = 0,
  Cards = 1
}

export interface FetchEventsParams {
  page?: number | undefined,
  pageSize?: number | undefined,
  statuses?: string[],
  userIds?: string[],
  startDate?: Date | null | undefined,
  endDate?: Date | null | undefined
}
