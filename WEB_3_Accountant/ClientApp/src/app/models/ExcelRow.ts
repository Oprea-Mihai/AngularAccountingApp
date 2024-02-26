import { DayOfWork } from './DayOfWork';
export class ExcelRow {
  public name: string;
  public taxPercentage: string;
  public hourlyRate: string;
  public daysOfWork: DayOfWork[] = [];
  public totalHours: string;
  public grossAmount: string;
  public netAmount: string;
}
