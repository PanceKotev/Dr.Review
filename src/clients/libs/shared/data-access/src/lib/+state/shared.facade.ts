import { Injectable } from '@angular/core';
import { SharedStore } from './shared.store';

@Injectable({
  providedIn: 'root'
})
export class SharedFacade {
  public constructor(private sharedStore: SharedStore) {}

  public updateUserName(newName: string): void {
    this.sharedStore.setLoading(true);
    this.sharedStore.update((state) => ({
      ...state,
      fullName: newName
    }));
    this.sharedStore.setLoading(false);
  }
}
