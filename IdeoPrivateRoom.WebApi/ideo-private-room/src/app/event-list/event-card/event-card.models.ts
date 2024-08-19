import { EventStatus } from "../../calendar/calendar.models";

export interface EventCardModel {
  id: string;
  name: string | undefined;
  title: string;
  icon: string | undefined;
  startDate: Date;
  endDate: Date | undefined;
  status: EventStatus;
}