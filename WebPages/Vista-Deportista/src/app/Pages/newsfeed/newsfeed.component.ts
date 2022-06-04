import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { GetService } from 'src/app/Services/Get/get-service';

@Component({
  selector: 'app-newsfeed',
  templateUrl: './newsfeed.component.html',
  styleUrls: ['./newsfeed.component.css']
})

/**
 * Newsfeed component where the athelete is able to see all the activities
 * posted by its friend
 */
export class NewsfeedComponent implements OnInit {

  ActivitiesArray: ActivityModel[] = [];

  constructor(private getSvc: GetService, private cookieSvc: CookieService) { }

  /**
   * Called after Angular has initialized all data-bound properties
   */
  ngOnInit(): void {
    this.getSvc.getFeed(this.cookieSvc.get("Username")).subscribe(
      res =>{
        console.log(res);
        this.ActivitiesArray = res;
      }, err => {
        alert("Ha ocurrido un error");
      }
    );
  }
}
