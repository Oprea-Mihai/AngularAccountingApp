import { TeamService } from './../../shared/services/team.service';
import { Component } from '@angular/core';
import { TeamMember } from 'src/app/shared/services/team.service';

@Component({
  selector: 'app-team-page',
  templateUrl: 'team.component.html',
  styleUrls: ['./team.component.scss'],
  providers: [],
  preserveWhitespaces: true,
})

export class TeamComponent {
  currentMember: TeamMember = new TeamMember();
  teamMembers: TeamMember[] = [];
  popupVisible = false;
  instagramRedirect: any;
  closeButtonOptions: any;
  positionOf: string;

  constructor(private service: TeamService) {
    const that = this;
    this.instagramRedirect = {
      text: 'Insta',
      onClick(e:any) {
        window.open(that.currentMember.insta, '_blank');
      },
    };
    this.closeButtonOptions = {
      text: 'Close',
      onClick(e:any) {
        that.popupVisible = false;
      },
    };
  }
  ngOnInit(): void {
    this.teamMembers = this.service.getTeamMembers();
  }

  detailsButtonMouseEnter(id:any) {
    this.positionOf = `#image${id}`;
  }

  showInfo(tm: any) {
    this.currentMember = tm;
    this.popupVisible = true;
  }
}
