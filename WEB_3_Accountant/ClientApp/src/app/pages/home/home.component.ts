import { Component } from '@angular/core';
import {
  TotalDescription,
  HomeService,
} from '../../shared/services/home1.service';
import { WeekInfo } from 'src/app/shared/services/WeekInfo';

@Component({
  selector: 'home-page',
  templateUrl: 'home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [HomeService],
  preserveWhitespaces: true,
})
export class HomeComponent {
  types: string[] = ['line', 'stackedline', 'fullstackedline'];

  weekInfo: WeekInfo[] = [];
  totalDescription: TotalDescription[];

  constructor(private service: HomeService) {}
  ngOnInit(): void {
    this.totalDescription = this.service.getTotalDescription();

    this.service.getWeekInfo().subscribe((response:WeekInfo[]) => {
      this.weekInfo = response;
    });
  }
}
