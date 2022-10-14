
import { DoctorApiService, SharedQuery, ILocation, TopDoctor, PopularityApiService } from '@drreview/shared/data-access';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable, of, Subject, switchMap, takeUntil } from 'rxjs';
@Component({
  selector: 'drreview-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnDestroy, OnInit {
  private destroying$ = new Subject<void>();
  public searchFormName = 'searchDoctor';

  public filterBy = '';
  public topDoctorsLoading = false;
  public topDoctors: TopDoctor[] = [];
  public formGroup!: FormGroup;

  public selectedLocation$: Observable<{nearLocation: ILocation | undefined}>;
  public topDoctors$: Observable<TopDoctor[]> | undefined;

  public constructor(
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef,
    private sharedQuery: SharedQuery,
    private popularityApi: PopularityApiService,
    private doctorsApi: DoctorApiService){
      this.selectedLocation$ = this.sharedQuery.homepageOptions$;

      this.formGroup = this.fb.group({
        searchDoctor: [null]
      });
  }
  public ngOnInit(): void {
    this.selectedLocation$.pipe(
      switchMap(({nearLocation}) => {
        console.log('hit switchMap', nearLocation);

        if(!nearLocation){
          return of([]);
        }
        this.topDoctorsLoading = true;

        return this.popularityApi.getTopDoctorsNearLocation(nearLocation.suid);
      }),
      takeUntil(this.destroying$)).subscribe({
        next: val => {
          this.topDoctors = val;
          this.topDoctorsLoading = false;
          this.cdr.detectChanges();
        },
        error: err => {
          console.log(err);
          this.topDoctorsLoading = false;
        }
      });
  }
  public ngOnDestroy(): void {
      this.destroying$.next();
      this.destroying$.complete();
  }

}
