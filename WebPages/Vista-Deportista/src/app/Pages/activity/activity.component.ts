import { Component, OnInit, AfterViewInit } from '@angular/core';
import { MapService } from 'src/app/Services/Map/map.service';
import { ActivityModel } from 'src/app/Models/activity-model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})
export class ActivityComponent implements OnInit {

  activity: any;
  activityNamen = '';
  gpx = '';
  activityDate = '';
  activityDuration = '';
  activityKilometers = 0;
  activityType = '';

  constructor(private mapService: MapService, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

}
