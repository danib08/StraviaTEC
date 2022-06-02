import { Component, OnInit} from '@angular/core';
import { ActivityModel } from 'src/app/Models/activity-model';
import { GetService } from 'src/app/Services/Get/get-service';
import { MapService } from 'src/app/Services/Map/map.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css']
})

export class ActivityComponent implements OnInit {

  constructor(private getService: GetService, private mapService: MapService) { }

  ngOnInit(): void { 
    this.getActivity();
  }

  getActivity() {
    this.getService.getActivity("Act5").subscribe(
      res =>{
        this.routeDecode(res.route);
      }, err => {
        alert("Ha ocurrido un error")
      }
    );
  }

  routeDecode(base64: string) {
    let decoded = atob(base64);
    var gpxFile = new File([decoded], 'sample.gpx');
    //saveAs(gpxFile);
    this.mapService.plotActivity(decoded);

    //FileSaver.saveAs(decoded, "sample.gpx");
  }


}
