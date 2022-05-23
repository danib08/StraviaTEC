import { Component, OnInit} from '@angular/core';
import { ActivityModel } from 'src/app/Models/activity-model';
import { GetService } from 'src/app/Services/Get/get-service';
import { MapService } from 'src/app/Services/Map/map.service';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})

export class ActivityComponent implements OnInit {
  //map!: L.Map;  // ! postfix operator (ignores this case)

  activity: ActivityModel = {
    ID: '',
    Name: '',
    Route: '',
    Date: '',
    Duration: '',
    Kilometers: 0,
    Type: '',
    AthleteUsername: ''
  }

  constructor(private getService: GetService, private mapService: MapService) { }

  ngOnInit(): void { 
    /*this.getService.getActivity("1").subscribe(
      res =>{
        this.activity = res;
      }, err => {
        alert("Ha ocurrido un error")
      }
    );*/

    var gpx = "../../assets/gpx/sample.gpx";
    this.mapService.plotActivity(gpx);
  }
}
