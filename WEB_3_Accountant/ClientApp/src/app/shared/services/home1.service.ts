import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { WeekInfo } from './WeekInfo';

export class TotalDescription {
  value: string;
  name: string;
}

const totalDescription: TotalDescription[] = [
  { value: 'totalGrossAmount', name: 'Gross Amount' },
  { value: 'totalNetAmount', name: 'Net Amount' },
];

@Injectable({
  providedIn: 'root'
})

export class HomeService
{
  getTotalDescription(): TotalDescription[] {
    return totalDescription;
  }

  constructor(private http: HttpClient) { }

  getWeekInfo(): Observable<WeekInfo[]> {
    const headers = { 'content-type': 'application/json' };

    return this.http.get<WeekInfo[]>('https://localhost:44441/api/home/getWeeks',{ headers: headers });
  }
}

