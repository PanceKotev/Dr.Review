import { IOptionItemWithLink } from './interfaces';
import { Observable } from 'rxjs';
import { FilterBy  } from "../enums/filter.enum";

export class BaseFilter {
  public page = 0;
  public itemsPerPage = 250;

  public constructor(page: number = 0, itemsPerPage :number = 10000){
    this.page = page;
    this.itemsPerPage = itemsPerPage;
  }
};

export class GetDoctorsFilter extends BaseFilter {
  public constructor(filterBy?: FilterByValue, page: number | undefined = undefined, itemsPerPage: number | undefined = undefined){
    super(page ?? 0, itemsPerPage ?? 10000);
    this.filterBy = filterBy;
  }

  public filterBy?: FilterByValue;

  public toQueryProperties(): string {
    let filter = `?page=${this.page}&itemsCount=${this.itemsPerPage}`;

    if(this.filterBy){
     filter = `${filter}&filterBy=${this.filterBy.property.valueOf()}&filterByValue=${this.filterBy.value}`;
    }

    return filter;

  }
}

export interface FilterByValue {
 property: FilterBy;
 value: string;
};

export interface PagingFilter {
  page: number;
  itemsPerPage: number;
}


export type IAdditionalSelectConfig =
  ILocationAdditionalSelectConfig |
  IInstitutionAdditionalSelectConfig |
  ISpecializationAdditionalSelectConfig |
  IAllSpecializationAdditionalSelectConfig;
export interface ILocationAdditionalSelectConfig {
  filterType: FilterBy.LOCATION;
  items$: Observable<IOptionItemWithLink<string>[]>;
  onlySubscriptions?: boolean;
}
export interface IInstitutionAdditionalSelectConfig {
  filterType: FilterBy.INSTITUTION;
  items$: Observable<IOptionItemWithLink<string>[]>;
  onlySubscriptions?: boolean;
}

export interface ISpecializationAdditionalSelectConfig {
  filterType: FilterBy.SPECIALIZATION;
  items$: Observable<IOptionItemWithLink<string>[]>;
  onlySubscriptions?: boolean;
}

export interface IAllSpecializationAdditionalSelectConfig {
  filterType: FilterBy.ALL;
  items$: Observable<IOptionItemWithLink<string>[]>;
  onlySubscriptions?: boolean;
};
