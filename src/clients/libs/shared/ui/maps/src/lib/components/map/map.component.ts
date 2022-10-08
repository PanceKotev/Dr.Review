import { Component, OnInit } from '@angular/core';
import { LatLng, Map,latLng, MapOptions, tileLayer } from 'leaflet';

@Component({
  selector: 'drreview-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  public currentLocation: LatLng  | undefined;
  public map: Map | undefined;
  public options: MapOptions =
  {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&amp;copy; OpenStreetMap contributors'
      })
    ],
    zoom: 9,
    center: latLng([ 41.6771844,21.6609852 ])
  };


  public ngOnInit(): void {
      if (navigator.geolocation){
        navigator.geolocation.getCurrentPosition((pos) => {
          if(pos && this.map){
            this.map.flyTo(latLng(pos.coords.latitude, pos.coords.longitude, pos.coords.accuracy), 10);
          }
        });
      }
  }

  public mapFinishedLoading(map: Map): void {
    this.map = map;
  }
}
