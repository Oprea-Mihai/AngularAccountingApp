import { Injectable } from '@angular/core';

@Injectable()
export class AppInfoService {
  constructor() {}

  public get title() {
    return 'Accountant';
  }

  public get footerTitle() {
    return 'Web 3 - Accountant';
  }

  public get currentYear() {
    return new Date().getFullYear();
  }
}
