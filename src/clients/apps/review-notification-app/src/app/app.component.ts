import { SharedFacade } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '@drreview/review-notification-app/common/services/auth';

@Component({
  selector: 'drreview-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy{

  public title = 'review-notification-app';
  public randomNumber = 0;

  public constructor(
    private authService: AuthService,
    private sharedFacade: SharedFacade){

  }

  public ngOnInit(): void {
    this.authService.initializeAuth();
    this.sharedFacade.getAndCacheFilterOptions();
  }
  public ngOnDestroy(): void {
    this.authService.ngOnDestroy();
  }

}
