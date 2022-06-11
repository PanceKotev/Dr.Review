import { ApiService } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { BehaviorSubject, takeUntil } from 'rxjs';

@Component({
  selector: 'drreview-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit, OnDestroy {
  public isDarkTheme = false;
  public randomNumber = 0;
  private readonly $destroying = new BehaviorSubject(false);

  private readonly _preferredThemeQuery = "(prefers-color-scheme: dark)";


  public constructor(
    private apiService: ApiService,
    private mediaMatcher: MediaMatcher) {

  }

  public ngOnInit(): void {

    this.isDarkTheme = this.mediaMatcher.matchMedia(this._preferredThemeQuery).matches;
    this.apiService.get("migrations/random")
    .pipe(
      takeUntil(this.$destroying)
      )
        .subscribe({
        next: res => {
          console.log(res);
        },
        error: error => {
          console.error("error", error);
        },
        complete: () => {

        }
      });
  }
  public ngOnDestroy(): void {
    this.$destroying.next(true);
    this.$destroying.complete();
  }

  public toggleTheme(): void {
    this.isDarkTheme = !this.isDarkTheme;
  }
}
