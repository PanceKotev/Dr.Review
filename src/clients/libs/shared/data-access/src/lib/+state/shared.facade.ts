import { Injectable } from '@angular/core';
import { OptionApiService } from '../common/option-api.service';
import { ILocation } from '../models/common';
import { SharedStore } from './shared.store';

@Injectable({
  providedIn: 'root'
})
export class SharedFacade {
  public constructor(private optionsApiService: OptionApiService,private sharedStore: SharedStore) {}

  public updateUserName(newName: string): void {
    this.sharedStore.setLoading(true);
    this.sharedStore.update((state) => ({
      ...state,
      fullName: newName
    }));
    this.sharedStore.setLoading(false);
  }

  public getAndCacheFilterOptions(): void {
    this.optionsApiService.getAllOptionItems().subscribe(val => {
      this.sharedStore.setOptionValues(val);
    });
  }

  public setHomepageLocationNear(location: ILocation): void {
    this.sharedStore.setNearLocation(location);
  }
}
