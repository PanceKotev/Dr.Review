import { Injectable, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { OptionApiService } from '../common/option-api.service';
import { ILocation } from '../models/common';
import { UserApiService } from '../user';
import { SharedStore } from './shared.store';

@Injectable({
  providedIn: 'root'
})
export class SharedFacade implements OnDestroy {
  private destroying$ = new Subject<boolean>();
  public constructor(
    private optionsApiService: OptionApiService,
    private userApiService: UserApiService,
    private sharedStore: SharedStore) {}

  public getAndCacheCurrentUser(): void {
    this.userApiService.getUserDetails().pipe(
      takeUntil(this.destroying$)
    )
    .subscribe(val => {
      this.sharedStore.setCurrentUser(val);
    });
  }

  public getAndCacheFilterOptions(): void {
    this.optionsApiService.getAllOptionItems().pipe(
      takeUntil(this.destroying$)
    )
    .subscribe(val => {
      this.sharedStore.setOptionValues(val);
    });
  }

  public setHomepageLocationNear(location: ILocation): void {
    this.sharedStore.setNearLocation(location);
  }

  public ngOnDestroy(): void{
    this.destroying$.next(true);
    this.destroying$.complete();
  }
}
