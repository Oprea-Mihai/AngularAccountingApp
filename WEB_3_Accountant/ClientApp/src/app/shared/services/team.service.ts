import { Injectable } from '@angular/core';

export class TeamMember {
  id: number;

  firstName: string;

  lastName: string;

  picture: string;

  birthDate: string;

  nickName: string;

  insta: string;
}

const teamMembers: TeamMember[] = [
  {
    id: 1,
    firstName: 'Mihai-Lucian',
    lastName: 'Oprea',
    picture: 'https://i.imgur.com/uJI0ifa.png',
    birthDate: '03/12/2001',
    nickName: 'Mike',
    insta: 'https://www.instagram.com/micul_mike/',
  },
  {
    id: 2,
    firstName: 'Adrian',
    lastName: 'Cimpianu',
    picture: 'https://i.imgur.com/UL0RBp7.jpg',
    birthDate: '26/01/2002',
    nickName: 'Adi',
    insta: 'https://www.instagram.com/adi_cimpianu/',
  },
  {
    id: 3,
    firstName: 'Ionut',
    lastName: 'Dragan',
    picture: 'https://i.pinimg.com/736x/a8/f3/ab/a8f3abc3acfdbf60688a663743555eda.jpg',
    birthDate: '02/11/2001',
    nickName: 'ionutdrg45',
    insta: 'https://www.instagram.com/ionutdrg45/',
  },
  {
    id: 4,
    firstName: 'Andrei',
    lastName: 'Biciin',
    picture: 'https://i.imgur.com/YkEWNrY.png',
    birthDate: '16/03/1999',
    nickName: 'andreibiciin',
    insta: 'https://www.instagram.com/andreibiciin/',
  },
  {
    id: 5,
    firstName: 'Patricia',
    lastName: 'Anghelache',
    picture: 'https://i.imgur.com/FZlqyjm.png',
    birthDate: '22/11/2002',
    nickName: 'Pati',
    insta: 'https://www.instagram.com/patricia.anelis/',
  }
];

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  constructor() {}

  getTeamMembers(): TeamMember[] {
    return teamMembers;
  }
}
