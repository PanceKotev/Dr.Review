import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ThemesService } from '@drreview/shared/services/themes';

@Component({
  selector: 'drreview-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit, OnDestroy {
  public isDarkTheme = false;
  public randomNumber = 0;
  private readonly destroying$ = new Subject();


  public constructor(
    private themesService: ThemesService) {

  }

  public ngOnInit(): void {
    this.themesService.isDarkTheme$.pipe(
      takeUntil(this.destroying$)
      )
       .subscribe({
         next: res => {
           this.isDarkTheme = res;
         },
         error: err => {
           console.error(err);
         }
       });
  }
  public ngOnDestroy(): void {
    this.destroying$.next(true);
    this.destroying$.complete();
  }
}
