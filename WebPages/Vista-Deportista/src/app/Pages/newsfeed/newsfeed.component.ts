import { Component, OnInit, AfterViewChecked} from '@angular/core';
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

  constructor(private getSvc: GetService, private cookieSvc: CookieService, private mapSvc: MapService) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
    this.getFeed();
  }

  ngAfterViewChecked(): void {
    this.displayMaps();
  }

  getFeed() {
    this.getSvc.getFeed(this.cookieSvc.get("Username")).subscribe(
      res =>{
        this.ActivitiesArray = res;
      }, err => {
        alert("Ha ocurrido un error");
      }
    );
  }

  displayMaps() {
    if (!this.mapsInitialized) {
      for (let i = 0; i < this.ActivitiesArray.length; i++) {
        this.routeDecode(this.ActivitiesArray[i].route, this.ActivitiesArray[i].id);
      }
    }
  }

  routeDecode(base64: string, mapid: string) {
    let decoded = atob(base64);
    this.mapSvc.plotActivity(decoded, mapid);
    this.mapsInitialized = true;
  }
}
