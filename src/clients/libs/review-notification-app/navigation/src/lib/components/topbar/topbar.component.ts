import { AuthService, CurrentUser } from '@drreview/review-notification-app/common/services/auth';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { map, Observable, shareReplay } from 'rxjs';

@Component({
  selector: 'drreview-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent {

  @Input()
  public appTitle = "DrReview";

  @Output()
  public drawerToggled = new EventEmitter();


  public get currentUser(): CurrentUser | null {
    return this.authService.getCurrentUser();
  }


  public isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );


    public constructor(
      private breakpointObserver: BreakpointObserver,
      private authService: AuthService){
    }


    public toggleDrawer(): void {
      this.drawerToggled.emit();
    }

    public logIn(): void{
      this.authService.loginRedirect();
    }

    public logOut(): void {
      this.authService.logout();
    }

}
