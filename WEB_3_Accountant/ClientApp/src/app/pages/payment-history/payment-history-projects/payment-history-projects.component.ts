import { Component, Input, OnInit } from '@angular/core';

import { Project } from '../../../shared/services/payment-history.service';

@Component({
  selector: 'app-payment-history-projects',
  templateUrl: './payment-history-projects.component.html',
  styleUrls: []
})
export class PaymentHistoryProjectsComponent implements OnInit {

  @Input() weekProjects: Project[];

  constructor() {
    this.customizeTooltip = this.customizeTooltip.bind(this);
  }

  ngOnInit(): void {
  }

  customizeTooltip(args: any) {
    return {
      html: `<div class='currency'>${args.valueText}</div>`,
    };
  }

}
