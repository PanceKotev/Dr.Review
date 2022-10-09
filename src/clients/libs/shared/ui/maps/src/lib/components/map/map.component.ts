import { Component, OnInit } from '@angular/core';
import { LatLng, Map,latLng, MapOptions, tileLayer, Marker, marker, icon, Layer } from 'leaflet';

@Component({
  selector: 'drreview-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  public currentLocation: LatLng  | undefined;
  public markers: Layer[] = [];
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

  public summit: Marker = marker([ 46.8523, -121.7603 ], {

    icon: icon({
      iconSize: [ 25, 41 ],
      iconAnchor: [ 13, 41 ],
      iconUrl: 'leaflet/marker-icon.png',
      shadowUrl: 'leaflet/marker-shadow.png'
    })
  });

  public ngOnInit(): void {
      if (navigator.geolocation){
        navigator.geolocation.getCurrentPosition((pos) => {
          if(pos && this.map){
            this.map.flyTo(latLng(pos.coords.latitude, pos.coords.longitude, pos.coords.accuracy), 10);
            this.markers.push(this.getMarker(pos));
          }
        });
      }
  }

  public mapFinishedLoading(map: Map): void {
    this.map = map;
  }

  public getMarker(position: GeolocationPosition): Marker {
    return marker([ position.coords.latitude, position.coords.longitude ], {
      title: 'Тековна локација',
      alt: 'Тековна локација',
      icon: icon({
        iconSize: [ 25, 41 ],
        iconAnchor: [ 13, 41 ],
        iconUrl: 'leaflet/marker-icon.png',
        shadowUrl: 'leaflet/marker-shadow.png'
      })
    });
  }
}
