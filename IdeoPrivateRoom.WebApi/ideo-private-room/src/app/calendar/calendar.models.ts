import { CalendarEvent } from "angular-calendar";

export interface SlotModel {
    order: number,
    event: CalendarUserEvent | null
}

export interface CalendarUserEvent extends CalendarEvent {
    userName: string
}