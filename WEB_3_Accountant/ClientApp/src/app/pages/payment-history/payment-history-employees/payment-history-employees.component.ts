import { Component, OnInit, Input } from '@angular/core';

import {
  Employee,
} from 'src/app/shared/services/payment-history.service';

@Component({
  selector: 'app-payment-history-employees',
  templateUrl: './payment-history-employees.component.html',
  styleUrls: [],
})
export class PaymentHistoryEmployeesComponent implements OnInit {
  @Input() weekEmployees: Employee[];

  constructor() {
  }

  ngOnInit(): void {}
}
