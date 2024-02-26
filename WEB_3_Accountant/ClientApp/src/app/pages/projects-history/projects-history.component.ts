import { Component, OnInit } from '@angular/core';
import { Employee, ProjectHistoryService } from 'src/app/shared/services/project-history.service';

@Component({
  selector: 'app-projects-history',
  templateUrl: './projects-history.component.html',
  styleUrls: []
})
export class ProjectsHistoryComponent implements OnInit {
  projectSelect: string[];
  projectSelected: string;
  projectEmployees: Employee[] = [];

  constructor(private service: ProjectHistoryService) { }

  ngOnInit(): void {
    this.service.getProjectToSelect().subscribe(response => {
      this.projectSelect = response;
      this.projectSelected = this.projectSelect[0];
      this.service.getProjectEmployees(this.projectSelected).subscribe(response => {
        this.projectEmployees  = response;
      });
    });
  }

  onValueChanged(e: any) {
    const previousValue = e.previousValue;
    const newValue = e.value;

    // get the data by week
    this.service.getProjectEmployees(this.projectSelected).subscribe(response => {
      this.projectEmployees  = response;
    });
  }

  customizeTooltip(args: any) {
    return {
      html: `<div class='hours'>${args.valueText}h</div>`,
    };
  }

}
