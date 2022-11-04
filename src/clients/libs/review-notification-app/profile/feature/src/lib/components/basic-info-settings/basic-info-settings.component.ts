import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnDestroy } from '@angular/core';
import { Subject, takeUntil, tap } from 'rxjs';
import { CurrentUserDetailsDto, SharedFacade, SharedQuery, UserApiService } from '@drreview/shared/data-access';

@Component({
  selector: 'drreview-basic-info-settings',
  templateUrl: './basic-info-settings.component.html',
  styleUrls: ['./basic-info-settings.component.scss']
})
export class BasicInfoSettingsComponent implements OnDestroy{
  private destroying$ = new Subject<boolean>();

  public fg: FormGroup = this.fb.group({
    firstName: ['', [Validators.required, Validators.maxLength(200)]],
    lastName: ['', [Validators.required,  Validators.maxLength(200)]],
    email: ['', Validators.required]
  });

  public userDetails: CurrentUserDetailsDto | undefined;
  public valueChanged = false;
  public isLoading = true;

  public constructor(
    private fb: FormBuilder,
    private userApi: UserApiService,
    private sharedFacade: SharedFacade,
    private sharedQuery: SharedQuery){
      this.isLoading = true;
      this.sharedQuery.currentUser$.pipe(
          tap(val => {
            if(!val){
              this.sharedFacade.getAndCacheCurrentUser();
              }
            }),
          takeUntil(this.destroying$))
          .subscribe(
            {
            next: val => {
            if(val){
              this.userDetails = val;
              this.fg.setValue({
                firstName: val.firstName,
                lastName: val.lastName,
                email: val.email
              }, {
                emitEvent: false, onlySelf: true
              });

            }
            this.isLoading = false;
          },
          error: err => {
            console.log(err);
            this.isLoading = false;
          }});

      this.fg.valueChanges.pipe(takeUntil(this.destroying$)).subscribe(change => {
        this.valueChanged = change.firstName !== this.userDetails?.firstName || change.lastName!== this.userDetails?.lastName;


      });
  }
  public updateUser(): void {
    if(this.fg.valid && this.valueChanged){
      this.isLoading = true;
      const fgValue = this.fg.getRawValue();
      this.userApi.updateUser({
        firstName: fgValue.firstName,
        lastName: fgValue.lastName
      }).pipe(takeUntil(this.destroying$))
      .subscribe(() => {
        this.sharedFacade.getAndCacheCurrentUser();
      });
    }
  }
  public ngOnDestroy(): void {
      this.destroying$.next(true);
      this.destroying$.complete();
  }
}
