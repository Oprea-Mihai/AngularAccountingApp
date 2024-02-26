import { HttpClient } from '@angular/common/http';
import { ProjectBudget } from '../../models/ProjectBudget';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectBudgetService {

  constructor(private http: HttpClient) {}

  getProjectBudget()
  {
   return this.http.get<ProjectBudget[]>("https://localhost:44441/api/project/budget");
  }
}
