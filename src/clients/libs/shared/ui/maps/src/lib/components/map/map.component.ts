/* eslint-disable @typescript-eslint/no-shadow */
import { combineLatest, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ILocation, SharedFacade, SharedQuery } from '@drreview/shared/data-access';
import { LatLng, Map as LeafletMap,
    latLng, MapOptions, tileLayer, Marker, marker, icon, Layer, IconOptions } from 'leaflet';

@Component({
  selector: 'drreview-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit, OnDestroy {
  public currentLocation: LatLng  | undefined;
  public markersMap = new Map<LatLng, Layer>();
  public markersOriginalIcons = new Map<LatLng, IconOptions>();
  public map: LeafletMap | undefined;
  public locations: ILocation[] = [];

  public markersMapLoaded$ = new Subject<boolean>();
  private destroying$ = new Subject<boolean>();

  public options: MapOptions =
  {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: ' &copy; OpenStreetMap contributors'
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

  public constructor(
    private sharedQuery: SharedQuery,
    private sharedFacade: SharedFacade){

    combineLatest([sharedQuery.homepageOptions$, this.markersMapLoaded$]).pipe(takeUntil(this.destroying$))
      .subscribe(([val,,]) => {
        if(!val.nearLocation){
          return;
        }
        const currentLatLng = latLng(val.nearLocation.latitude, val.nearLocation.longitude);
        this.currentLocation = currentLatLng;

        const entryFromMap = Array.from(this.markersMap.entries())
          .find(([key,,]) => key.lat === currentLatLng.lat && key.lng === currentLatLng.lng);

        if(entryFromMap){
          const selectedMarker = entryFromMap[1] as Marker;

          if(!selectedMarker){
            return;
          }
          const existingIcon = selectedMarker.getIcon();


          this.markersMap.forEach((val, key) => {
            if(key !== selectedMarker.getLatLng() && this.markersOriginalIcons.has(key)){
              const originalIcon = this.markersOriginalIcons.get(key);
              // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
              if(originalIcon){
                (val as Marker).setIcon(icon(originalIcon));
              }
            }
          });

          selectedMarker.setIcon(icon({
            ...existingIcon.options as IconOptions,
            iconSize: [30, 46],
            iconAnchor: [15, 46]
          }));
        }
      });
    sharedQuery.locations$.pipe(takeUntil(this.destroying$))
      .subscribe({
        next: val => {
          this.locations = val ?? [];
          val.forEach(v => {
            const latitude = latLng(v.latitude, v.longitude);
            const marker2 = this.getLocationMarker(latitude, v.name);
            this.markersOriginalIcons.set(latitude, marker2.getIcon().options as IconOptions);
            if(!this.markersMap.has(latitude)){
              marker2.addEventListener('click', () =>  this.handleMarkerClick(marker2));
            }
            this.markersMap.set(latitude, marker2);
          });

          this.markersMapLoaded$.next(true);
        }
      });
  }

  public ngOnInit(): void {
      if (navigator.geolocation){
        navigator.geolocation.getCurrentPosition((pos) => {
          if(pos && this.map){
              const latitudeLongitude = latLng(pos.coords.latitude, pos.coords.longitude);
              if(this.currentLocation){
                this.map.flyTo(this.currentLocation, 10);
              } else {
                this.map.flyTo(latitudeLongitude, 10);

              }
              const marker1 = this.getMarker(latitudeLongitude);
              if(!this.markersMap.has(latitudeLongitude)){
                marker1.addEventListener('click', () =>  this.handleMarkerClick(marker1));
              }
              this.markersMap.set(latitudeLongitude, marker1);
              this.markersOriginalIcons.set(latitudeLongitude, marker1.getIcon().options as IconOptions);

              if(!this.currentLocation){
                this.handleMarkerClick(marker1);
              }
            }

        });
      }
  }

  public handleMarkerClick(markerLayer: Marker): void {
    const existingIcon = markerLayer.getIcon();
    this.markersMap.forEach((val, key) => {
      if(key !== markerLayer.getLatLng() && this.markersOriginalIcons.has(key)){
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        (val as Marker).setIcon(icon(this.markersOriginalIcons.get(key)!));
      }
    });

    markerLayer.setIcon(icon({
      ...existingIcon.options as IconOptions,
      iconSize: [30, 46],
      iconAnchor: [15, 46]
    }));

    const location = this.locations.find(x => x.latitude === markerLayer.getLatLng().lat && x.longitude === markerLayer.getLatLng().lng);

    if(location){
      this.sharedFacade.setHomepageLocationNear({...location});
    } else {
      this.sharedFacade.setHomepageLocationNear({
        latitude: markerLayer.getLatLng().lat,
        longitude: markerLayer.getLatLng().lng,
        name: 'Тековна локација',
        suid: ''
      });
    }
  }
  public mapFinishedLoading(map: LeafletMap): void {
    this.map = map;
  }

  public getMarker(latLng: LatLng, title: string = 'Тековна Локација'): Marker {
    return marker([ latLng.lat, latLng.lng ], {
      title: title,
      alt: title,
      icon: icon({
        iconSize: [ 25, 41 ],
        iconAnchor: [ 13, 41 ],
        iconUrl: 'leaflet/marker-icon.png',
        shadowUrl: 'leaflet/marker-shadow.png'
      }),
      zIndexOffset: 50
    });
  }

  public getLocationMarker(latLng: LatLng, title: string = 'Локација'): Marker {
    return marker([ latLng.lat, latLng.lng ], {
      title: title,
      alt: title,
      icon: icon({
        iconSize: [ 18, 34 ],
        iconAnchor: [ 10, 34 ],
        iconUrl: 'map-markers/hospital-location-marker.png',
        shadowUrl: 'leaflet/marker-shadow.png',
        shadowAnchor: [10, 38]
      })
    });
  }

  public ngOnDestroy(): void {
      this.destroying$.next(true);
      this.destroying$.complete();

    this.markersMap.forEach(mark => {
      mark.removeEventListener('click', () => this.handleMarkerClick(mark as Marker));
    });
  }
}
