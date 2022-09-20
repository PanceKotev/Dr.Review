import { Router } from '@angular/router';
import { Observable, of, combineLatest, switchMap, startWith } from 'rxjs';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
} from '@angular/core';
import {
  IAdditionalSelectConfig,
  IOptionItemWithLink,
  FilterBy
} from '@drreview/shared/data-access';
import { FormControl } from '@angular/forms';
import { inputToCyrillic } from '@drreview/shared/utils/rxjs';
import { ThemesService } from '@drreview/shared/services/themes';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'drreview-additional-select-filter',
  templateUrl: './additional-select-filter.component.html',
  styleUrls: ['./additional-select-filter.component.scss']
})
export class AdditionalSelectFilterComponent implements OnInit, OnChanges {
  @Input() public config: IAdditionalSelectConfig | undefined;
  @Input() public selectedOption: string | undefined;

  public autocompleteForm = new FormControl('');

  public filterType = FilterBy.ALL;

  public FilterBy = FilterBy;

  public items$: Observable<IOptionItemWithLink<string>[]> | undefined;

  public filteredItems$: Observable<IOptionItemWithLink<string>[]> | undefined;

  @Output()
  public selectionChanged = new EventEmitter<string>();

  public constructor(public themeService: ThemesService, private router: Router){

  }

  public ngOnInit(): void {
    this.items$ = this.config?.items$;
    this.setItemsObservable();
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes['config']) {
      this.items$ = this.config?.items$;
      this.setItemsObservable();
    }

    if (changes['selectedOption']){
      this.autocompleteForm.setValue(this.selectedOption ?? '');
    }
  }

  public setItemsObservable(): void {
    let items: Observable<IOptionItemWithLink<string>[]> = of([]);

    if (this.config?.items$) {
      items = combineLatest([
        this.autocompleteForm.valueChanges.pipe(startWith(''), inputToCyrillic()),
        this.config.items$
      ]).pipe(
        switchMap(([formFilter, item]) => {
          console.log('wot');
          console.log(formFilter);
          if (!formFilter) {
            return of(item);
          }
          console.log(formFilter, '2');
          console.log(item, '2');

          const filteredItems = item.filter((i) =>
               i.value.toLowerCase().includes(formFilter.toLowerCase()));
          console.log('filtered items', filteredItems);

          return of(
                filteredItems
              );
        })
      );
    }
    console.log('here');
    this.filteredItems$ = items;
  }

  public filterSelected(selection: MatAutocompleteSelectedEvent): void {
    this.selectionChanged.emit(selection.option.value ?? '');
  }
}
