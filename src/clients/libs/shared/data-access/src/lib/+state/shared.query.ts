import { Injectable } from '@angular/core';
import { Query } from '@datorama/akita';
import { SharedState, SharedStore } from './shared.store';

@Injectable({
  providedIn: 'root'
})
export class SharedQuery extends Query<SharedState> {
  public constructor(protected override store: SharedStore) {
    super(store);
  }

}
