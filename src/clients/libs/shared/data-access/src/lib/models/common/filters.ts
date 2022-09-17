import { FilterBy } from "../enums/filter.enum";

export class BaseFilter {
  public startPage = 0;
  public itemsPerPage = 250;
};

export class GetDoctorsFilter extends BaseFilter {
  public constructor(filterBy: FilterByValue | undefined){
    super();
    this.filterBy = filterBy;
  }

  public filterBy?: FilterByValue;

  public toQueryProperties(): string {

    if(this.filterBy){
     return `?filterBy=${this.filterBy.property.valueOf()}&filterByValue=${this.filterBy.value}`;
    }

    return '';

  }
}

export interface FilterByValue {
 property: FilterBy;
 value: string;
};
