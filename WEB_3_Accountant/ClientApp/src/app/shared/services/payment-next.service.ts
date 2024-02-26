import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from "rxjs/operators";
import { Week } from 'src/app/pages/payment-next/payment-next-no-unpaid/Week';
import { UnpaidWeek } from 'src/app/models/UnpaidWeek';

@Injectable({
  providedIn: 'root',
})
export class PaymentNextService {
  constructor(private http: HttpClient) {}

  getWeekData(): Observable<UnpaidWeek> {
    return this.http.get<UnpaidWeek>(
      'https://localhost:44441/api/payment/unpaid'
    ).pipe(
      catchError((err: HttpErrorResponse): Observable<UnpaidWeek> => {
        return new Observable<UnpaidWeek>();
      })
    );
  }

  getLastWeekData(): Observable<Week> {
    return this.http.get<Week>(
      'https://localhost:44441/api/payment/lastpayment'
    );
  }
  putUnpaidWeek(date: Date, body: UnpaidWeek): Observable<Week> {
    return this.http.put<Week>(
      'https://localhost:44441/api/payment/updatepayment?startDate=' + date,
      body
    );
  }
  putUpdateProjectBudget(date: Date): Observable<any> {
    return this.http.put<any>(
      'https://localhost:44441/api/project/updateprojectbudget?startDate=' +
        date,
      {}
    );
  }
}
