import { MediaMatcher } from '@angular/cdk/layout';
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class ThemesService implements  OnDestroy{
  private readonly _preferredThemeQuery = "(prefers-color-scheme: dark)";
  public isDarkTheme$ = new BehaviorSubject(false);
  private mediaOnchange = (e : MediaQueryListEvent) : void => {
    console.log(e);
    this.isDarkTheme$.next(e.matches);
  };

  public constructor(private mediaMatcher: MediaMatcher){
    this.isDarkTheme$.next(this.mediaMatcher.matchMedia(this._preferredThemeQuery).matches);
    this.mediaMatcher.matchMedia(this._preferredThemeQuery).addEventListener("change", this.mediaOnchange);
  }

  public switchTheme(darkThemeValue: boolean): void {
    this.isDarkTheme$.next(darkThemeValue);
  }

  public ngOnDestroy(): void {
    this.mediaMatcher.matchMedia(this._preferredThemeQuery).removeEventListener("change", this.mediaOnchange);
  }
}
