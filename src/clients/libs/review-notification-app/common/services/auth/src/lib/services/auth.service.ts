import { Inject, Injectable, OnDestroy } from '@angular/core';
import {
  MsalBroadcastService,
  MsalGuardConfiguration,
  MsalService,
  MSAL_GUARD_CONFIG
} from '@azure/msal-angular';
import { EventMessage, EventType, InteractionStatus, RedirectRequest } from '@azure/msal-browser';
import { ApiService } from '@drreview/shared/data-access';
import { filter, Subject, takeUntil, switchMap } from 'rxjs';
import { CurrentUser } from '../models/current-user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  public isIframe = false;
  private readonly _destroying$ = new Subject<void>();

  public constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private msalService: MsalService,
    private apiService: ApiService,
    private msalBroadcastService: MsalBroadcastService
  ) {
    this.isIframe = window !== window.parent && !window.opener;
  }

  public initializeAuth(): void {

    this.msalService.instance.handleRedirectPromise().then();
    this.msalService.instance.enableAccountStorageEvents();
    this.msalBroadcastService.msalSubject$
      .pipe(
        filter(
          (msg: EventMessage) =>
            msg.eventType === EventType.ACCOUNT_ADDED ||
            msg.eventType === EventType.ACCOUNT_REMOVED
        )
      )
      .subscribe({
        next: () => {
          if (this.msalService.instance.getAllAccounts().length === 0) {
            window.location.pathname = '/';
          }
        },
        error: (err) => {
          console.log(err);
        }
      });

    this.msalBroadcastService.inProgress$
      .pipe(
        filter(
          (status: InteractionStatus) => status === InteractionStatus.None
        ),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.checkAndSetActiveAccount();
      });

      this.msalBroadcastService.msalSubject$.pipe(
        filter(v => v.eventType === EventType.LOGIN_SUCCESS),
        switchMap(() => this.apiService.post("v1/users/create")),
        takeUntil(this._destroying$)
      )
        .subscribe({
          next : v =>{
            console.log(v);
          },
          error: err => {
            console.log(err);
          }
        });
  }

  public checkAndSetActiveAccount(): void {
    const activeAccount = this.msalService.instance.getActiveAccount();

    if (
      !activeAccount &&
      this.msalService.instance.getAllAccounts().length > 0
    ) {
      const accounts = this.msalService.instance.getAllAccounts();
      this.msalService.instance.setActiveAccount(accounts[0]);
    }
  }

  public loginRedirect(): void {
    if (this.msalGuardConfig.authRequest) {
      this.msalService.loginRedirect({
        ...this.msalGuardConfig.authRequest
      } as RedirectRequest);
    } else {
      this.msalService.loginRedirect();
    }
  }

  public getCurrentUser(): CurrentUser | null {
    const activeAccount = this.msalService.instance.getActiveAccount();

    if (!activeAccount) {
      return null;
    }

    if (!activeAccount.idTokenClaims) {
      return null;
    }

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const idTokenClaims = activeAccount.idTokenClaims as any;

    return {
      uid: idTokenClaims.sub,
      firstName: idTokenClaims.given_name,
      lastName: idTokenClaims.family_name,
      email: idTokenClaims.email
    };

  }

  public logout(): void {
    this.msalService.logoutRedirect();
  }

  public ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }
}
