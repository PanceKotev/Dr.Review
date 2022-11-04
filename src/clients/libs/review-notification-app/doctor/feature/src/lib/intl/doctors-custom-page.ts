import { MatPaginatorIntl } from "@angular/material/paginator";
import { Subject } from "rxjs";

export const DoctorsCustomLabels: MatPaginatorIntl = {
  firstPageLabel: 'Прва Страна',
  changes: new Subject<void>(),
  itemsPerPageLabel: 'бр. доктори по страна',
 lastPageLabel: 'Последна Страна',
 nextPageLabel: 'Следна Страна',
 getRangeLabel: (page, pageSize, length) => {
  if (length === 0 || pageSize === 0) {
    return `0 од ${length}`;
  }

  const customLength =Math.max(length, 0);

  const startIndex = page * pageSize;
  const endIndex = startIndex < length ?
    Math.min(startIndex + pageSize, length) :
    startIndex + pageSize;

  return `${startIndex+1} - ${endIndex} од ${customLength}`;
}
 ,
 previousPageLabel: 'Претходна Страна'
};

