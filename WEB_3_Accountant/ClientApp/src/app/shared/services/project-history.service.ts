import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export class Employee {
  name: string;
  workedHours: number;
}

@Injectable({
  providedIn: 'root',
})
export class ProjectHistoryService {
  constructor(private http: HttpClient) {}

  getProjectEmployees(project: string) {
    return this.http.get<Employee[]>(
      'https://localhost:44441/api/projecthistory?projectName=' + project
    );
  }

  getProjectToSelect() {
    return this.http.get<string[]>(
      'https://localhost:44441/api/projecthistory/toselect'
    );
  }
}
