
import { SharedQuery, ILocation, TopDoctor, PopularityApiService } from '@drreview/shared/data-access';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { combineLatest,
  debounceTime, distinctUntilChanged, filter, map, Observable, of, startWith, Subject, switchMap, takeUntil } from 'rxjs';
import { notNull } from '@drreview/shared/utils/typescript-helpers';
@Component({
  selector: 'drreview-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnDestroy, OnInit {
  private destroying$ = new Subject<void>();

  public filterBy = '';
  public topDoctorsLoading = false;
  public topDoctors: TopDoctor[] = [];
  public kmDistance = new FormControl(25);

  public kmDistance$: Observable<number>;
  public selectedLocation$: Observable<{nearLocation: ILocation | undefined}>;
  public topDoctors$: Observable<TopDoctor[]> | undefined;

  public constructor(
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef,
    private sharedQuery: SharedQuery,
    private popularityApi: PopularityApiService){
      this.selectedLocation$ = this.sharedQuery.homepageOptions$;
      this.kmDistance$ = this.kmDistance.valueChanges.pipe(
        startWith(26),
        filter(notNull),
        map(x => x + 1),
        debounceTime(300),
        distinctUntilChanged());

  }
  public ngOnInit(): void {
    combineLatest([this.kmDistance$, this.selectedLocation$])
    .pipe(
      switchMap(([distance, {nearLocation}]) => {
        console.log('hit switchMap', nearLocation);

        if(!nearLocation){
          return of([]);
        }
        this.topDoctorsLoading = true;

        return this.popularityApi.getTopDoctorsNearLocation(nearLocation.latitude, nearLocation.longitude, distance);
      }),
      takeUntil(this.destroying$))
      .subscribe({
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
