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

/**
 * Subscriptions component where the user registers itself on challenges
 * or competitions
 */
export class SubscriptionsComponent implements OnInit {

  flag = false;
  selectedCompetition = '';
  eventType = '';
  challengesArray: Challenge[] =[]; 
  competitionsArray: Competition[] =[]; 

  athleteInChallenge: AthleteInChallenge = {
    athleteid: '',
    challengeid: '',
    status: 'En curso',
    kilometers: 0
   }

  athleteInCompetition: AthleteInCompetition = {
    athleteid: '',
    competitionid: '',
    status: 'No Aceptado',
    receipt: '',
    duration: '00:00:00'
  }

  /**
   * Creates the Subscriptions component
   * @param getService service for GET requests to the API
   * @param postService service for POST requests to the API
   * @param cookieSvc service for cookie creating to store the username
   */
  constructor(private getService: GetService, private postService: PostService, private cookieSvc:CookieService) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {}

  /**
   * Gets all challenges or competitions available for subscriptions
   * depending on the user's radio button choice
   * @param event the radio button event
   */
  radioSelect(event: Event) {
    if ((event.target as HTMLInputElement).value == 'Challenge'){
      this.getService.getNotJoinedChallenges(this.cookieSvc.get("Username")).subscribe(
        res => {
          this.challengesArray = res;
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
    else if ((event.target as HTMLInputElement).value == 'Competition'){
      this.getService.getNotJoinedCompetitions(this.cookieSvc.get("Username")).subscribe(
        res => {
          console.log(res);
          this.competitionsArray = res;
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
    }
    this.eventType = (event.target as HTMLInputElement).value;
  }

  /**
   * Adds the user to the selected challenge
   * @param chllngID the ID of the selected challenge
   * @param $event the mouse event
   */
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
    location.reload();
  }

  
  /**
   * Reads the content of the .gpx when uploaded
   * @param fileList list of files uploaded
   */
   public onChange(fileList: FileList): void {

    let file = fileList[0];
    let fileReader: FileReader = new FileReader();
    let self = this;

    fileReader.onloadend = function(x) {
      let receiptRead = fileReader.result as string;
      self.encode64(receiptRead);
    }
    fileReader.readAsText(file);
  }

  /**
   * Encodes string from the .gpx file to base 64 and sets it
   * to the activity route
   */
  encode64(fileText: string) {
    let receiptEncoded = btoa(fileText);
    this.athleteInCompetition.receipt = receiptEncoded;
  }

  /**
   * Adds the user to the selected competition
   * @param compID the ID of the selected competition
   * @param $event the mouse event
   */
  joinCompetition(compID:string, $event: MouseEvent) {
    ($event.target as HTMLButtonElement).disabled = true;

    this.athleteInCompetition.athleteid = this.cookieSvc.get('Username');
    this.athleteInCompetition.competitionid = compID;
    console.log(this.athleteInCompetition);
    this.postService.createAthleteInCompetition(this.athleteInCompetition).subscribe(
      res => {
        alert('Inscripción exitosa');
        location.reload()
      },
      err => {
        alert('No se pudo inscribir a la competencia');
      }
    );
    //location.reload();
  }

  /**
   * This method gets called when a competition is chosen by the user, and the flag gets 
   * activated in order to show the payment option
   * @param compID the ID of the selected competition
   */
  onClick(compID: string) {
    this.flag = true;
    this.selectedCompetition = compID;
  }
}
