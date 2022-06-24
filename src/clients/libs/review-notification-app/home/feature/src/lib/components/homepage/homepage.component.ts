
import { SearchDoctorDto, DoctorApiService } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { debounceTime, Subject, switchMap, takeUntil } from 'rxjs';

@Component({
  selector: 'drreview-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit, OnDestroy {
  private destroying$ = new Subject<void>();
  public searchFormName = 'searchDoctor';

  public doctors: SearchDoctorDto[] = [];
  public formGroup!: FormGroup;

  public constructor(private fb: FormBuilder, private doctorsApi: DoctorApiService){
    this.formGroup = this.fb.group({
      searchDoctor: [null]
    });
  }

  public ngOnInit(): void {
    this.formGroup.get(this.searchFormName)?.valueChanges.pipe(
      takeUntil(this.destroying$),
      debounceTime(500),
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
