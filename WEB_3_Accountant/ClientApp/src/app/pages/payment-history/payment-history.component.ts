import { WeekHistory } from 'src/app/shared/services/payment-history.service';
import { Component, OnInit} from '@angular/core';
import { DomSanitizer} from '@angular/platform-browser';
import { PaymentHistoryService } from '../../shared/services/payment-history.service';
import { Tab } from '../../models/Tab';



@Component({
  templateUrl: './payment-history.component.html',
  styleUrls: ['./payment-history.component.scss'],
})
export class PaymentHistoryComponent implements OnInit {
  weekSelect: string[];
  weekSelected: string;
  weekHistory: WeekHistory = new WeekHistory();

  // for tab change
  tabs: Tab[];
  currentTabId: number;

  constructor(public dom: DomSanitizer, private service: PaymentHistoryService) {
    this.tabs = [
      {
        id: 0,
        text: 'Projects',
        icon: 'fields',
      },
      {
        id: 1,
        text: 'Employees',
        icon: 'user',
      },
    ];

    this.currentTabId = 0;
  }

  selectTab(e: any) {
    this.currentTabId = e.itemData.id;
  }

  ngOnInit(): void {
    this.service.getWeekToSelect().subscribe(response => {
      this.weekSelect = response;
      this.weekSelected = this.weekSelect[0];
      this.service.getWeekDetails(this.weekSelected).subscribe((response:WeekHistory) => {
        this.weekHistory  = response;
      });
    });
  }

  onValueChanged(e: any) {
    const previousValue = e.previousValue;
    const newValue = e.value;

    // get the data by week
    this.service.getWeekDetails(this.weekSelected).subscribe(response => {
      this.weekHistory  = response;
    });
  }
}
