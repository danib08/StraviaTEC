import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { AthleteInChallenge } from 'src/app/Models/athlete-in-challenge';
import { AthleteInCompetition } from 'src/app/Models/athlete-in-competition';
import { Challenge } from 'src/app/Models/challenge';
import { Competition } from 'src/app/Models/competition';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';


@Component({
  selector: 'app-create-activity',
  templateUrl: './create-activity.component.html',
  styleUrls: ['./create-activity.component.css']
})

export class CreateActivityComponent implements OnInit {

  eventType = '';
  challengeSelected = '';
  competitionSelected = '';
  challengesArray: AthleteInChallenge[] =[]; 
  competitionsArray: AthleteInCompetition[] =[]; 
  haveSelectedChallenge = false;
  haveSelectedCompetition = false;
  
  currentChallenge: Challenge = {
    ID: '',
    Name: '',
    EndDate: '',
    StartDate: '',
    Privacy: '',
    Kilometers: 0,
    Type: ''
  }
  currentCompetition: Competition = {
    ID: '',
    Name: '',
    Route: '',
    Date: '',
    Privacy: '',
    BankAccount: '',
    Price: 0,
    ActivityID: ''
  }

  activity: ActivityModel = {
    ID: 0,
    Name: '',
    Route: '',
    Date: '',
    Duration: 0, 
    Kilometers: 0,
    Type: '',
    ChallengeID: 0
}

  constructor(private postService: PostService, private getService: GetService, private cookieSvc:CookieService) { }

  ngOnInit(): void {
  }

  createActivity(){
    this.postService.createActivity(this.activity).subscribe(
      res =>{
        location.reload();
      }, err => {
        alert("Ha ocurrido un error")
      }
    );
  }

  radioSelect(event: Event) {
      
      if ((event.target as HTMLInputElement).value == 'Challenge'){
        this.getService.getAthleteinChallenge(this.cookieSvc.get('Username')).subscribe(
          res => {
            this.challengesArray = res;
          },
          err=>{
            alert('Ha ocurrido un error')
          }
        );
      }
      else if ((event.target as HTMLInputElement).value == 'Competition'){
        this.getService.getAthleteinCompetition(this.cookieSvc.get('Username')).subscribe(
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

  getChallengeInfo(){
    this.getService.getChallenge(this.challengeSelected).subscribe(
      res => {
        this.currentChallenge = res;
        this.haveSelectedChallenge = true;
        this.activity.Type = this.currentChallenge.Type;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  getCompetitonInfo(){
    this.getService.getCompetition(this.competitionSelected).subscribe(
      res => {
        this.currentCompetition = res;
        this.haveSelectedCompetition = true;
        this.activity.Name = this.currentCompetition.Name;
        this.activity.Date = this.currentCompetition.Date;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

}
