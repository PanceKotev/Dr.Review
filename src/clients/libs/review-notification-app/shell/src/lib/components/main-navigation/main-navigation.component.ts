import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'drreview-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrls: ['./main-navigation.component.scss']
})
export class MainNavigationComponent {

  @ViewChild("sidenav")
  public sidenav: MatSidenav | undefined;

  public isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  public constructor(private breakpointObserver: BreakpointObserver) {}

  public toggleSidenav(): void {
    if(this.sidenav){
      console.log('clicked sidenav');

      this.sidenav.toggle();
    }
  }
}
