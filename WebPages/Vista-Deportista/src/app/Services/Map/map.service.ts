import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

var apiToken = environment.MAPBOX_API_KEY;
declare var leaflet: any;

const defaultCoords: number[] = [39.8282, -98.5795]
const defaultZoom: number = 3

@Injectable({
  providedIn: 'root'
})

/**
 * Receives the content of a gpx file and shows it on-screen
 */
export class MapService {

  constructor() { }

  /**
   * Uses Leaflet and its GPX plugin to display maps on the HTML file
   * @param gpx the string content of the gpx file
   * @param mapId the ID of the HTML div element where the map will be displayed
   */
  plotActivity(gpx: any, mapId: string){

    var map = leaflet.map(mapId).setView(defaultCoords, defaultZoom);

    leaflet.tileLayer('https://api.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
      attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
      maxZoom: 18,
      minZoom: 3,
      id: 'mapbox.satellite',
      accessToken: apiToken
    }).addTo(map);

    new leaflet.GPX(gpx, {async: true}).on('loaded', function(e: { target: { getBounds: () => any; }; }) {
      map.fitBounds(e.target.getBounds());
    }).addTo(map);
  }
}