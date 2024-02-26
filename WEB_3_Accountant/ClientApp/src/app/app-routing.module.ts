import { TeamComponent } from './pages/team/team.component';
import { PaymentHistoryProjectsComponent } from './pages/payment-history/payment-history-projects/payment-history-projects.component';
import { PaymentHistoryEmployeesComponent } from './pages/payment-history/payment-history-employees/payment-history-employees.component';
import { ImportComponent } from './pages/import/import.component';
import { ProjectsUndoneComponent } from './pages/projects-undone/projects-undone.component';
import { ProjectsHistoryComponent } from './pages/projects-history/projects-history.component';
import { ProjectBudgetsComponent } from './pages/project-budgets/project-budgets.component';
import { PaymentNextComponent } from './pages/payment-next/payment-next.component';
import { PaymentHistoryComponent } from './pages/payment-history/payment-history.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';

import { CommonModule } from '@angular/common';
import { DxCheckBoxModule } from 'devextreme-angular/ui/check-box';
import { DxButtonModule } from 'devextreme-angular/ui/button';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxDataGridModule } from 'devextreme-angular/ui/data-grid';
import { DxTextBoxModule } from 'devextreme-angular/ui/text-box';
import { DxTabsModule } from 'devextreme-angular/ui/tabs';
import { DxiLocationModule } from 'devextreme-angular/ui/nested';
import { DxSelectBoxModule } from 'devextreme-angular/ui/select-box';
import { DxResponsiveBoxModule } from 'devextreme-angular/ui/responsive-box';
import { DxChartModule } from 'devextreme-angular/ui/chart';
import { DxPopupModule, DxScrollViewModule, DxListModule } from 'devextreme-angular';
import { PaymentNextNoUnpaidComponent } from './pages/payment-next/payment-next-no-unpaid/payment-next-no-unpaid.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'payment-next',
    component: PaymentNextComponent
  },
  {
    path: 'payment-history',
    component: PaymentHistoryComponent
  },
  {
    path: 'project-budgets',
    component: ProjectBudgetsComponent
  },
  {
    path: 'projects-history',
    component: ProjectsHistoryComponent
  },
  {
    path: 'projects-undone',
    component: ProjectsUndoneComponent
  },
  {
    path: 'import',
    component: ImportComponent
  },
  {
    path:'team',
    component:TeamComponent
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true }),
    DxDataGridModule,
    DxFormModule,
    DxButtonModule,
    CommonModule,
    DxCheckBoxModule,
    DxTextBoxModule,
    DxTabsModule,
    DxiLocationModule,
    DxSelectBoxModule,
    DxResponsiveBoxModule,
    DxTabsModule,
    DxChartModule,
    DxPopupModule,
    DxScrollViewModule,
    DxListModule,
    DxSelectBoxModule
  ],
  providers: [],
  exports: [RouterModule],
  declarations: [
    ImportComponent,
    PaymentNextComponent,
    PaymentNextNoUnpaidComponent,
    ProjectBudgetsComponent,
    ProjectsUndoneComponent,
    PaymentHistoryComponent,
    PaymentHistoryEmployeesComponent,
    PaymentHistoryProjectsComponent,
    ProjectsHistoryComponent,
    TeamComponent,
    HomeComponent
  ]
})
export class AppRoutingModule
{ }
