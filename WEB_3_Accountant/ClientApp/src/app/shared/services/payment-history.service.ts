import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { formatDate } from '@angular/common';

export class Project {
  name: string;
  totalSum: number;
}

export class Employee {
  name: string;
  totalHours: number;
  grossAmount: number;
  netAmount: number;
  taxPercentage: number;
}

export class WeekHistory {
  startDate: Date;
  endDate: Date;
  employees: Employee[];
  projects: Project[];
  totalGrossAmount: number;
  totalNetAmount: number;
}

@Injectable({
  providedIn: 'root',
})
export class PaymentHistoryService {
  constructor(private http: HttpClient) {}

  getWeekDetails(week: string) {
    let startDate = formatDate(week.split(" - ")[0], 'MM/dd/yyyy', 'en-US');
    return this.http.get<WeekHistory>(
      'https://localhost:44441/api/paymenthistory?startDate=' + startDate
    );
  }

  getWeekToSelect() {
    return this.http.get<string[]>(
      'https://localhost:44441/api/paymenthistory/toselect'
    );
  }
}
