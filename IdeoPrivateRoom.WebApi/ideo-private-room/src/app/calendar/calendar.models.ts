import { CalendarEvent } from "angular-calendar";

export interface SlotModel {
    order: number,
    event: CalendarEvent | null
}