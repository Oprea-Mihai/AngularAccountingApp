import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ProjectsUndoneService {
  constructor(private http: HttpClient) {}

  getUndoneProjects() {
    return this.http.get<string[]>('https://localhost:44441/api/project/unfinishedProjects');
  }

  setProjectFinished(prjName: string) {
    const headers = { 'content-type': 'application/json' };
    return this.http.post('https://localhost:44441/api/project/markFinishedProject?prjName='+prjName,
      {headers: headers,});
  }
}
