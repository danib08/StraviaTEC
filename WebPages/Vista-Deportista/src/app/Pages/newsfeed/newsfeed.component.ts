import { Component, OnInit, AfterViewChecked} from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { GetService } from 'src/app/Services/Get/get-service';
import { MapService } from 'src/app/Services/Map/map.service';

@Component({
  selector: 'app-newsfeed',
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})

/**
 * Newsfeed component where the athelete is able to see all the activities
 * posted by its friend
 */
export class NewsfeedComponent implements OnInit, AfterViewChecked {

  mapsInitialized = false;
  ActivitiesArray: ActivityModel[] = [];

 /**
  * @param getSvc service for GET requests to the API
  * @param cookieSvc service for cookie creating to store the username
  * @param mapSvc service for loading gpx files into a leaflet map
  * @param router used to re-route the user to different pages
  */
  constructor(private getSvc: GetService, private cookieSvc: CookieService, private mapSvc: MapService, 
    private router: Router) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
    this.getFeed();
  }

  /**
   * Called after view is initialized so maps are displayed
   */
  ngAfterViewChecked(): void {
    this.displayMaps();
  }

  /**
   * Gets the acitivity feed for the athlete
   */
  getFeed() {
    this.getSvc.getFeed(this.cookieSvc.get("Username")).subscribe(
      res =>{
        this.ActivitiesArray = res;
      }, err => {
        alert("Ha ocurrido un error");
      }
    );
  }

  /**
   * Iterates through the ActivitiesArray to decode each route (.gpx)
   */
  displayMaps() {
    if (!this.mapsInitialized) {
      for (let i = 0; i < this.ActivitiesArray.length; i++) {
        this.routeDecode(this.ActivitiesArray[i].route, this.ActivitiesArray[i].id);
      }
    }
  }

  /**
   * Decodes each .gpx file and then displays it as a map
   * @param base64 encoded content of the gpx file
   * @param mapid ID of map div on the HTML file
   */
  routeDecode(base64: string, mapid: string) {
    let decoded = atob(base64);
    this.mapSvc.plotActivity(decoded, mapid);
    this.mapsInitialized = true;
  }


  /**
   * Reroutes the page to the Comments component
   * @param activityID the ID of the activity that comments want to be seen
   */
  goToComments(activityID: string) {
    this.cookieSvc.set("ActivityID", activityID); 
    this.router.navigate(["comments"]);
  }
}
