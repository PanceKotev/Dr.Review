import { MediaMatcher } from '@angular/cdk/layout';
import { Injectable, OnDestroy } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class ThemesService implements  OnDestroy{
  private readonly _preferredThemeQuery = "(prefers-color-scheme: dark)";
  public isDarkTheme$ = new BehaviorSubject(false);
  private mediaOnchange = (e : MediaQueryListEvent) : void => {
    this.isDarkTheme$.next(e.matches);
  };
  public constructor(private  matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private mediaMatcher: MediaMatcher) {

    const darkThemeToggled = this.mediaMatcher.matchMedia(this._preferredThemeQuery).matches;

    this.isDarkTheme$.next(darkThemeToggled);
    this.mediaMatcher.matchMedia(this._preferredThemeQuery).addEventListener("change", this.mediaOnchange);

    const logoPath = `/assets/logo/dr_review_logo_${darkThemeToggled? 'dark' : 'light'}.svg`;

    this.matIconRegistry.addSvgIcon(
      "drreview-logo",
      this.domSanitizer.bypassSecurityTrustResourceUrl(logoPath)
    );
  }

  public switchTheme(darkThemeValue: boolean): void {
    this.isDarkTheme$.next(darkThemeValue);
    this.matIconRegistry.addSvgIcon(
      "drreview-logo",
      this.domSanitizer.bypassSecurityTrustResourceUrl(`/assets/logo/dr_review_logo_
      ${darkThemeValue? 'dark' : 'light'}.svg`)
    );
  }

  public ngOnDestroy(): void {
    this.mediaMatcher.matchMedia(this._preferredThemeQuery).removeEventListener("change", this.mediaOnchange);
  }
}
