import { Injectable } from '@angular/core';
import { Query } from '@datorama/akita';
import { SharedState, SharedStore } from './shared.store';

@Injectable({
  providedIn: 'root'
})
export class SharedQuery extends Query<SharedState> {
  public locations$ = this.select(x => x.locations);
  public institutions$ = this.select(x => x.institutions);
  public specializations$ = this.select(x => x.specializations);
  public isLoading$ = this.selectLoading();
  public homepageOptions$ = this.select(x => x.homepage);

  public constructor(protected override store: SharedStore) {
    super(store);
  }

}
