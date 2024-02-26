
import { ProjectBudget } from '../../models/ProjectBudget';
import { Component, OnInit } from '@angular/core';
import { ProjectBudgetService } from 'src/app/shared/services/project-budget.service';

@Component({
  selector: 'app-project-budgets',
  templateUrl: './project-budgets.component.html',
  styleUrls: []
})
export class ProjectBudgetsComponent implements OnInit {

  projectsBudget : ProjectBudget[] = [];

  constructor(private service : ProjectBudgetService) {
   }

  ngOnInit(): void {
    this.service.getProjectBudget().subscribe(respond => {
      this.projectsBudget = respond;
    });
  }
  onCellPrepared(e: any) {

    e.cellElement.style.fontWeight = 500;
    if(e.rowType === "data" && e.column.dataField === "currentBudget") {

        e.cellElement.style.color = e.data.currentBudget >= 0 ? "green" : "red";
        e.cellElement.style.fontWeight = 700;

        e.watch(function() {

            return e.data.currentBudget;

        }, function() {

            e.cellElement.style.color = e.data.currentBudget >= 0 ? "green" : "red";
            e.cellElement.style.fontWeight = 700;
        })
      }
}

}
