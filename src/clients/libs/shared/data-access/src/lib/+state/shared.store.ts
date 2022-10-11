import { Injectable } from '@angular/core';
import { Store, StoreConfig } from '@datorama/akita';

export interface SharedState {
   email: string;
   fullName: string;
}

export function createInitialState(): SharedState {
  return {
    email: '',
    fullName: ''
  };
}

@StoreConfig({ name: 'shared' })
@Injectable({providedIn: 'root'})
export class SharedStore extends Store<SharedState> {
  public constructor() {
    super(createInitialState());
  }
}
