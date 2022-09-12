import { AuthService, CurrentUser } from '@drreview/review-notification-app/common/services/auth';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, Input } from '@angular/core';
import { map, Observable, shareReplay } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'drreview-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent {
  public searchFormName = 'searchDoctor';

  public formGroup = new FormGroup({
    searchDoctor: new FormControl<string | undefined>('')
  });
  @Input()
  public appTitle = "DrReview";


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

    public logIn(): void{
      this.authService.loginRedirect();
    }

    public logOut(): void {
      this.authService.logout();
    }

}
