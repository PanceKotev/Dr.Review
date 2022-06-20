
import { SearchDoctorDto, DoctorApiService } from '@drreview/shared/data-access';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { debounceTime, EMPTY, Subject, switchMap, takeUntil } from 'rxjs';

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
      debounceTime(300),
      switchMap((search: string) => {
        if(search.length > 3){
          return this.doctorsApi.searchDoctors(search);
        }

        return EMPTY;
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
