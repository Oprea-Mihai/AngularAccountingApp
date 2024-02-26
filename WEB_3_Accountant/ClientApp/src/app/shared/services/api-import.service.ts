import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Excel } from 'src/app/models/Excel';

@Injectable({
  providedIn: 'root',
})
export class ApiImportService {
  constructor(private http: HttpClient) {}

  checkNewEmployees(names: Array<string>): Observable<string[]> {
    const headers = { 'content-type': 'application/json' };
    var body = JSON.stringify(names);
    return this.http.post<string[]>('https://localhost:44441/api/excel/checkNewEmp',body,{headers:headers});
  }

  addExcel(excel: Excel): Observable<Excel> {
    const headers = { 'content-type': 'application/json' };
    var body: string = JSON.stringify(excel);
    return this.http.post<any>('https://localhost:44441/api/excel/post', body, {
      headers: headers,
    });
  }

  checkExcelIsValid(excel: Excel): Observable<any>{
    const headers = { 'content-type': 'application/json' };
    var body: string = JSON.stringify(excel);
    return this.http.post<any>('https://localhost:44441/api/excel/validateExcel', body, {
      headers: headers,
    });
  }
}
