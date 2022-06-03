import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Challenge } from 'src/app/Models/challenge';
import { Competition } from 'src/app/Models/competition';
import { GetService } from 'src/app/Services/Get/get-service';
import { AthleteInChallenge } from 'src/app/Models/athlete-in-challenge';
import { AthleteInCompetition } from "src/app/Models/athlete-in-competition";
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit {

  flag = false;
  selectedCompetition = '';
  eventType = '';
  challengesArray: Challenge[] =[]; 
  competitionsArray: Competition[] =[]; 

  athleteInChallenge: AthleteInChallenge = {
    athleteid: '',
    challengeid: '',
    status: ''
  }

  athleteInCompetition: AthleteInCompetition = {
    athleteid: '',
    competitionid: '',
    status: ''
  }

  constructor(private getService: GetService, private postService: PostService, private cookieSvc:CookieService) { }

  ngOnInit(): void {
  }

  radioSelect(event: Event) {
    if ((event.target as HTMLInputElement).value == 'Challenge'){
      this.getService.getChallenges().subscribe(
        res => {
          this.challengesArray = res;
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
    else if ((event.target as HTMLInputElement).value == 'Competition'){
      this.getService.getCompetitions().subscribe(
        res => {
          this.competitionsArray = res;
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
    this.eventType = (event.target as HTMLInputElement).value;
  }

  joinChallenge(chllngID:string, $event: MouseEvent) {
    ($event.target as HTMLButtonElement).disabled = true;

    this.athleteInChallenge.athleteid = this.cookieSvc.get('Username');
    this.athleteInChallenge.challengeid = chllngID;
    this.postService.createAthleteInChallenge(this.athleteInChallenge).subscribe(
      res => {
        console.log(res);
      },
      err => {
        alert('No se pudo inscribir al reto')
      }
    );
  }

  joinCompetition(compID:string, $event: MouseEvent) {
    ($event.target as HTMLButtonElement).disabled = true;

    this.athleteInCompetition.athleteid = this.cookieSvc.get('Username');
    this.athleteInCompetition.competitionid = compID;
    this.postService.createAthleteInCompetition(this.athleteInCompetition).subscribe(
      res => {
        console.log(res);
      },
      err => {
        alert('No se pudo inscribir a la competencia')
      }
    );
  }

  onClick(compID:string) {
    this.flag = true;
    this.selectedCompetition = compID;
  }
}
