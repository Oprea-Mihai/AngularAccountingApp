import { Component, OnInit } from '@angular/core';
import notify from 'devextreme/ui/notify';
import { ProjectsUndoneService } from 'src/app/shared/services/projects-undone.service';

@Component({
  selector: 'projects-undone',
  providers: [ProjectsUndoneService],
  templateUrl: './projects-undone.component.html',
  styleUrls: ['./projects-undone.component.scss'],
  preserveWhitespaces: true
})
export class ProjectsUndoneComponent implements OnInit {
  projects: string[];
  showMarkFinished: string = "off";
  popupVisible = false;
  confirmButtonOptions: any;
  closeButtonOptions: any;
  positionOf: string;

  constructor(private service: ProjectsUndoneService) {
    const that = this;
    this.confirmButtonOptions = {
      icon: 'check',
      text: 'Mark finished',
      onClick(e: any) {
        const message = `Project ${that.showMarkFinished} was marked as finished.`;
        const position = "bottom right";
        const direction = "up-push";
        const indexOfObject = that.projects.indexOf(that.showMarkFinished);

        that.projects.splice(indexOfObject, 1);

        that.service.setProjectFinished(that.showMarkFinished).subscribe();

        that.showMarkFinished = "off";

        that.popupVisible = false;
        notify({
          message,
          height: 45,
          width: 180,
          minWidth: 150,
          type: "success",
          displayTime: 3500,
          animation: {
            show: {
              type: 'fade', duration: 400, from: 0, to: 1,
            },
            hide: { type: 'fade', duration: 40, to: 0 },
          },
        },
        { position, direction });
      },
    };
    this.closeButtonOptions = {
      text: 'Close',
      onClick(e: any) {
        that.popupVisible = false;
      },
    };
  }

  ngOnInit(): void {
    this.service.getUndoneProjects().subscribe(response => {
      this.projects  = response;
    });
  }

  detailsButtonMouseEnter(projectName: string) {
    this.positionOf = `#pos${projectName}`;
  }

  showInfo() {
    this.popupVisible = true;
  }

  markFinishedButton(project: string) {
    this.showMarkFinished = project;
  }
}
