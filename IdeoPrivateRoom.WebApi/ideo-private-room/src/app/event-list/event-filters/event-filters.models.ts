import { RangeDatepickerModel } from "../../shared/range-datepicker/range-datepicker.model";

export interface EventFilters {
    employee: {id: string, name: string}[] | null | undefined;
    status: string[] | undefined | null;
    dates: RangeDatepickerModel | undefined | null
  }