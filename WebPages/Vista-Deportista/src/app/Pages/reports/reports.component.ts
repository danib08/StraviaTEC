import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AthleteInChallenge } from 'src/app/Models/athlete-in-challenge';
import { AthleteInCompetition } from 'src/app/Models/athlete-in-competition';
import { ChallengeReport } from 'src/app/Models/challenge-report';
import { CompetitionReport } from 'src/app/Models/competition-report';
import { GetService } from 'src/app/Services/Get/get-service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})

/**
 * Reports component where the user is able to see reports about his 
 * challenges and reports
 */
export class ReportsComponent implements OnInit {

  eventType = '';
  competitionSelected = '';
  challengeSelected = '';
  athleteCompetitions: AthleteInCompetition[] = [];
  athleteChallenges: AthleteInChallenge[] = [];
  competitionReport: CompetitionReport[] = [];
  challengeReport: ChallengeReport[] = [];

  /**
   * Creates the Reports component
   * @param getSvc service for GET requests to the API
   * @param cookieSvc service for cookie creating to store the username
   */
  constructor(private getSvc: GetService, private cookieSvc: CookieService) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
  }

  /**
   * Gets all challenges or competitions where the users is participating
   * in order to show information about them, according to the option they chose
   * on the radio button
   * @param event the radio button event
   */
  radioSelect(event: Event) {
    if ((event.target as HTMLInputElement).value == 'Challenge'){
      this.getAthleteChallenges();
    }
    else if ((event.target as HTMLInputElement).value == 'Competition'){
      this.getAthleteCompetitions();
    }
    this.eventType = (event.target as HTMLInputElement).value;
  }

  /**
   * Gets all of the challenges where the user is participating
   */
  getAthleteChallenges(){
    this.getSvc.getAthleteChallenges(this.cookieSvc.get('Username')).subscribe(
      res=>{
        this.athleteChallenges = res;
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }

  /**
   * Gets all of the competitions where the user is participating and accepted
   */
  getAthleteCompetitions(){
    //PROBAR NUEVO URL
    this.getSvc.getAcceptedCompetitions(this.cookieSvc.get('Username')).subscribe(
      res=>{
        console.log(res);
        this.athleteCompetitions = res;
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }

  /**
   * Gets the ranking of the currently selected competition
   */
  getCurrentCompetitionReport(){
    this.getSvc.getPositionsReport(this.competitionSelected).subscribe(
      res=>{
        this.competitionReport = res;
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }

  /**
   * Gets the progress information about the currently selected challenge
   */
  getCurrentChallengeReport(){
    this.getSvc.getChallengeReport(this.challengeSelected).subscribe(
      res=>{
        this.challengeReport = res;
      },
      err=> {
        alert('Ha ocurrido un error')
      }
    );
  }

}
