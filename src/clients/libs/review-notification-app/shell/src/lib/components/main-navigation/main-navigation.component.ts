import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'drreview-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrls: ['./main-navigation.component.scss']
})
export class MainNavigationComponent {

  @Input()
  public initialDarkThemeValue = false;

  @Output()
  public themeChanged= new EventEmitter();

  public isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  public constructor(private breakpointObserver: BreakpointObserver) {}


  public toggleTheme(): void {
    this.themeChanged.emit();
  }
}
