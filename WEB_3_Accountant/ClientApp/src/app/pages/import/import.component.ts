import { ExcelRow } from '../../models/ExcelRow';
import { ApiImportService } from './../../shared/services/api-import.service';
import { Component, OnInit } from '@angular/core';
import * as XLSX from 'xlsx';

import notify from 'devextreme/ui/notify';
import { Excel } from 'src/app/models/Excel';
import { DayOfWork } from 'src/app/models/DayOfWork';
import { ExcelErrorData } from 'src/app/models/ExcelErrorData';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.scss'],
})
export class ImportComponent implements OnInit {
  data: any[][];
  excel: Excel = new Excel();
  weekStart: Date;
  weekEnd: Date;
  isCheckingEmployees: boolean | null | undefined = null;
  newEmployeesToAdd: boolean | null | undefined = true;
  isValidExcel: boolean | null | undefined = null;
  dataCalcVerified: boolean | null | undefined = null;
  newEmployeeAdd: boolean | null | undefined = null;
  EmployeeList: Array<string> = [];
  excelErrorList: ExcelErrorData[] = [];

  // new Employee popup
  popupVisible = false;
  confirmButtonOptions: any;
  declineButtonOptions: any;

  constructor(private apiService: ApiImportService) {
    const that = this;
    this.confirmButtonOptions = {
      icon: 'check',
      text: 'Confirm',
      onClick() {
        that.newEmployeeAdd = true;
        that.popupVisible = false;

        const message = `You choosed to add the new employees to database.`;
        const position = 'bottom right';
        const direction = 'up-push';
        notify(
          {
            message: message,
            width: 180,
            height: 65,
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
      },
    };
    this.declineButtonOptions = {
      icon: 'clear',
      text: 'Cancel',
      onClick() {
        that.newEmployeeAdd = false;
        that.resetData(that);
        const message = `You choosed to not add new employees.`;
        const position = 'bottom right';
        const direction = 'up-push';
        notify(
          {
            message: message,
            height: 50,
            width: 180,
            minWidth: 150,
            type: 'error',
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
      },
    };
  }

  ngOnInit(): void {}

  onFileChange(event: any) {
    const target: DataTransfer = <DataTransfer>event.target;
    const position = 'bottom right';
    const direction = 'up-push';

    if (target.files.length !== 1) alert('Cannot use multiple files');

    const reader: FileReader = new FileReader();

    reader.onload = (e: any) => {
      const binaryStr: string = e.target.result;
      const workBook: XLSX.WorkBook = XLSX.read(binaryStr, {
        type: 'binary',
        cellDates: true,
        cellNF: false,
        cellText: false,
      });
      const sheetName: string = workBook.SheetNames[0];
      const workSheet: XLSX.WorkSheet = workBook.Sheets[sheetName];

      this.data = XLSX.utils.sheet_to_json(workSheet, { header: 1 });

      this.weekStart = this.data[2][3];
      this.weekStart.setDate(this.weekStart.getDate() + 1);

      this.weekEnd = this.data[2][15];
      this.weekEnd.setDate(this.weekEnd.getDate() + 1);

      let weekLength = (() => {
        for(let row = this.data.length - 1; row >= 0; row--) {
          if(this.data[row][0] == undefined && this.data[row][19] != undefined) return row;
        }
        return 0;
      })();

      this.newEmployeeAdd = null;

      for (let i = 4; i < weekLength; i++) {
        let days: DayOfWork[] = [];
        let row: ExcelRow = new ExcelRow();

        if (this.data[i][1] != undefined)
          row.taxPercentage = String(this.data[i][1]);
        else row.taxPercentage = '45';
        row.hourlyRate = String(this.data[i][2]);
        row.name = this.data[i][0];

        if (!this.EmployeeList.includes(row.name))
          this.EmployeeList.push(row.name);

        row.totalHours = String(this.data[i][17]);
        row.grossAmount = String(this.data[i][18]);
        row.netAmount = String(this.data[i][19]);

        // we start with j from 3 because the days of the week are from column 3 to 15
        // also increment by 2 beacuse on the first column are the hours for the task and on the second the task and the project
        for (let j = 3; j <= 15; j += 2) {
          let day: DayOfWork = new DayOfWork();
          // the day date is on the same column as the hours and on row 2
          day.date = this.data[2][j];
          if (this.data[i][j] == undefined) {
            day.workedHours = '';
          } else day.workedHours = String(this.data[i][j]);

          if (this.data[i][j + 1] == 'Off') {
            day.project = 'Off';
            day.task = 'Off';
          } else if (this.data[i][j + 1] !== undefined) {
            let splittedTaskAndProject = this.data[i][j + 1].split('-');
            day.project = String(splittedTaskAndProject[0]).trim();
            day.task = String(splittedTaskAndProject[1]).trim();
          } else {
            day.project = '';
            day.task = '';
          }
          days.push(day);
        }
        row.daysOfWork = days;
        this.excel.excelRow.push(row);
      }

      this.excel.totalGrossAmount = String(this.data[weekLength][18]);
      this.excel.totalNetAmount = String(this.data[weekLength][19]);

      notify(
        {
          message: `Excel was sucessfully processed!`,
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
    };
    reader.readAsBinaryString(target.files[0]);
  }

  async onSubmit() {
    await this.apiService.checkExcelIsValid(this.excel).subscribe((data) => {
      this.isValidExcel = data;
      const position = 'bottom right';
      const direction = 'up-push';

      if (this.isValidExcel == true) {
        this.apiService.addExcel(this.excel).subscribe((data) => {});
        this.resetData(this);
        setTimeout(() => {
          window.location.reload();
        }, 500);
        notify(
          {
            message: `The changes were submitted!`,
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
      } else {
        this.excelErrorList = data;
        console.log(data);

        for(let row = 0; row < this.excel.excelRow.length;row++) {
          for(let col = 0; col < this.excel.excelRow[row].daysOfWork.length; col++) {
            document.getElementById("error-" + row + "c" + col + "1")?.classList.remove("bg-error");
            document.getElementById("error-" + row + "c" + col + "2")?.classList.remove("bg-error");
          }
          document.getElementById("error-" + row + "1")?.classList.remove("bg-error");
          document.getElementById("error-" + row + "2")?.classList.remove("bg-error");
          document.getElementById("error-" + row + "3")?.classList.remove("bg-error");
        }
        document.getElementById("error--1")?.classList.remove("bg-error");
        document.getElementById("error--2")?.classList.remove("bg-error");

        notify(
          {
            message: `Looks like you have some errors, check on the table the fileds you must review!`,
            height: 80,
            width: 180,
            minWidth: 150,
            type: 'error',
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
      }
    });
  }

  checkNewEmployees() {
    this.apiService
      .checkNewEmployees(this.EmployeeList)
      .subscribe((response) => {
        this.EmployeeList = response;
        this.isCheckingEmployees = false;
        if (this.EmployeeList.length == 0) {
          this.newEmployeesToAdd = false;
        } else {
          this.popupVisible = true;
          this.newEmployeesToAdd = null;
        }
      });

    this.isCheckingEmployees = true;
  }

  checkError(rowNumber: number, fieldName: string, colNumber: number = -1): string {
    if (
      this.excelErrorList.some(
        (x) => x.rowNumber == rowNumber && x.fieldName == fieldName && colNumber == -1
      )
    ) {
      if (fieldName == 'total gross') {
        document.getElementById('error--1')?.classList.add('bg-error');
      } else if (fieldName == 'total net') {
        document.getElementById('error--2')?.classList.add('bg-error');
      } else if (fieldName == 'total hours') {
        document
          .getElementById('error-' + rowNumber + '1')
          ?.classList.add('bg-error');
      } else if (fieldName == 'gross amount') {
        document
          .getElementById('error-' + rowNumber + '2')
          ?.classList.add('bg-error');
      } else if (fieldName == '-tax') {
        document
          .getElementById('error-' + rowNumber + '3')
          ?.classList.add('bg-error');
      }
      let message =
          'Wrong calculation, the correct value is ' +
          this.excelErrorList[
            this.excelErrorList.findIndex((object) => {
              return (
                object.rowNumber == rowNumber && object.fieldName == fieldName
              );
            })
          ].correctValue + '.';
      return message;
    }
    else if(this.excelErrorList.some(
      (x) => x.rowNumber == rowNumber && x.fieldName == fieldName && x.colNumber == colNumber
    )) {
      if (fieldName == 'project') {
        document
          .getElementById('error-' + rowNumber + "c" + colNumber + '1')
          ?.classList.add('bg-error');
      } else if (fieldName == 'task') {
        document
          .getElementById('error-' + rowNumber + "c" + colNumber + '2')
          ?.classList.add('bg-error');
      }
      let message = 'This value is not valid, please make some changes.';
      return message;
    }

    return '';
  }

  resetData(that: any) {
    that.popupVisible = false;
    that.excel = new Excel();
    that.isCheckingEmployees = null;
    that.newEmployeesToAdd = true;
    that.isValidExcel = false;
    that.dataCalcVerified = null;
  }

  checkWorkHours(workedHours: string): boolean {
    if(workedHours == "Off") return true;
    return false;
  }

}
