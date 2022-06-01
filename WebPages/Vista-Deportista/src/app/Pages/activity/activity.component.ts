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
    this.getService.getActivity("Act7").subscribe(
      res =>{
        this.activity = res;
        console.log(res);
      }, err => {
        alert("Ha ocurrido un error")
      }
    );
 
    this.equisde();

    /*const fileName = 'route.gpx';
    const fileBlob = this.dataURItoBlob(this.activity.Route);
    const gpxFile = new File([fileBlob], fileName, {type: 'text'});

    var gpx = "";
    this.mapService.plotActivity(gpxFile);*/
  }

  dataURItoBlob(dataURI: string) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], {type: 'text/plain;charset=utf-8'});    
    return blob;
  }

  equisde() {
    const blob = this.dataURItoBlob(this.activity.Route);
    var FileSaver = require('file-saver');
    FileSaver.saveAs(blob, "sample.gpx");
  }

}
