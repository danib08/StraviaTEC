import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { AthleteInChallenge } from 'src/app/Models/athlete-in-challenge';
import { AthleteInCompetition } from 'src/app/Models/athlete-in-competition';
import { ActivityInChallenge } from 'src/app/Models/activity-in-challenge';
import { Challenge } from 'src/app/Models/challenge';
import { Competition } from 'src/app/Models/competition';
import { GetService } from 'src/app/Services/Get/get-service';
import { PostService } from 'src/app/Services/Post/post-service';
import { PutService } from 'src/app/Services/Put/put-service';

@Component({
  selector: 'app-create-activity',
  templateUrl: './create-activity.component.html',
  styleUrls: ['./create-activity.component.css']
})

/**
 * Create Activity component where the athlete registers its own activities
 */
export class CreateActivityComponent implements OnInit {

  eventType = '';
  challengeSelected = '';
  competitionSelected = '';
  challengesArray: Challenge[] =[]; 
  competitionsArray: AthleteInCompetition[] =[]; 
  haveSelectedChallenge = false;
  haveSelectedCompetition = false;
  gpxEncoded = '';
  
  // Holds info about the challenge related to the activity
  currentChallenge: Challenge = {
    id: '',
    name: '',
    enddate: '',
    startdate: '',
    privacy: '',
    kilometers: 0,
    type: ''
  }
  
  // Holds info about the competition related to the activity
  currentCompetition: Competition = {
    id: '',
    name: '',
    route: '',
    date: '',
    privacy: '',
    bankaccount: '',
    price: 0,
    activityid: ''
  }

  // Model for the newly created activity
  activity: ActivityModel = {
    id: '',
    name: '',
    route: '',
    date: '',
    duration: '',
    kilometers: 0,
    type: '',
    athleteusername: ''
  }

  // Info for the activity related to the competition
  activityCompetition: ActivityModel = {
    id: '',
    name: '',
    route: '',
    date: '',
    duration: '',
    kilometers: 0,
    type: '',
    athleteusername: ''
  }

  // Model for associating this activity with a challenge
  activityInChallenge: ActivityInChallenge = {
    activityid: '',
    challengeid: ''
  }

  // Model for changing the competition status to finished
  athleteInCompetition: AthleteInCompetition = {
    athleteid: '',
    competitionid: '',
    status: '',
    receipt: '',
    duration: ''
  }

 /**
  * Creates the Create Activity component
  * @param postService service for POST requests to the API
  * @param getService service for GET requests to the API
  * @param putService service for PUT requests to the API
  * @param cookievc service for cookie creating to store the username
  */
  constructor(private postService: PostService, private getService: GetService, 
    private putService: PutService, private cookieSvc:CookieService) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
  }

  /**
   * If challenge or competition is selected as an option, all events where the user
   * is subscribed are requested to the API
   * @param event 
   */
  radioSelect(event: Event) {
      if ((event.target as HTMLInputElement).value == 'Challenge'){
        this.getService.getOnGoingChallenges(this.cookieSvc.get('Username')).subscribe(
          res => {
            this.challengesArray = res;
          },
          err=>{
            alert('Ha ocurrido un error')
          }
        );
      }
      else if ((event.target as HTMLInputElement).value == 'Competition'){
        // PROBAR CON NUEVOS URLS
        this.getService.getAcceptedCompetitions(this.cookieSvc.get('Username')).subscribe(
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

  /**
   * Gets the info associated to the challenge the athlete chose
   */
  getChallengeInfo(){
    this.getService.getChallenge(this.challengeSelected).subscribe(
      res => {
        this.currentChallenge = res;
        this.haveSelectedChallenge = true;
        this.activity.type = this.currentChallenge.type;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  /**
   * Gets the info associated to the competition the athlete chose
   */
  getCompetitonInfo(){
    this.getService.getCompetition(this.competitionSelected).subscribe(
      res => {
        this.currentCompetition = res;
        this.getActivityType();
        this.haveSelectedCompetition = true;
        this.activity.name = this.currentCompetition.name;
        this.activity.date = this.currentCompetition.date;
        this.activity.route = this.currentCompetition.route;
      },
      err=>{
        alert('Ha ocurrido un error')
      }
    );
  }

  /**
   * Gets the activity related to the competition the athlete chose
   */
  getActivityType() {
      this.getService.getActivity(this.currentCompetition.activityid).subscribe(
        res => {
          this.activityCompetition = res;
          this.activity.type = this.activityCompetition.type;
        },
        err=>{
          alert('Ha ocurrido un error')
        }
      );
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
      let gpxRead = fileReader.result as string;
      self.encode64(gpxRead);
    }
    fileReader.readAsText(file);
  }

  /**
   * Encodes string from the .gpx file to base 64 and sets it
   * to the activity route
   */
  encode64(fileText: string) {
    let gpxEncoded = btoa(fileText);
    this.activity.route = gpxEncoded;
  }

  /**
   * Creates the final activity to be POSTED to the API and also
   * the ActivityInChallenge object if its associated to a challenge
   */
  createActivity(){
    this.activity.athleteusername = this.cookieSvc.get('Username');
    this.postService.createActivity(this.activity).subscribe(
      res =>{
        if (res == "") {
          alert("Actividad creada satisfactoriamente.");
          this.createChallenge();
        }
        else {
          if (res[0].message_id == 2601) {
            alert("El ID de la actividad ya existe.");
          }
        }
      }
    );

    if (this.eventType == 'Competition') {

      for (let i = 0; i < this.competitionsArray.length; i++) {
        if (this.competitionsArray[i].competitionid == this.competitionSelected) {
          this.athleteInCompetition = this.competitionsArray[i];
          break;
        }
      }

      this.athleteInCompetition.status = "Finalizado";
      this.athleteInCompetition.duration = this.activity.duration;

      this.putService.updateAthleteInCompetition(this.athleteInCompetition).subscribe(
        res => {
        }, err => {
          alert('Ha ocurrido un error')
        }
      );
    }

    //location.reload();
  }

  createChallenge() {
    if (this.eventType == 'Challenge') {
      this.activityInChallenge.activityid = this.activity.id;
      this.activityInChallenge.challengeid = this.currentChallenge.id;
      this.postService.createActivityInChallenge(this.activityInChallenge).subscribe(
        res =>{
        }, err => {
          console.log(err);
          alert("Ha ocurrido un error")
        }
      );
    }
  }

}
