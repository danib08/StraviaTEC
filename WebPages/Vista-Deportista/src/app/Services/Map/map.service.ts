import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

var apiToken = environment.MAPBOX_API_KEY;
declare var omnivore: any;
declare var leaflet: any;

const defaultCoords: number[] = [39.8282, -98.5795]
const defaultZoom: number = 3

@Injectable({
  providedIn: 'root'
})
export class MapService {

  constructor() { }

  plotActivity(gpx: any){
    var myStyle = {
      "color": "#3949AB",
      "weight": 5,
      "opacity": 0.95
    };

    var map = leaflet.map('map').setView(defaultCoords, defaultZoom);

    leaflet.tileLayer('https://api.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
      attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
      maxZoom: 18,
      minZoom: 3,
      id: 'mapbox.satellite',
      accessToken: apiToken
    }).addTo(map);

    var customLayer = leaflet.geoJson(null, {
      style: myStyle
    });

    new leaflet.GPX(gpx, {async: true}).on('loaded', function(e: { target: { getBounds: () => any; }; }) {
      map.fitBounds(e.target.getBounds());
    }).addTo(map);

    /*var gpxLayer = omnivore.gpx(gpx, null, customLayer)
    .on('ready', function() {
      map.fitBounds(gpxLayer.getBounds());
    }).addTo(map);*/
  }
  
}