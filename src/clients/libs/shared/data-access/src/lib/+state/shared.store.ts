import { Injectable } from '@angular/core';
import { Store, StoreConfig } from '@datorama/akita';
import { IAllOptionsItems, ILocation } from '../models/common';
import { CurrentUserDetailsDto } from '../models/user';

export interface SharedState {
   currentUser: CurrentUserDetailsDto | undefined;
   locations: ILocation[];
   institutions: string[];
   specializations: string[];
   homepage: {
    nearLocation: ILocation | undefined;
   }
}

export function createInitialState(): SharedState {
  return {
    currentUser: undefined,
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

  public setCurrentUser(currentUser: CurrentUserDetailsDto): void {
    this.setLoading(true);
    this.update(state => ({
      ...state,
      currentUser: {...currentUser}
    }));
    this.setLoading(false);
  }
}
