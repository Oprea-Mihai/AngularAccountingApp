import { Week } from './Week';
import { PaymentNextService } from './../../../shared/services/payment-next.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-payment-next-no-unpaid',
  templateUrl: './payment-next-no-unpaid.component.html',
  styleUrls: ['./payment-next-no-unpaid.component.scss']
})
export class PaymentNextNoUnpaidComponent implements OnInit {

  weekDetails: Week = new Week();

  constructor(private service: PaymentNextService) { }

  ngOnInit(): void {
    this.service.getLastWeekData().subscribe(response => {
      if(response != null)
      {
        this.weekDetails = response;
      }
    });
  }

}
