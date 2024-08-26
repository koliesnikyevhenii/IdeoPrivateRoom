import { CalendarEvent } from "angular-calendar";

export interface CalendarUserEvent extends CalendarEvent {
    userName: string
}