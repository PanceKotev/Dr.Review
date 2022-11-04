import { Component } from '@angular/core';
import { CurrentUserDetailsDto, SharedFacade, SharedQuery } from '@drreview/shared/data-access';
import { Observable } from 'rxjs';

@Component({
  selector: 'drreview-profile-details-root',
  templateUrl: './profile-details-root.component.html',
  styleUrls: ['./profile-details-root.component.scss']
})
export class ProfileDetailsRootComponent {
  public currentUser$: Observable<CurrentUserDetailsDto | undefined>;
  public constructor(private facade: SharedFacade, sharedQuery: SharedQuery){
    this.facade.getAndCacheCurrentUser();
    this.currentUser$ = sharedQuery.currentUser$;
  }
}
