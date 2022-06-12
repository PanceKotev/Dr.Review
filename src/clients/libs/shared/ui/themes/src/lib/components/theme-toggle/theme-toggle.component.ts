import { Component, OnDestroy, OnInit } from '@angular/core';
import { ThemesService } from '@drreview/shared/services/themes';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'drreview-theme-toggle',
  templateUrl: './theme-toggle.component.html',
  styleUrls: ['./theme-toggle.component.scss']
})
export class ThemeToggleComponent implements OnInit, OnDestroy{
  public isDarkTheme = false;
  private destroying$ = new Subject();

  public constructor(private themesService: ThemesService){
  }

  public ngOnInit(): void {
    this.themesService.isDarkTheme$.pipe(
      takeUntil(this.destroying$)
      ).subscribe({
        next: res => {
          this.isDarkTheme = res;
        },
        error: err => {
          console.error(err);
        }
      });
  }
  public switchTheme(): void{
    this.themesService.switchTheme(!this.isDarkTheme);
  }

  public ngOnDestroy(): void {
    this.destroying$.next(true);
    this.destroying$.complete();
  }

}
