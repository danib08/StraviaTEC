import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { AthleteFollower } from 'src/app/Models/athlete-follower';
import { AthleteModel } from 'src/app/Models/athlete-model';
import { AthleteSearch } from 'src/app/Models/athlete-search';
import { PostService } from 'src/app/Services/Post/post-service';

@Component({
  selector: 'app-athletes-search',
  templateUrl: './search-athletes.component.html',
  styleUrls: ['./search-athletes.component.css']
})

/**
 * Search Athletes component where the user can search for other
 * athletes on the app and follow them.
 */
export class SearchAthletesComponent implements OnInit {

  AthleteName: String = '';
  AthletesArray: AthleteModel[] = [];
  State = false;

  // Model for searching other athletes according to name and last name
  athleteSearch: AthleteSearch = {
    name: '',
    lastname: ''
  }

  // Model that will indicate that the user follows a specified athlete
  athleteFollower: AthleteFollower = {
    athleteid: '',
    followerid: ''
  }
  
  /**
   * Creates the Search Athletes component
   * @param postSvc service for POST requests to the API
   * @param cookieSvc service for cookie creating to store the username
   */
  constructor(private postSvc: PostService, private cookieSvc:CookieService) { }

   /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
  }

  /**
   * Obtains a list of all the athletes that match the name and
   * last name the user entered
   */
  getAthletes(){
    if (this.AthleteName != '') {
      var splitted = this.AthleteName.split(" ", 2); 

      if (splitted.length > 0) {
        if (splitted.length == 1) {
          this.athleteSearch.name = splitted[0];
          this.athleteSearch.lastname = "";
        }
        else if (splitted.length == 2) {
          this.athleteSearch.name = splitted[0];
          this.athleteSearch.lastname = splitted[1];
        }

        this.postSvc.searchAthletes(this.athleteSearch).subscribe(
        res => {
          this.AthletesArray = res;

          this.AthletesArray.forEach((element, index) => {
            let username = this.cookieSvc.get("Username");
            if (element.username == username) {
              this.AthletesArray.splice(index, 1);
            }
          });

          this.State = true;
        },
        err => {
          alert('No se encontraron atletas coincidentes.');
        }
        );
      }
    }
  }

  /**
   * Lets the user follow the selected athlete
   * @param username the username of the athlete to follow
   * @param $event triggered by a mouse click
   */
  followAthlete(username: string,  $event: MouseEvent){
    ($event.target as HTMLButtonElement).disabled = true;

    this.athleteFollower.athleteid = this.cookieSvc.get('Username');
    this.athleteFollower.followerid = username;
    this.postSvc.addFollower(this.athleteFollower).subscribe(
      res => {
        alert('Atleta seguido exitosamente');
      },
      err => {
        alert('No se pudo seguir al atleta');
      }
    );
  }
}
