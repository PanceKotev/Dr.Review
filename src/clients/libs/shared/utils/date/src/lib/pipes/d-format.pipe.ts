import { Pipe, PipeTransform } from '@angular/core';
import { drReviewDate }  from '../custom-dayjs';

@Pipe({
  name: 'dFormat'
})
export class DFormatPipe implements PipeTransform {

  public transform(value: Date | string | undefined | null, arg: string = ''): string {
    if(!arg.length){
      return value? value.toString() : '';
    }

    return value ? drReviewDate().format(arg) : '';
  }

}
