
import { SearchDoctorDto, DoctorApiService, SharedQuery, ILocation } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { debounceTime, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { inputToCyrillic } from '@drreview/shared/utils/rxjs';
@Component({
  selector: 'drreview-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit, OnDestroy {
  private destroying$ = new Subject<void>();
  public searchFormName = 'searchDoctor';

  public filterBy = '';

  public doctors: SearchDoctorDto[] = [];
  public formGroup!: FormGroup;

  public selectedLocation$: Observable<{nearLocation: ILocation | undefined}> | undefined;

  public constructor(
    private fb: FormBuilder,
    private sharedQuery: SharedQuery,
    private doctorsApi: DoctorApiService){
      this.selectedLocation$ = this.sharedQuery.homepageOptions$;
      this.formGroup = this.fb.group({
        searchDoctor: [null]
      });
  }

  public ngOnInit(): void {
    this.formGroup.get(this.searchFormName)?.valueChanges.pipe(
      takeUntil(this.destroying$),
      debounceTime(500),
      inputToCyrillic(),
      switchMap((search: string) => {
        const formattedString = search.length ? search : ' ';

        return this.doctorsApi.searchDoctors(formattedString);
      })
    ).subscribe({
      next: results => {
        this.doctors = results;
      },
      error: error => {
        console.error(error);
      }
    });
  }

  public ngOnDestroy(): void {
      this.destroying$.next();
      this.destroying$.complete();
  }

}
