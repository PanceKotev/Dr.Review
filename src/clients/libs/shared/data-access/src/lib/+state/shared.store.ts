import { Injectable } from '@angular/core';
import { Store, StoreConfig } from '@datorama/akita';
import { IAllOptionsItems, ILocation } from '../models/common';

export interface SharedState {
   email: string;
   fullName: string;
   locations: ILocation[];
   institutions: string[];
   specializations: string[];
   homepage: {
    nearLocation: ILocation | undefined;
   }
}

export function createInitialState(): SharedState {
  return {
    email: '',
    fullName: '',
    locations: [],
    institutions: [],
    specializations: [],
    homepage: {
      nearLocation: undefined
    }
  };
}

@StoreConfig({ name: 'shared' })
@Injectable({providedIn: 'root'})
export class SharedStore extends Store<SharedState> {
  public constructor() {
    super(createInitialState());
  }


  public setOptionValues(values: IAllOptionsItems): void {
    this.setLoading(true);
    this.update(state => ({
      ...state,
      ...values
    }));
    this.setLoading(false);
  }

  public setNearLocation(location: ILocation): void {
    this.setLoading(true);
    this.update(state => ({
      ...state,
      homepage: {
        nearLocation: location
      }
    }));
    this.setLoading(false);
  }
}
