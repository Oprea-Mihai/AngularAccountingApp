import { UnpaidWeekEmployees } from "./UnpaidWeekEmployees";

export class UnpaidWeek
{
  startDate:Date;
  endDate:Date;
  employees: UnpaidWeekEmployees[];
  isPaid: boolean;
  totalGrossAmount:number;
  totalNetAmount:number;
}
