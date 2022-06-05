import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AthleteInCompetition } from 'src/app/Models/athlete-in-competition';
import { Competition } from 'src/app/Models/competition';
import { GetService } from 'src/app/Services/Get/get-service';
import { PutService } from 'src/app/Services/Put/put-service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-accept-registration',
  templateUrl: './accept-registration.component.html',
  styleUrls: ['./accept-registration.component.css']
})

export class AcceptRegistrationComponent implements OnInit {


  selectedCompetition = '';
  selectedAthlete = '';
  athletesArray: AthleteInCompetition[] = [];

  athlete: AthleteInCompetition = {
    athleteid: '',
    competitionid: '',
    status: '',
    duration: 0,
    receipt: ''
  }
  athleteCreatedCompetitions: Competition[] = [];
  constructor(private getService: GetService, private putService: PutService,private cookieSvc:CookieService) { }

  ngOnInit(): void {
    this.getAthleteCreatedCompetitions();
  }

  getAthleteCreatedCompetitions(){
    this.getService.getAthleteCreatedCompetitions(this.cookieSvc.get('Username')).subscribe(
      res => {
        this.athleteCreatedCompetitions = res;
      }, err => {
        alert('Ha ocurrido un error')
      }
    );
  }

  getParticipants(){
    this.getService.getAthleteRegister(this.selectedCompetition).subscribe(
      res => {
        this.athletesArray = res;
      }, err => {
        alert('Ha ocurrido un error')
      }
    );
  }

  getCurrentAthlete(){
    for(let i=0; i < this.athletesArray.length; i++){
      if(this.athletesArray[i].athleteid == this.selectedAthlete){
        this.athlete = this.athletesArray[i];
        break;
      }
    }
  }

  downloadReceipt(){
    const source = `data:application/pdf;base64,${this.athlete.receipt}`;
    const link = document.createElement("a");
    link.href = source;
    link.download = `${'recibo'}.txt`
    link.click();
  }

  setValueAthlete(){
    this.putService.modifyAthleteStatusInCompetition(this.athlete).subscribe(
      res => {
        location.reload();
      }, err => {
        alert('Ha ocurrido un error')
      }
    );
  }
}
