import { PaymentNextService } from './../../shared/services/payment-next.service';
import { Component, OnInit } from '@angular/core';

import ArrayStore from 'devextreme/data/array_store';
import notify from 'devextreme/ui/notify';
import { UnpaidWeek } from 'src/app/models/UnpaidWeek';

@Component({
  selector: 'app-payment-next',
  templateUrl: './payment-next.component.html',
  styleUrls: ['./payment-next.component.scss'],
})
export class PaymentNextComponent implements OnInit {
  week: UnpaidWeek = new UnpaidWeek();
  signature: boolean | null | undefined = null;

  constructor(private service: PaymentNextService) {
    this.signature = false;
  }

  store = new ArrayStore();

  ngOnInit(): void {
    this.service.getWeekData().subscribe((response) => {
      this.week = response;
    });
  }

  makePayment(e: any) {
    this.service.putUnpaidWeek(this.week.startDate, this.week).subscribe();
    this.service.putUpdateProjectBudget(this.week.startDate).subscribe();
    const position = 'bottom right';
    const direction = 'up-push';

    this.service.putUnpaidWeek(this.week.startDate, this.week).subscribe();
    notify(
      {
        message: `Your payment was successful.`,
        height: 45,
        width: 180,
        minWidth: 150,
        type: 'success',
        displayTime: 3500,
        animation: {
          show: {
            type: 'fade',
            duration: 400,
            from: 0,
            to: 1,
          },
          hide: { type: 'fade', duration: 40, to: 0 },
        },
      },
      { position, direction }
    );

    setTimeout(() => {
      window.location.reload();
    }, 1000);
  }
}
